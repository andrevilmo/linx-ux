$ErrorActionPreference = 'Stop'
$appDir = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$vol = 'Linx.Internet.Application-output'

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    Write-Warning 'podman not on PATH; skipping sync to volume Linx.Internet.Application-output.'
    exit 0
}

& podman volume exists $vol 2>&1 | Out-Null
if ($LASTEXITCODE -ne 0) {
    & podman volume create $vol | Out-Null
}

# One line so we do not depend on a shell script file (CRLF breaks Alpine sh).
$innerSh = 'mkdir -p /out; for d in /src/*/; do [ ! -d "$d" ] && continue; name=$(basename "$d"); if [ -d "/src/$name/bin" ]; then mkdir -p "/out/$name" && cp -a "/src/$name/bin" "/out/$name/"; fi; done'

& podman run --rm --pull=missing `
    -v "${vol}:/out" `
    -v "${appDir}:/src:ro" `
    docker.io/library/alpine:3.19 `
    sh -c $innerSh

exit $LASTEXITCODE
