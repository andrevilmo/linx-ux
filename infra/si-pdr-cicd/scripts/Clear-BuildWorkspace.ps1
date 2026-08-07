<#
.SYNOPSIS
  Frees disk by removing old SI-PDR CI workdirs under C:\lx\

.NOTES
  Persistent workspace C:\lx\si-pdr (and its obj/ caches) is always kept so
  MSBuild can stay incremental across runs.
#>
[CmdletBinding()]
param(
    [string] $KeepRunId = '',
    [int] $KeepCount = 0,
    [string] $PersistentWorkspace = 'C:\lx\si-pdr',
    [switch] $PurgeObjCaches
)

$ErrorActionPreference = 'Continue'

function Write-CleanLog([string] $Message) { Write-Output $Message }

function Get-PathSizeBytes([string] $Path) {
    if (-not (Test-Path -LiteralPath $Path)) { return [int64]0 }
    $item = Get-Item -LiteralPath $Path -Force
    if (-not $item.PSIsContainer) { return [int64]$item.Length }
    return [int64]((Get-ChildItem -LiteralPath $Path -Recurse -Force -ErrorAction SilentlyContinue |
            Measure-Object -Property Length -Sum).Sum)
}

function Remove-PathSafe([string] $Path) {
    if (-not (Test-Path -LiteralPath $Path)) { return }
    $size = Get-PathSizeBytes $Path
    try {
        Remove-Item -LiteralPath $Path -Recurse -Force -ErrorAction Stop
        Write-CleanLog ("Removed {0:n1} MB  {1}" -f ($size / 1MB), $Path)
    } catch {
        Write-CleanLog "WARN could not remove ${Path}: $($_.Exception.Message)"
    }
}

$drive = Get-PSDrive -Name C
Write-CleanLog ("Disk before: free={0:n1} MB used={1:n1} MB" -f ($drive.Free / 1MB), ($drive.Used / 1MB))

if (-not (Test-Path -LiteralPath 'C:\lx')) {
    New-Item -ItemType Directory -Force -Path 'C:\lx' | Out-Null
    exit 0
}

$keep = New-Object 'System.Collections.Generic.HashSet[string]'
if ($KeepRunId) { [void]$keep.Add($KeepRunId) }
# Always preserve the persistent incremental workspace directory name
[void]$keep.Add('si-pdr')
[void]$keep.Add('si-pdr-staging')

$runDirs = @(Get-ChildItem -LiteralPath 'C:\lx' -Directory -Force -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -match '^\d+$' -or $_.Name -in @('si-pdr', 'si-pdr-staging') } |
    Sort-Object LastWriteTime -Descending)

$extra = 0
foreach ($dir in $runDirs) {
    if ($keep.Contains($dir.Name)) { continue }
    if ($dir.Name -match '^\d+$' -and $extra -lt $KeepCount) {
        [void]$keep.Add($dir.Name)
        $extra++
    }
}

foreach ($dir in $runDirs) {
    if ($dir.Name -eq 'si-pdr') {
        Write-CleanLog "Keeping persistent workspace $($dir.FullName)"
        continue
    }
    if ($dir.Name -eq 'si-pdr-staging') {
        # Staging is ephemeral; always remove after a run
        Remove-PathSafe $dir.FullName
        continue
    }
    if ($keep.Contains($dir.Name)) {
        Write-CleanLog "Keeping $($dir.FullName)"
        continue
    }
    Remove-PathSafe $dir.FullName
}

Get-ChildItem -LiteralPath 'C:\lx' -File -Force -ErrorAction SilentlyContinue |
    Where-Object { $_.Extension -in '.zip', '.log', '.gz' -or $_.Name -like '*.tar.gz' } |
    ForEach-Object {
        if ($keep.Contains($_.BaseName) -and $_.Extension -eq '.log') {
            Write-CleanLog "Keeping $($_.FullName)"
            return
        }
        # Keep a small rolling set of logs for the persistent workspace marker
        if ($_.Name -eq 'si-pdr.log') {
            Write-CleanLog "Keeping $($_.FullName)"
            return
        }
        Remove-PathSafe $_.FullName
    }

# Only purge obj caches when explicitly requested (breaks incremental MSBuild).
if ($PurgeObjCaches -and (Test-Path -LiteralPath $PersistentWorkspace)) {
    Get-ChildItem -LiteralPath $PersistentWorkspace -Directory -Recurse -Force -ErrorAction SilentlyContinue |
        Where-Object { $_.Name -eq 'obj' } |
        ForEach-Object { Remove-PathSafe $_.FullName }
}

$drive = Get-PSDrive -Name C
Write-CleanLog ("Disk after: free={0:n1} MB used={1:n1} MB" -f ($drive.Free / 1MB), ($drive.Used / 1MB))
exit 0
