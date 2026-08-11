<#
.SYNOPSIS
  Ensure Main\Binary\Library exists for MSBuild HintPaths without re-shipping it every CI run.

.DESCRIPTION
  Prefer a junction to the IIS Framework Library (already seeded on the host).
  Falls back to robocopy from Framework when a junction is not possible.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string] $RepoRoot,
    [string] $FrameworkRoot = $(if ($env:LINX_IIS_ROOT) { $env:LINX_IIS_ROOT } else { 'C:\Linx Program Files\Linx Framework 6.0.0' })
)

$ErrorActionPreference = 'Stop'

$binaryLibrary = Join-Path $RepoRoot 'Main\Binary\Library'
$frameworkLibrary = Join-Path $FrameworkRoot 'Library'
$markerRel = 'Business Model\Linx.Framework.Autorizacao.BM.dll'
$binaryMarker = Join-Path $binaryLibrary $markerRel
$frameworkMarker = Join-Path $frameworkLibrary $markerRel

function Write-LibLog([string] $Message) {
    Write-Host ("Ensure-BinaryLibrary: {0}" -f $Message)
}

if (Test-Path -LiteralPath $binaryMarker) {
    Write-LibLog "OK already present: $binaryMarker"
    exit 0
}

if (-not (Test-Path -LiteralPath $frameworkMarker)) {
    throw "Framework Library missing marker $frameworkMarker — run Ensure-IisSiPdr without SkipHeavySeed once (force_full_seed)."
}

$parent = Split-Path -Parent $binaryLibrary
New-Item -ItemType Directory -Force -Path $parent | Out-Null

if (Test-Path -LiteralPath $binaryLibrary) {
    # Partial/empty Library folder — fill from Framework (no junction over existing dir).
    Write-LibLog "Seeding incomplete Binary Library from Framework via robocopy"
    & robocopy $frameworkLibrary $binaryLibrary /E /XO /R:1 /W:1 /NFL /NDL /NJH /NJS /NP | Out-Null
    if ($LASTEXITCODE -ge 8) {
        throw "robocopy Framework Library -> Binary Library failed: $LASTEXITCODE"
    }
    cmd.exe /c "exit /b 0" | Out-Null
    $global:LASTEXITCODE = 0
} else {
    Write-LibLog "Creating junction: $binaryLibrary => $frameworkLibrary"
    $p = Start-Process -FilePath 'cmd.exe' -ArgumentList @('/c', 'mklink', '/J', $binaryLibrary, $frameworkLibrary) -Wait -PassThru -NoNewWindow
    if ($p.ExitCode -ne 0 -or -not (Test-Path -LiteralPath $binaryLibrary)) {
        Write-LibLog 'Junction failed; falling back to robocopy'
        New-Item -ItemType Directory -Force -Path $binaryLibrary | Out-Null
        & robocopy $frameworkLibrary $binaryLibrary /E /XO /R:1 /W:1 /NFL /NDL /NJH /NJS /NP | Out-Null
        if ($LASTEXITCODE -ge 8) {
            throw "robocopy Framework Library -> Binary Library failed: $LASTEXITCODE"
        }
        cmd.exe /c "exit /b 0" | Out-Null
        $global:LASTEXITCODE = 0
    }
}

if (-not (Test-Path -LiteralPath $binaryMarker)) {
    throw "Binary Library still missing marker after ensure: $binaryMarker"
}

Write-LibLog "Ready: $binaryMarker"
exit 0
