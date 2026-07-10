$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'podman-volume-helpers.ps1')

$appDir = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$hostOut = Get-PodmanHostOutputDir -AppDir $appDir
$vol = 'Linx.Internet.Application-output'

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    Write-Warning "podman not on PATH; skipping sync to host output '$hostOut'."
    exit 0
}

Ensure-PodmanHostOutputDir -HostOut $hostOut

# One line so we do not depend on a shell script file (CRLF breaks Alpine sh).
$innerSh = 'mkdir -p /out; for d in /src/*/; do [ ! -d "$d" ] && continue; name=$(basename "$d"); if [ -d "/src/$name/bin" ]; then mkdir -p "/out/$name" && cp -a "/src/$name/bin" "/out/$name/"; fi; done'

& podman run --rm --pull=missing `
    -v "${hostOut}:/out" `
    -v "${appDir}:/src:ro" `
    docker.io/library/alpine:3.19 `
    sh -c $innerSh

if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

Sync-HostOutputToNamedVolume -HostOut $hostOut -VolumeName $vol
Write-PodmanHostOutputSummary -HostOut $hostOut -VolumeName $vol -Detail "  /out/<project>/bin = Release bin folders from Application child projects"

exit $LASTEXITCODE
