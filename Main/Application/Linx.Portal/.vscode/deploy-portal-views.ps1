param(
    [string]$File,
    [string]$Dll,
    [string]$TargetPath = $env:PORTAL_DEPLOY_PATH,
    [switch]$Force,
    [switch]$SkipBinarySync,
    [switch]$SkipDlls,
    [switch]$DllsOnly
)

$ErrorActionPreference = 'Stop'

$portalRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$workspace = (Resolve-Path (Join-Path $portalRoot '..\..')).Path

$portalProject = Join-Path $portalRoot 'Linx.Portal'
$appViews = Join-Path $portalProject 'Views'
$portalProjectBin = Join-Path $portalProject 'bin'
$binaryPortal = Join-Path $workspace 'Binary\Portal'
$binaryViews = Join-Path $binaryPortal 'Views'
$binaryPortalBin = Join-Path $binaryPortal 'bin'
$binaryServiceBin = Join-Path $workspace 'Binary\Service\bin'

if (-not $TargetPath) {
    $TargetPath = 'C:\Linx Program Files\Linx Framework 6.0.0\Portal'
}

$targetBin = Join-Path $TargetPath 'bin'

if (-not (Test-Path $TargetPath)) {
    Write-Error "Running Portal folder not found: $TargetPath. Set PORTAL_DEPLOY_PATH or pass -TargetPath."
    exit 1
}

if (-not (Test-Path $targetBin)) {
    Write-Error "Running Portal bin folder not found: $targetBin"
    exit 1
}

