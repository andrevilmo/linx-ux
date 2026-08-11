param(
    [switch]$SkipBuild,
    [string]$OutRoot = 'C:\Linx Workspace\out\toPublish',
    [string]$BaselineRoot = $env:LINX_IIS_ROOT,
    # Comma-separated or array: All | Tools | Bv | Application | Portal
    # Example CI: -BuildTargets Portal   or -BuildTargets Tools,Application,Portal
    [string[]]$BuildTargets = @('All')
)

$ErrorActionPreference = 'Stop'

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
# Source tree lives under Main\ (same resolution as deploy-to-linx-framework.ps1)
$workspace = if (Test-Path (Join-Path $repoRoot 'Main\Application')) {
    Join-Path $repoRoot 'Main'
}
else {
    $repoRoot
}

if (-not $BaselineRoot) {
    $BaselineRoot = 'C:\Linx Program Files\Linx Framework 6.0.0'
}

# Normalize BuildTargets (allow a single comma-separated string from SSM/env).
$rawTargets = @()
foreach ($t in $BuildTargets) {
    if ([string]::IsNullOrWhiteSpace($t)) { continue }
    $rawTargets += @($t.Split(',') | ForEach-Object { $_.Trim() } | Where-Object { $_ })
}
if ($rawTargets.Count -eq 0) { $rawTargets = @('All') }
$targetSet = New-Object 'System.Collections.Generic.HashSet[string]' ([StringComparer]::OrdinalIgnoreCase)
foreach ($t in $rawTargets) { [void]$targetSet.Add($t) }
$buildAll = $targetSet.Contains('All')
function Test-ShouldBuild([string]$Name) {
    if ($buildAll) { return $true }
    return $targetSet.Contains($Name)
}

$portalProject = Join-Path $workspace 'Application\Linx.Portal\Linx.Portal'
$applicationProject = Join-Path $workspace 'Application\Linx.Internet.Application\Linx.Internet.Application'
$apiRoot = Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI.DS'
$bvRoot = Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV'

$binaryPortalBin = Join-Path $workspace 'Binary\Portal\bin'
$binaryServiceBin = Join-Path $workspace 'Binary\Service\bin'
$binaryApplicationBin = Join-Path $workspace 'Binary\Application\bin'
$applicationPublishBin = Join-Path $workspace 'Application\Linx.Internet.Application\publish-output\bin'

