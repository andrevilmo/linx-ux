param(
    [switch]$SkipBuild,
    [switch]$Force,
    [switch]$ResetBaseline,
    [string]$BaselineRoot = $env:LINX_IIS_ROOT,
    [string]$OutRoot = 'C:\Linx Workspace\out\toDeploy'
)

$ErrorActionPreference = 'Stop'

$workspace = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$stackStateRoot = Join-Path (Split-Path $OutRoot -Parent) '.stack-state'
$stackBaselinePath = Join-Path $stackStateRoot 'baseline.json'

if (-not $BaselineRoot) {
    $BaselineRoot = 'C:\Linx Program Files\Linx Framework 6.0.0'
}

$portalProject = Join-Path $workspace 'Application\Linx.Portal\Linx.Portal'
$applicationProject = Join-Path $workspace 'Application\Linx.Internet.Application\Linx.Internet.Application'
$apiRoot = Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI.DS'
$bvRoot = Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV'

$binaryPortalBin = Join-Path $workspace 'Binary\Portal\bin'
$binaryPortalViews = Join-Path $workspace 'Binary\Portal\Views'
$binaryServiceBin = Join-Path $workspace 'Binary\Service\bin'
$binaryApplicationBin = Join-Path $workspace 'Binary\Application\bin'
$binaryApplicationViews = Join-Path $workspace 'Binary\Application\Views'
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

$script:stackBaseline = @{}
$script:scannedHashes = @{}

function Get-FileSha256 {
    param([string]$Path)
    return (Get-FileHash -Path $Path -Algorithm SHA256).Hash
}

function Get-StackArtifactKey {
    param(
        [string]$Site,
        [string]$Kind,
        [string]$Relative
    )

    $normalized = $Relative -replace '\\', '/'
    return "$Site|$Kind|$normalized"
}

function Load-StackBaseline {
    if ($ResetBaseline -and (Test-Path $stackBaselinePath)) {
        Remove-Item $stackBaselinePath -Force
        Write-Host 'Reset stack baseline.'
    }

    if (-not (Test-Path $stackBaselinePath)) {
        return
    }

    $payload = Get-Content -Path $stackBaselinePath -Raw | ConvertFrom-Json
    if ($payload.artifacts) {
        foreach ($property in $payload.artifacts.PSObject.Properties) {
            $script:stackBaseline[$property.Name] = $property.Value
        }
    }
}

function Save-StackBaseline {
    if (-not (Test-Path $stackStateRoot)) {
        New-Item -ItemType Directory -Force -Path $stackStateRoot | Out-Null
    }

    foreach ($key in $script:scannedHashes.Keys) {
        $script:stackBaseline[$key] = $script:scannedHashes[$key]
    }

    $artifactObject = [ordered]@{}
    foreach ($key in ($script:stackBaseline.Keys | Sort-Object)) {
        $artifactObject[$key] = $script:stackBaseline[$key]
    }

    $payload = [PSCustomObject]@{
        version   = 1
        updatedAt = (Get-Date).ToString('o')
        artifacts = [PSCustomObject]$artifactObject
    }

    $payload | ConvertTo-Json -Depth 3 | Set-Content -Path $stackBaselinePath -Encoding UTF8
}

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

function Test-StackArtifactChanged {
    param(
        [string]$Key,
        [string]$SourcePath,
        [string]$FallbackBaselinePath,
        [switch]$Force
    )

    $hash = Get-FileSha256 -Path $SourcePath
    $script:scannedHashes[$Key] = $hash

    if ($Force) {
        return $true
    }

    if ($Key -match '\|View\|') {
        if (-not $script:stackBaseline.ContainsKey($Key)) {
            return $true
        }
        return $script:stackBaseline[$Key] -ne $hash
    }

    if ($script:stackBaseline.ContainsKey($Key)) {
        return $script:stackBaseline[$Key] -ne $hash
    }

    if ($FallbackBaselinePath -and (Test-Path $FallbackBaselinePath)) {
        $fallbackHash = Get-FileSha256 -Path $FallbackBaselinePath
        return $hash -ne $fallbackHash
    }

    return $true
}

