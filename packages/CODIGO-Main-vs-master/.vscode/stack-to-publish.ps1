param(
    [switch]$SkipBuild,
    [string]$OutRoot = 'C:\Linx Workspace\out\toPublish',
    [string]$BaselineRoot = $env:LINX_IIS_ROOT
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
    'Microsoft.Identity.Client.dll' = @(
        (Join-Path $portalProject 'bin\Microsoft.Identity.Client.dll')
        (Join-Path $workspace 'Application\Linx.Portal\packages\Microsoft.Identity.Client.4.54.1\lib\net461\Microsoft.Identity.Client.dll')
        (Join-Path $binaryPortalBin 'Microsoft.Identity.Client.dll')
        (Join-Path $workspace 'Binary\Library\Common\Microsoft\Identity\Microsoft.Identity.Client.dll')
    )
    'Microsoft.IdentityModel.Abstractions.dll' = @(
        (Join-Path $portalProject 'bin\Microsoft.IdentityModel.Abstractions.dll')
        (Join-Path $workspace 'Application\Linx.Portal\packages\Microsoft.IdentityModel.Abstractions.6.22.0\lib\net461\Microsoft.IdentityModel.Abstractions.dll')
        (Join-Path $binaryPortalBin 'Microsoft.IdentityModel.Abstractions.dll')
        (Join-Path $workspace 'Binary\Library\Common\Microsoft\Identity\Microsoft.IdentityModel.Abstractions.dll')
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
    # Match "Build All" task order (Tools first)
    $buildScripts = @(
        (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\.vscode\msbuild-build.ps1')
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
