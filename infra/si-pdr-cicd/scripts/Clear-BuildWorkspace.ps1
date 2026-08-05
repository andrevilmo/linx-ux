<#
.SYNOPSIS
  Frees disk by removing old SI-PDR CI workdirs under C:\lx\
#>
[CmdletBinding()]
param(
    [string] $KeepRunId = '',
    [int] $KeepCount = 0
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

$runDirs = @(Get-ChildItem -LiteralPath 'C:\lx' -Directory -Force -ErrorAction SilentlyContinue |
    Where-Object { $_.Name -match '^\d+$' } |
    Sort-Object LastWriteTime -Descending)

$extra = 0
foreach ($dir in $runDirs) {
    if ($keep.Contains($dir.Name)) { continue }
    if ($extra -lt $KeepCount) {
        [void]$keep.Add($dir.Name)
        $extra++
    }
}

foreach ($dir in $runDirs) {
    if ($keep.Contains($dir.Name)) {
        Write-CleanLog "Keeping $($dir.FullName)"
        continue
    }
    Remove-PathSafe $dir.FullName
}

Get-ChildItem -LiteralPath 'C:\lx' -File -Force -ErrorAction SilentlyContinue |
    Where-Object { $_.Extension -in '.zip', '.log' } |
    ForEach-Object {
        if ($keep.Contains($_.BaseName) -and $_.Extension -eq '.log') {
            Write-CleanLog "Keeping $($_.FullName)"
            return
        }
        Remove-PathSafe $_.FullName
    }

foreach ($id in $keep) {
    $runRoot = Join-Path 'C:\lx' $id
    if (-not (Test-Path -LiteralPath $runRoot)) { continue }
    Get-ChildItem -LiteralPath $runRoot -Directory -Recurse -Force -ErrorAction SilentlyContinue |
        Where-Object { $_.Name -eq 'obj' } |
        ForEach-Object { Remove-PathSafe $_.FullName }
}

$drive = Get-PSDrive -Name C
Write-CleanLog ("Disk after: free={0:n1} MB used={1:n1} MB" -f ($drive.Free / 1MB), ($drive.Used / 1MB))
exit 0