function Copy-DeployArtifact {
    param(
        [System.IO.FileInfo]$Source,
        [string]$DestPath,
        [string]$Site,
        [string]$Kind,
        [string]$Relative,
        [string]$FallbackBaselinePath,
        [switch]$Force
    )

    $key = Get-StackArtifactKey -Site $Site -Kind $Kind -Relative $Relative

    if (-not (Test-StackArtifactChanged -Key $key -SourcePath $Source.FullName -FallbackBaselinePath $FallbackBaselinePath -Force:$Force)) {
        return $null
    }

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
        Relative = $DestPath.Substring($OutRoot.Length).TrimStart('\', '/')
        Source   = $Source.FullName
        Baseline = $FallbackBaselinePath
        Key      = $key
    }
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

    $uiRoot = Join-Path $workspace 'User Interface'
    if (Test-Path $uiRoot) {
        Get-ChildItem -Path $uiRoot -Recurse -Filter 'Linx*.dll' -File -ErrorAction SilentlyContinue |
            Where-Object { $_.FullName -match '\\bin\\(Release|Debug)\\' } |
            ForEach-Object {
                $name = $_.Name
                if (-not $byName.ContainsKey($name) -or $_.LastWriteTime -gt $byName[$name].LastWriteTime) {
                    $byName[$name] = $_
                }
            }
    }

    return $byName
}

function Invoke-WorkspaceBuild {
    $buildScripts = @(
        (Join-Path $workspace 'User Interface\Linx.Framework.BV\.vscode\msbuild-build.ps1')
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI.DS\.vscode\msbuild-build.ps1')
        (Join-Path $workspace 'Application\Linx.Internet.Application\.vscode\msbuild-build.ps1')
        (Join-Path $workspace 'Application\Linx.Portal\.vscode\msbuild-build.ps1')
    )

    $previousSkip = $env:SKIP_PODMAN_SYNC
    $env:SKIP_PODMAN_SYNC = '1'

    try {
        foreach ($script in $buildScripts) {
            if (-not (Test-Path $script)) {
                Write-Error "Build script not found: $script"
            }

            Write-Host "Building via $script"
            & $script
            if ($LASTEXITCODE -ne 0) {
                exit $LASTEXITCODE
            }
        }
    }
    finally {
        if ($null -eq $previousSkip) {
            Remove-Item Env:SKIP_PODMAN_SYNC -ErrorAction SilentlyContinue
        }
        else {
            $env:SKIP_PODMAN_SYNC = $previousSkip
        }
    }
}

