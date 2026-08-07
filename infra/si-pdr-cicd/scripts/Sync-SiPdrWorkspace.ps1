<#
.SYNOPSIS
  Merge a freshly extracted source tree into the persistent SI-PDR workspace.

.DESCRIPTION
  Preserves **/obj directories under the destination so MSBuild can do incremental
  compiles across CI runs. Uses robocopy /MIR so deleted source files are
  removed from the workspace, while /XD obj keeps compile caches.
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

Write-Host ("Syncing workspace (preserve obj): {0} -> {1}" -f $StagingRoot, $WorkspaceRoot)
# /MIR mirror; /XD obj keep incremental MSBuild caches; quiet flags for SSM log size
& robocopy $StagingRoot $WorkspaceRoot /MIR /XD obj /R:1 /W:1 /NFL /NDL /NJH /NJS /NP | Out-Null
$rc = $LASTEXITCODE
# robocopy 0-7 = success
if ($rc -ge 8) {
    throw "robocopy workspace sync failed exit=$rc"
}
cmd.exe /c "exit /b 0" | Out-Null
$global:LASTEXITCODE = 0
Write-Host ("Workspace sync complete (robocopy exit={0})" -f $rc)
exit 0
