param(
    [string]$Dll,
    [string]$File,
    [string]$TargetPath = $env:SERVICE_DEPLOY_PATH,
    [switch]$Force,
    [switch]$SkipBinarySync
)

$ErrorActionPreference = 'Stop'

$apiRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$workspace = (Resolve-Path (Join-Path $apiRoot '..\..\..')).Path

$binaryServiceBin = Join-Path $workspace 'Binary\Service\bin'

if (-not $TargetPath) {
    $TargetPath = 'C:\Linx Program Files\Linx Framework 6.0.0\Service'
}

$targetBin = Join-Path $TargetPath 'bin'

if (-not (Test-Path $targetBin)) {
    Write-Error "Running Service bin folder not found: $targetBin. Set SERVICE_DEPLOY_PATH or pass -TargetPath."
    exit 1
}

$knownDlls = @{
    'Linx.Tools.dll' = @(
        (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll')
        (Join-Path $binaryServiceBin 'Linx.Tools.dll')
    )
    'Linx.Framework.BV.dll' = @(
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV\bin\Release\Linx.Framework.BV.dll')
        (Join-Path $binaryServiceBin 'Linx.Framework.BV.dll')
    )
    'Linx.Framework.BV.WebAPI.DS.dll' = @(
        (Join-Path $apiRoot 'bin\Release\Linx.Framework.BV.WebAPI.DS.dll')
        (Join-Path $binaryServiceBin 'Linx.Framework.BV.WebAPI.DS.dll')
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
        Write-Error "Source not found for $DllName. Build the project first."
        exit 1
    }

    return ($candidates | Sort-Object LastWriteTime -Descending | Select-Object -First 1)
}

function Get-DllNameFromFile {
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
    $fileName = [System.IO.Path]::GetFileName($full)
    $extension = [System.IO.Path]::GetExtension($full).ToLowerInvariant()

    if ($extension -in @('.ps1', '.json', '.md', '.config', '.pubxml', '.csproj', '.sln')) {
        return $null
    }

    if ($full -match '\\\.vscode\\|\\tasks\.json$') {
        return $null
    }

    if ($knownDlls.ContainsKey($fileName)) {
        return $fileName
    }

    if ($full -match 'Linx\.Desktop\.Tools|\\LinxMail\.cs$') {
        return 'Linx.Tools.dll'
    }

    if ($full -match '\\Linx\.Framework\.BV\\Linx\.Framework\.BV\\') {
        return 'Linx.Framework.BV.dll'
    }

    if ($full -match '\\Linx\.Framework\.BV\.WebAPI\.DS\\') {
        return 'Linx.Framework.BV.WebAPI.DS.dll'
    }

    return $null
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
    Write-Host "Running Service: $dest"

    $pdbSource = [System.IO.Path]::ChangeExtension($source.FullName, '.pdb')
    if (Test-Path $pdbSource) {
        $pdbDest = Join-Path $targetBin ([System.IO.Path]::GetFileName($pdbSource))
        Copy-Item -Path $pdbSource -Destination $pdbDest -Force
        Write-Host "Running Service: $pdbDest"
    }

    if (-not $SkipBinarySync) {
        if (-not (Test-Path $binaryServiceBin)) {
            New-Item -ItemType Directory -Force -Path $binaryServiceBin | Out-Null
        }
        $binaryDest = Join-Path $binaryServiceBin $DllName
        $sameBinaryPath = $false
        if (Test-Path $binaryDest) {
            $sameBinaryPath = ($source.FullName -eq (Resolve-Path $binaryDest).Path)
        }
        if (-not $sameBinaryPath) {
            Copy-Item -Path $source.FullName -Destination $binaryDest -Force
            Write-Host "Binary: $binaryDest"
            if (Test-Path $pdbSource) {
                Copy-Item -Path $pdbSource -Destination (Join-Path $binaryServiceBin ([System.IO.Path]::GetFileName($pdbSource))) -Force
            }
        }
    }

    return $true
}

function Restart-ServiceAppDomain {
    $webConfig = Join-Path $TargetPath 'Web.config'
    if (-not (Test-Path $webConfig)) {
        Write-Warning "Web.config not found; skipped app reload: $webConfig"
        return
    }

    (Get-Item $webConfig).LastWriteTime = Get-Date
    Write-Host "Touched Web.config to reload Service: $webConfig"
}

$dllNames = @()

if ($File) {
    $fromFile = Get-DllNameFromFile -Path $File
    if ($fromFile) {
        $dllNames += $fromFile
    }
    else {
        Write-Warning "Could not map file to a Service DLL: $File"
        Write-Warning "Deploying all known Service DLLs (updated only) instead."
        $dllNames = @('Linx.Tools.dll', 'Linx.Framework.BV.dll', 'Linx.Framework.BV.WebAPI.DS.dll')
    }
}
elseif ($Dll) {
    $name = [System.IO.Path]::GetFileName($Dll)
    if (-not $knownDlls.ContainsKey($name)) {
        Write-Error "Unsupported DLL: $name"
        exit 1
    }
    $dllNames += $name
}
else {
    $dllNames = @('Linx.Tools.dll', 'Linx.Framework.BV.dll', 'Linx.Framework.BV.WebAPI.DS.dll')
}

Write-Host "Deploying Service DLLs..."
Write-Host "  To: $targetBin"

$deployed = $false
foreach ($dllName in $dllNames) {
    if (Copy-DllArtifact -DllName $dllName) {
        $deployed = $true
    }
}

if ($deployed) {
    Restart-ServiceAppDomain
}

Write-Host "Service DLL deploy finished."
exit 0