function Initialize-DeploySiteLayout {
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

function Add-StackViews {
    param(
        [string]$Site,
        [string]$ViewsRoot,
        [string]$BinaryViewsRoot,
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
        $fallbackBaseline = if ($BinaryViewsRoot) { Join-Path $BinaryViewsRoot $relative } else { $null }
        $entry = Copy-DeployArtifact -Source $_ -DestPath $destPath -Site $Site -Kind 'View' -Relative $stackRelative -FallbackBaselinePath $fallbackBaseline -Force:$Force
        if ($entry) {
            $Manifest.Add($entry) | Out-Null
            Write-Host "[$Site] $($entry.Relative)"
        }
    }
}

function Add-StackDlls {
    param(
        [string]$Site,
        [hashtable]$DllMap,
        [System.Collections.Generic.List[object]]$Manifest
    )

    foreach ($dllName in $DllMap.Keys) {
        $source = Resolve-NewestSource -Candidates $DllMap[$dllName]
        if (-not $source) {
            Write-Warning "[$Site] Source not found for $dllName"
            continue
        }

        $destPath = Join-Path (Join-Path (Join-Path $OutRoot $Site) 'bin') $dllName
        $stackRelative = "bin/$dllName"
        $fallbackBaseline = Join-Path (Join-Path (Join-Path $BaselineRoot $Site) 'bin') $dllName
        $entry = Copy-DeployArtifact -Source $source -DestPath $destPath -Site $Site -Kind 'Dll' -Relative $stackRelative -FallbackBaselinePath $fallbackBaseline -Force:$Force
        if ($entry) {
            $Manifest.Add($entry) | Out-Null
            Write-Host "[$Site] $($entry.Relative)"
        }
    }
}

Load-StackBaseline

if (-not $SkipBuild) {
    Write-Host 'Running full workspace build...'
    Invoke-WorkspaceBuild
}

if (Test-Path $OutRoot) {
    Remove-Item $OutRoot -Recurse -Force
}
New-Item -ItemType Directory -Force -Path $OutRoot | Out-Null
Initialize-DeploySiteLayout

Write-Host "IIS reference root: $BaselineRoot"
Write-Host "Stack baseline:     $stackBaselinePath"
Write-Host "Stack output:       $OutRoot"
Write-Host ''

$manifest = [System.Collections.Generic.List[object]]::new()

Write-Host '=== Portal ==='
Add-StackViews -Site 'Portal' -ViewsRoot (Join-Path $portalProject 'Views') -BinaryViewsRoot $binaryPortalViews -Manifest $manifest
Add-StackDlls -Site 'Portal' -DllMap $portalDllSources -Manifest $manifest

Write-Host ''
Write-Host '=== Service ==='
Add-StackDlls -Site 'Service' -DllMap $serviceDllSources -Manifest $manifest

Write-Host ''
Write-Host '=== Application ==='
Add-StackViews -Site 'Application' -ViewsRoot (Join-Path $applicationProject 'Views') -BinaryViewsRoot $binaryApplicationViews -Manifest $manifest

$applicationDlls = Get-ApplicationDllSources
$applicationBaselineBin = Join-Path (Join-Path $BaselineRoot 'Application') 'bin'
$applicationDllNames = @()

if (Test-Path $applicationBaselineBin) {
    $applicationDllNames = Get-ChildItem -Path $applicationBaselineBin -Filter 'Linx*.dll' -File |
        Select-Object -ExpandProperty Name
}
else {
    Write-Warning "Application IIS bin not found: $applicationBaselineBin. Stacking all discovered Linx*.dll sources."
    $applicationDllNames = @($applicationDlls.Keys)
}

foreach ($dllName in ($applicationDllNames | Sort-Object)) {
    if (-not $applicationDlls.ContainsKey($dllName)) {
        continue
    }

    $source = $applicationDlls[$dllName]
    $destPath = Join-Path (Join-Path (Join-Path $OutRoot 'Application') 'bin') $dllName
    $stackRelative = "bin/$dllName"
    $fallbackBaseline = Join-Path $applicationBaselineBin $dllName
    $entry = Copy-DeployArtifact -Source $source -DestPath $destPath -Site 'Application' -Kind 'Dll' -Relative $stackRelative -FallbackBaselinePath $fallbackBaseline -Force:$Force
    if ($entry) {
        $manifest.Add($entry) | Out-Null
        Write-Host "[Application] $($entry.Relative)"
    }
}

Save-StackBaseline

$manifestPath = Join-Path $OutRoot 'manifest.json'
$manifestPayload = [PSCustomObject]@{
    generatedAt      = (Get-Date).ToString('o')
    baselineRoot     = $BaselineRoot
    stackBaseline    = $stackBaselinePath
    force            = [bool]$Force
    resetBaseline    = [bool]$ResetBaseline
    skipBuild        = [bool]$SkipBuild
    itemCount        = $manifest.Count
    items            = @($manifest)
}
$manifestPayload | ConvertTo-Json -Depth 5 | Set-Content -Path $manifestPath -Encoding UTF8

Write-Host ''
if ($manifest.Count -eq 0) {
    Write-Host 'No changed deployable files found (DLLs or views).'
    Write-Host 'Tip: use -ResetBaseline to treat all sources as new, or -Force to stack everything.'
}
else {
    Write-Host "Stacked $($manifest.Count) changed file(s) to: $OutRoot"
    Write-Host "Manifest: $manifestPath"
}

foreach ($site in @('Portal', 'Service', 'Application')) {
    $siteCount = @($manifest | Where-Object { $_.Site -eq $site }).Count
    Write-Host "  $site`: $siteCount file(s)"
}

exit 0