$knownDlls = @{
    'Linx.Portal.dll' = @(
        (Join-Path $portalProjectBin 'Linx.Portal.dll')
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

function Resolve-SourcePath {
    param([string]$DllName)

    if (-not $knownDlls.ContainsKey($DllName)) {
        Write-Error "Unsupported DLL: $DllName. Supported: $($knownDlls.Keys -join ', ')"
        exit 1
    }

    $candidates = @()
    foreach ($path in $knownDlls[$DllName]) {
        if (Test-Path $path) {
            $candidates += Get-Item $path
        }
    }

    if ($candidates.Count -eq 0) {
        Write-Error "Source not found for $DllName. Build Linx.Portal first."
        exit 1
    }

    return ($candidates | Sort-Object LastWriteTime -Descending | Select-Object -First 1)
}

function Get-ViewRelativePath {
    param([string]$Path)

    if ([string]::IsNullOrWhiteSpace($Path)) {
        return $null
    }

    $full = $Path
    if (-not [System.IO.Path]::IsPathRooted($Path)) {
        $full = Join-Path $workspace $Path
    }

    if (-not (Test-Path $full)) {
        return $null
    }

    $full = (Resolve-Path $full).Path

    foreach ($root in @($appViews, $binaryViews)) {
        if ($full.StartsWith($root, [StringComparison]::OrdinalIgnoreCase)) {
            return $full.Substring($root.Length).TrimStart('\', '/')
        }
    }

    return $null
}

function Get-DllNamesFromFile {
    param([string]$Path)

    if ([string]::IsNullOrWhiteSpace($Path)) {
        return @()
    }

    $full = $Path
    if (-not [System.IO.Path]::IsPathRooted($Path)) {
        $full = Join-Path $workspace $Path
    }

    if (-not (Test-Path $full)) {
        return @()
    }

    $full = (Resolve-Path $full).Path
    $fileName = [System.IO.Path]::GetFileName($full)
    $extension = [System.IO.Path]::GetExtension($full).ToLowerInvariant()

    if ($knownDlls.ContainsKey($fileName)) {
        return @($fileName)
    }

    if ($extension -eq '.cshtml') {
        return @()
    }

    if ($extension -in @('.ps1', '.json', '.md', '.config', '.pubxml', '.csproj', '.sln')) {
        return @()
    }

    if ($full -match '\\\.vscode\\|\\tasks\.json$') {
        return @()
    }

    if ($full -match 'Linx\.Desktop\.Tools|\\LinxMail\.cs$') {
        return @('Linx.Tools.dll')
    }

    if ($full -match '\\Application\\Linx\.Portal\\Linx\.Portal\\') {
        return @('Linx.Portal.dll')
    }

    return @()
}

function Copy-ViewFile {
    param([string]$RelativePath)

    $source = Join-Path $appViews $RelativePath
    if (-not (Test-Path $source)) {
        Write-Error "Source view not found: $source"
        exit 1
    }

    if (-not $SkipBinarySync) {
        $binaryTarget = Join-Path $binaryViews $RelativePath
        $binaryDir = Split-Path $binaryTarget -Parent
        if (-not (Test-Path $binaryDir)) {
            New-Item -ItemType Directory -Force -Path $binaryDir | Out-Null
        }
        Copy-Item -Path $source -Destination $binaryTarget -Force
        Write-Host "Binary: $binaryTarget"
    }

    $deployTarget = Join-Path $TargetPath (Join-Path 'Views' $RelativePath)
    $deployDir = Split-Path $deployTarget -Parent
    if (-not (Test-Path $deployDir)) {
        New-Item -ItemType Directory -Force -Path $deployDir | Out-Null
    }
    Copy-Item -Path $source -Destination $deployTarget -Force
    Write-Host "Running Portal: $deployTarget"
    return $true
}

function Copy-DllArtifact {
    param([string]$DllName)

    $source = Resolve-SourcePath -DllName $DllName
    $dest = Join-Path $targetBin $DllName

    if (-not $Force -and (Test-Path $dest)) {
        $destItem = Get-Item $dest
        if ($source.LastWriteTime -le $destItem.LastWriteTime -and $source.Length -eq $destItem.Length) {
            Write-Host "Skipped (already up to date): $DllName"
            return $false
        }
    }

    Copy-Item -Path $source.FullName -Destination $dest -Force
    Write-Host "Running Portal: $dest"

    $pdbSource = [System.IO.Path]::ChangeExtension($source.FullName, '.pdb')
    if (Test-Path $pdbSource) {
        $pdbDest = Join-Path $targetBin ([System.IO.Path]::GetFileName($pdbSource))
        Copy-Item -Path $pdbSource -Destination $pdbDest -Force
        Write-Host "Running Portal: $pdbDest"
    }

    if (-not $SkipBinarySync) {
        if (-not (Test-Path $binaryPortalBin)) {
            New-Item -ItemType Directory -Force -Path $binaryPortalBin | Out-Null
        }
        $binaryDest = Join-Path $binaryPortalBin $DllName
        $sameBinaryPath = $false
        if (Test-Path $binaryDest) {
            $sameBinaryPath = ($source.FullName -eq (Resolve-Path $binaryDest).Path)
        }
        if (-not $sameBinaryPath) {
            Copy-Item -Path $source.FullName -Destination $binaryDest -Force
            Write-Host "Binary: $binaryDest"
            if (Test-Path $pdbSource) {
                Copy-Item -Path $pdbSource -Destination (Join-Path $binaryPortalBin ([System.IO.Path]::GetFileName($pdbSource))) -Force
            }
        }
    }

    return $true
}

function Restart-PortalAppDomain {
    $webConfig = Join-Path $TargetPath 'Web.config'
    if (-not (Test-Path $webConfig)) {
        Write-Warning "Web.config not found; skipped app reload: $webConfig"
        return
    }

    (Get-Item $webConfig).LastWriteTime = Get-Date
    Write-Host "Touched Web.config to reload Portal: $webConfig"
}

$deployViews = -not $DllsOnly
$deployDlls = -not $SkipDlls
$viewRelative = $null
$dllNames = @()
$changed = $false

if ($File) {
    $viewRelative = Get-ViewRelativePath -Path $File
    $dllNames = Get-DllNamesFromFile -Path $File

    if ($viewRelative) {
        $deployViews = $true
        $deployDlls = $false
    }
    elseif ($dllNames.Count -gt 0) {
        $deployViews = $false
        $deployDlls = $true
    }
    else {
        Write-Warning "Could not map file to a Portal view or DLL: $File"
        Write-Warning "Deploying all Portal views and DLLs (updated only) instead."
    }
}
elseif ($Dll) {
    $name = [System.IO.Path]::GetFileName($Dll)
    if (-not $knownDlls.ContainsKey($name)) {
        Write-Error "Unsupported DLL: $name"
        exit 1
    }
    $dllNames = @($name)
    $deployViews = $false
    $deployDlls = $true
}

if ($deployViews -and -not $viewRelative -and -not $File) {
    Write-Host "Deploying all Portal views..."
    Write-Host "  From: $appViews"
    Write-Host "  To:   $TargetPath\Views"

    Get-ChildItem -Path $appViews -Recurse -Filter '*.cshtml' -File | ForEach-Object {
        $relative = $_.FullName.Substring($appViews.Length).TrimStart('\', '/')
        if (Copy-ViewFile -RelativePath $relative) {
            $changed = $true
        }
    }
}
elseif ($viewRelative) {
    if (Copy-ViewFile -RelativePath $viewRelative) {
        $changed = $true
    }
}

if ($deployDlls -and $dllNames.Count -eq 0) {
    $dllNames = @('Linx.Portal.dll', 'Linx.Tools.dll', 'Linx.Resources.Localization.dll')
}

if ($deployDlls) {
    Write-Host "Deploying Portal DLLs..."
    Write-Host "  To: $targetBin"

    foreach ($dllName in $dllNames) {
        if (Copy-DllArtifact -DllName $dllName) {
            $changed = $true
        }
    }
}

if ($changed) {
    Restart-PortalAppDomain
}

Write-Host "Portal deploy finished."
exit 0
