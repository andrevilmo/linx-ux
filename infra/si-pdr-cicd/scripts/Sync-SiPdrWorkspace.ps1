<#
.SYNOPSIS
  Merge a freshly extracted source tree into the persistent SI-PDR workspace.

.DESCRIPTION
  Preserves incremental MSBuild caches across CI runs:
  - **/obj  — compiler intermediates
  - **/bin  — project outputs (recreated only when MSBuild runs)
  - Main\Binary\Library — large reference tree (usually already on the host /
    junctioned from Framework; omitted from the CI package)

  Pass 1 mirrors the staged tree while excluding those directories from delete.
  Pass 2 refreshes Main\Binary (including site bins) without wiping Library
  when the package did not include it.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string] $StagingRoot,
    [Parameter(Mandatory = $true)][string] $WorkspaceRoot
)

$ErrorActionPreference = 'Stop'
if (-not (Test-Path -LiteralPath $StagingRoot)) {
    throw "Staging root missing: $StagingRoot"
}

New-Item -ItemType Directory -Force -Path $WorkspaceRoot | Out-Null

function Invoke-RobocopyChecked {
    param(
        [Parameter(Mandatory = $true)][string] $Source,
        [Parameter(Mandatory = $true)][string] $Dest,
        [Parameter(Mandatory = $true)][string[]] $ArgumentList,
        [string] $Label = 'robocopy'
    )
    Write-Host ("{0}: {1} -> {2} ({3})" -f $Label, $Source, $Dest, ($ArgumentList -join ' '))
    & robocopy $Source $Dest @ArgumentList | Out-Null
    $rc = $LASTEXITCODE
    if ($rc -ge 8) {
        throw "{0} failed exit={1}" -f $Label, $rc
    }
    cmd.exe /c "exit /b 0" | Out-Null
    $global:LASTEXITCODE = 0
    Write-Host ("{0} complete (exit={1})" -f $Label, $rc)
}

$quiet = @('/R:1', '/W:1', '/NFL', '/NDL', '/NJH', '/NJS', '/NP')

# Pass 1: mirror sources; keep obj/bin/Library so incremental builds + host Library survive.
Invoke-RobocopyChecked `
    -Source $StagingRoot `
    -Dest $WorkspaceRoot `
    -ArgumentList (@('/MIR', '/XD', 'obj', 'bin', 'Library') + $quiet) `
    -Label 'Workspace MIR (preserve obj/bin/Library)'

# Pass 2: refresh Binary site outputs (Portal/Service/Application bins + configs).
$stagingBinary = Join-Path $StagingRoot 'Main\Binary'
$workspaceBinary = Join-Path $WorkspaceRoot 'Main\Binary'
if (Test-Path -LiteralPath $stagingBinary) {
    New-Item -ItemType Directory -Force -Path $workspaceBinary | Out-Null
    $stagingLibrary = Join-Path $stagingBinary 'Library'
    $binArgs = @('/E', '/XO') + $quiet
    if (-not (Test-Path -LiteralPath $stagingLibrary)) {
        # Package omitted Library (~400MB); do not delete/overwrite host Library.
        $binArgs = @('/E', '/XO', '/XD', 'Library') + $quiet
        Write-Host 'Binary package has no Library — preserving workspace Main\Binary\Library'
    }
    Invoke-RobocopyChecked `
        -Source $stagingBinary `
        -Dest $workspaceBinary `
        -ArgumentList $binArgs `
        -Label 'Binary refresh'
}

Write-Host 'Workspace sync complete'
exit 0
