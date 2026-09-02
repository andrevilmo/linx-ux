<#
.SYNOPSIS
  Merge a freshly extracted source tree into the persistent SI-PDR workspace.

.DESCRIPTION
  Preserves **/obj directories under the destination so MSBuild can do incremental
  compiles across CI runs. Default is robocopy /MIR so deleted source files are
  removed from the workspace, while /XD obj keeps compile caches. Pass -NoMirror
  on skip_build so a lightweight package does not delete previously compiled bins.
#>
[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)][string] $StagingRoot,
    [Parameter(Mandatory = $true)][string] $WorkspaceRoot,
    # skip_build packages only Binary + Portal/App sources. /MIR would delete
    # previously compiled bins (Portal.dll, BV, etc.) from the persistent workspace.
    [switch] $NoMirror
)

$ErrorActionPreference = 'Stop'
if (-not (Test-Path -LiteralPath $StagingRoot)) {
    throw "Staging root missing: $StagingRoot"
}

New-Item -ItemType Directory -Force -Path $WorkspaceRoot | Out-Null

$copyMode = if ($NoMirror) { '/E' } else { '/MIR' }
Write-Host ("Syncing workspace (preserve obj, mode={0}): {1} -> {2}" -f $copyMode, $StagingRoot, $WorkspaceRoot)
# /MIR mirror (full package) or /E copy (skip_build); /XD obj keep incremental MSBuild caches
& robocopy $StagingRoot $WorkspaceRoot $copyMode /XD obj /R:1 /W:1 /NFL /NDL /NJH /NJS /NP | Out-Null
$rc = $LASTEXITCODE
# robocopy 0-7 = success
if ($rc -ge 8) {
    throw "robocopy workspace sync failed exit=$rc"
}
cmd.exe /c "exit /b 0" | Out-Null
$global:LASTEXITCODE = 0
Write-Host ("Workspace sync complete (robocopy exit={0})" -f $rc)
exit 0
