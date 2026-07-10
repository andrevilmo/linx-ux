# Kept for compatibility: host output is written directly by build/deploy scripts.
# This script re-syncs the named Podman volume into podman-volume-output if needed.
param(
    [Parameter(Mandatory = $false)]
    [string] $VolumeName = 'Linx.Internet.Application-output',

    [Parameter(Mandatory = $false)]
    [string] $HostDir = ''
)

$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'podman-volume-helpers.ps1')

if (-not $HostDir) {
    $appDir = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
    $HostDir = Get-PodmanHostOutputDir -AppDir $appDir
}

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    Write-Warning "podman not on PATH; skipping expose of volume '$VolumeName'."
    exit 0
}

& podman volume exists $VolumeName 2>&1 | Out-Null
if ($LASTEXITCODE -ne 0) {
    Write-Warning "Volume '$VolumeName' does not exist; nothing to expose."
    exit 0
}

Ensure-PodmanHostOutputDir -HostOut $HostDir

$innerSh = 'cd /from && tar cf - . 2>/dev/null | (cd /to && tar xf - 2>/dev/null); exit 0'

& podman run --rm --pull=missing `
    -v "${VolumeName}:/from:ro" `
    -v "${HostDir}:/to" `
    docker.io/library/alpine:3.19 `
    sh -c $innerSh

if ($LASTEXITCODE -eq 0) {
    Write-PodmanHostOutputSummary -HostOut $HostDir -VolumeName $VolumeName
}

exit $LASTEXITCODE