$portalDllSources = @{
    'Linx.Portal.dll' = @(
        (Join-Path $portalProject 'bin\Linx.Portal.dll')
        (Join-Path $binaryPortalBin 'Linx.Portal.dll')
    )
    'Linx.Tools.dll' = @(
        (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll')
        (Join-Path $binaryPortalBin 'Linx.Tools.dll')
        (Join-Path $binaryServiceBin 'Linx.Tools.dll')
    )
    'Linx.Resources.Localization.dll' = @(
        (Join-Path $binaryServiceBin 'Linx.Resources.Localization.dll')
        (Join-Path $binaryPortalBin 'Linx.Resources.Localization.dll')
    )
}

$serviceDllSources = @{
    'Linx.Tools.dll' = @(
        (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll')
        (Join-Path $binaryServiceBin 'Linx.Tools.dll')
    )
    'Linx.Framework.BV.dll' = @(
        (Join-Path $bvRoot 'bin\Release\Linx.Framework.BV.dll')
        (Join-Path $binaryServiceBin 'Linx.Framework.BV.dll')
    )
    'Linx.Framework.BV.WebAPI.DS.dll' = @(
        (Join-Path $apiRoot 'bin\Release\Linx.Framework.BV.WebAPI.DS.dll')
        (Join-Path $binaryServiceBin 'Linx.Framework.BV.WebAPI.DS.dll')
    )
}

$applicationDllSourceRoots = @(
    $binaryApplicationBin
    $applicationPublishBin
    (Join-Path $workspace 'Binary\Library\User Interface')
)

function Resolve-NewestSource {
    param([string[]]$Candidates)

    $found = @()
    foreach ($path in $Candidates) {
        if (Test-Path $path) {
            $found += Get-Item $path
        }
    }

    if ($found.Count -eq 0) {
        return $null
    }

    return ($found | Sort-Object LastWriteTime -Descending | Select-Object -First 1)
}

function Copy-PublishArtifact {
    param(
        [System.IO.FileInfo]$Source,
        [string]$DestPath,
        [string]$Site,
        [string]$Kind,
        [string]$Relative
    )

    $destDir = Split-Path $DestPath -Parent
    if (-not (Test-Path $destDir)) {
        New-Item -ItemType Directory -Force -Path $destDir | Out-Null
    }

    Copy-Item -Path $Source.FullName -Destination $DestPath -Force

    if ($Kind -eq 'Dll') {
        $pdbSource = [System.IO.Path]::ChangeExtension($Source.FullName, '.pdb')
        if (Test-Path $pdbSource) {
            Copy-Item -Path $pdbSource -Destination (Join-Path $destDir ([System.IO.Path]::GetFileName($pdbSource))) -Force
        }
    }

    return [PSCustomObject]@{
        Site     = $Site
        Kind     = $Kind
        Relative = $Relative
        Source   = $Source.FullName
    }
}

function Resolve-ApplicationDllSource {
    param(
        [string]$DllName,
        [hashtable]$DiscoveredSources
    )

    if ($DiscoveredSources.ContainsKey($DllName)) {
        return $DiscoveredSources[$DllName]
    }

    $baselinePath = Join-Path (Join-Path (Join-Path $BaselineRoot 'Application') 'bin') $DllName
    if (Test-Path $baselinePath) {
        return Get-Item $baselinePath
    }

    return $null
}

function Get-ApplicationDllNames {
    param([hashtable]$DiscoveredSources)

    $applicationBaselineBin = Join-Path (Join-Path $BaselineRoot 'Application') 'bin'
    if (Test-Path $applicationBaselineBin) {
        return @(Get-ChildItem -Path $applicationBaselineBin -Filter 'Linx*.dll' -File |
            Select-Object -ExpandProperty Name |
            Sort-Object)
    }

    Write-Warning "Application IIS bin not found: $applicationBaselineBin. Using discovered Linx*.dll sources only."
    return @($DiscoveredSources.Keys | Sort-Object)
}

function Get-ApplicationDllSources {
    $byName = @{}

    foreach ($root in $applicationDllSourceRoots) {
        if (-not (Test-Path $root)) {
            continue
        }

        Get-ChildItem -Path $root -Filter 'Linx*.dll' -File -ErrorAction SilentlyContinue | ForEach-Object {
            $name = $_.Name
            if (-not $byName.ContainsKey($name) -or $_.LastWriteTime -gt $byName[$name].LastWriteTime) {
                $byName[$name] = $_
            }
        }
    }

    # Prefer known UI bin roots over a full recursive walk of User Interface (slow on t3.small).
    $uiBinRoots = @(
        (Join-Path $workspace 'User Interface\Linx.Framework.BV\Linx.Framework.BV.SPA\bin')
        (Join-Path $workspace 'User Interface\Linx.Framework.BV\Linx.Framework.BV.SPA\bin\Release')
        (Join-Path $workspace 'Binary\Library\User Interface')
    )
    foreach ($uiRoot in $uiBinRoots) {
        if (-not (Test-Path $uiRoot)) { continue }
        Get-ChildItem -Path $uiRoot -Filter 'Linx*.dll' -File -ErrorAction SilentlyContinue | ForEach-Object {
            $name = $_.Name
            if (-not $byName.ContainsKey($name) -or $_.LastWriteTime -gt $byName[$name].LastWriteTime) {
                $byName[$name] = $_
            }
        }
    }

    return $byName
}

function Invoke-BuildScript {
    param([Parameter(Mandatory = $true)][string]$ScriptPath, [string]$Label)
    if (-not (Test-Path $ScriptPath)) {
        Write-Error "Build script not found: $ScriptPath"
        exit 1
    }
    Write-Host ("Building {0} via {1}" -f $Label, $ScriptPath)
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    & $ScriptPath
    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }
    Write-Host ("{0} build done in {1:n1}s" -f $Label, $sw.Elapsed.TotalSeconds)
}

function Invoke-WorkspaceBuild {
    # CI-optimized order:
    #   Tools → BV (UI sln, includes WebAPI.DS) → Application || Portal
    # Skip the standalone WebAPI.DS script — it is already built by the BV sln
    # and again by Application.sln (was a full duplicate MSBuild on t3.small).
    $toolsScript = Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\.vscode\msbuild-build.ps1'
    $bvScript = Join-Path $workspace 'User Interface\Linx.Framework.BV\.vscode\msbuild-build.ps1'
    $appScript = Join-Path $workspace 'Application\Linx.Internet.Application\.vscode\msbuild-build.ps1'
    $portalScript = Join-Path $workspace 'Application\Linx.Portal\.vscode\msbuild-build.ps1'

    $doTools = Test-ShouldBuild 'Tools'
    $doBv = Test-ShouldBuild 'Bv'
    $doApp = Test-ShouldBuild 'Application'
    $doPortal = Test-ShouldBuild 'Portal'

    # Portal-only / Application-only still need Tools if those outputs are missing.
    if ($doPortal -and -not $doTools -and -not $buildAll) {
        $toolsDll = Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll'
        $binaryTools = Join-Path $workspace 'Binary\Portal\bin\Linx.Tools.dll'
        if (-not (Test-Path $toolsDll) -and -not (Test-Path $binaryTools)) {
            Write-Host 'Linx.Tools.dll missing — adding Tools to build targets'
            $doTools = $true
        }
    }

    Write-Host ("Build targets: Tools={0} Bv={1} Application={2} Portal={3} (All={4})" -f `
        $doTools, $doBv, $doApp, $doPortal, $buildAll)

    $previousSkip = $env:SKIP_PODMAN_SYNC
    $env:SKIP_PODMAN_SYNC = '1'
    # Skip the extra BV.csproj pre-build inside the UI script (sln already builds it).
    $previousSkipBvPre = $env:SI_PDR_SKIP_BV_PREBUILD
    $env:SI_PDR_SKIP_BV_PREBUILD = '1'

    try {
        if ($doTools) { Invoke-BuildScript -ScriptPath $toolsScript -Label 'Tools' }
        if ($doBv) { Invoke-BuildScript -ScriptPath $bvScript -Label 'BV' }

        # Sequential on purpose: t3.small (~2 GiB) OOMs/thrashs when two /m MSBuild trees run together.
        if ($doApp) { Invoke-BuildScript -ScriptPath $appScript -Label 'Application' }
        if ($doPortal) { Invoke-BuildScript -ScriptPath $portalScript -Label 'Portal' }
    }
    finally {
        if ($null -eq $previousSkip) {
            Remove-Item Env:SKIP_PODMAN_SYNC -ErrorAction SilentlyContinue
        }
        else {
            $env:SKIP_PODMAN_SYNC = $previousSkip
        }
        if ($null -eq $previousSkipBvPre) {
            Remove-Item Env:SI_PDR_SKIP_BV_PREBUILD -ErrorAction SilentlyContinue
        }
        else {
            $env:SI_PDR_SKIP_BV_PREBUILD = $previousSkipBvPre
        }
    }
}

function Initialize-PublishSiteLayout {
    $layouts = @(
        @{ Site = 'Portal';      IncludeViews = $true }
        @{ Site = 'Service';     IncludeViews = $false }
        @{ Site = 'Application'; IncludeViews = $true }
    )

    foreach ($layout in $layouts) {
        $siteRoot = Join-Path $OutRoot $layout.Site
        if ($layout.IncludeViews) {
            New-Item -ItemType Directory -Force -Path (Join-Path $siteRoot 'Views') | Out-Null
        }
        New-Item -ItemType Directory -Force -Path (Join-Path $siteRoot 'bin') | Out-Null
    }
}

function Add-PublishViews {
    param(
        [string]$Site,
        [string]$ViewsRoot,
        [System.Collections.Generic.List[object]]$Manifest
    )

    if (-not (Test-Path $ViewsRoot)) {
        Write-Warning "Views folder not found for ${Site}: $ViewsRoot"
        return
    }

    Get-ChildItem -Path $ViewsRoot -Recurse -Filter '*.cshtml' -File | ForEach-Object {
        $relative = $_.FullName.Substring($ViewsRoot.Length).TrimStart('\', '/')
        $destPath = Join-Path (Join-Path (Join-Path $OutRoot $Site) 'Views') $relative
        $stackRelative = "Views/$relative"
        $entry = Copy-PublishArtifact -Source $_ -DestPath $destPath -Site $Site -Kind 'View' -Relative $stackRelative
        $Manifest.Add($entry) | Out-Null
        Write-Host "[$Site] $($entry.Relative)"
    }
}

function Add-PublishDlls {
    param(
        [string]$Site,
        [hashtable]$DllMap,
        [System.Collections.Generic.List[object]]$Manifest
    )

    foreach ($dllName in ($DllMap.Keys | Sort-Object)) {
        $source = Resolve-NewestSource -Candidates $DllMap[$dllName]
        if (-not $source) {
            Write-Warning "[$Site] Source not found for $dllName"
            continue
        }

        $destPath = Join-Path (Join-Path (Join-Path $OutRoot $Site) 'bin') $dllName
        $stackRelative = "bin/$dllName"
        $entry = Copy-PublishArtifact -Source $source -DestPath $destPath -Site $Site -Kind 'Dll' -Relative $stackRelative
        $Manifest.Add($entry) | Out-Null
        Write-Host "[$Site] $($entry.Relative)"
    }
}

if (-not $SkipBuild) {
    Write-Host 'Running full workspace build...'
    Invoke-WorkspaceBuild
}

if (Test-Path $OutRoot) {
    Remove-Item $OutRoot -Recurse -Force
}
New-Item -ItemType Directory -Force -Path $OutRoot | Out-Null
Initialize-PublishSiteLayout

Write-Host "Publish output: $OutRoot"
Write-Host "IIS reference:  $BaselineRoot"
Write-Host ''

$manifest = [System.Collections.Generic.List[object]]::new()

Write-Host '=== Portal ==='
Add-PublishViews -Site 'Portal' -ViewsRoot (Join-Path $portalProject 'Views') -Manifest $manifest
Add-PublishDlls -Site 'Portal' -DllMap $portalDllSources -Manifest $manifest

Write-Host ''
Write-Host '=== Service ==='
Add-PublishDlls -Site 'Service' -DllMap $serviceDllSources -Manifest $manifest

Write-Host ''
Write-Host '=== Application ==='
Add-PublishViews -Site 'Application' -ViewsRoot (Join-Path $applicationProject 'Views') -Manifest $manifest

$applicationDlls = Get-ApplicationDllSources
$applicationDllNames = Get-ApplicationDllNames -DiscoveredSources $applicationDlls
foreach ($dllName in $applicationDllNames) {
    $source = Resolve-ApplicationDllSource -DllName $dllName -DiscoveredSources $applicationDlls
    if (-not $source) {
        Write-Warning "[Application] Source not found for $dllName"
        continue
    }
    $destPath = Join-Path (Join-Path (Join-Path $OutRoot 'Application') 'bin') $dllName
    $stackRelative = "bin/$dllName"
    $entry = Copy-PublishArtifact -Source $source -DestPath $destPath -Site 'Application' -Kind 'Dll' -Relative $stackRelative
    $manifest.Add($entry) | Out-Null
    Write-Host "[Application] $($entry.Relative)"
}

$manifestPath = Join-Path $OutRoot 'manifest.json'
$manifestPayload = [PSCustomObject]@{
    generatedAt  = (Get-Date).ToString('o')
    outRoot      = $OutRoot
    baselineRoot = $BaselineRoot
    skipBuild    = [bool]$SkipBuild
    itemCount    = $manifest.Count
    items        = @($manifest)
}
$manifestPayload | ConvertTo-Json -Depth 5 | Set-Content -Path $manifestPath -Encoding UTF8

Write-Host ''
Write-Host "Published $($manifest.Count) file(s) to: $OutRoot"
Write-Host "Manifest: $manifestPath"

foreach ($site in @('Portal', 'Service', 'Application')) {
    $siteCount = @($manifest | Where-Object { $_.Site -eq $site }).Count
    Write-Host "  $site`: $siteCount file(s)"
}

exit 0
