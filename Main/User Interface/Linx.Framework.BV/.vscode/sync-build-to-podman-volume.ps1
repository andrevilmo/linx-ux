$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'podman-volume-helpers.ps1')

$appDir = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$businessBvDir = (Resolve-Path (Join-Path $appDir '..\..\Business\Linx.Framework.BV')).Path
$hostOut = Get-PodmanHostOutputDir -AppDir $appDir
$vol = 'Linx.Framework.BV-output'

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    Write-Warning "podman not on PATH; skipping sync to host output '$hostOut'."
    exit 0
}

Ensure-PodmanHostOutputDir -HostOut $hostOut

# Paths with spaces are passed via env vars; mount points avoid spaces in sh -c.
$innerSh = 'mkdir -p /out/build; copy_bins() { root="$1"; prefix="$2"; find "$root" -type d -name bin ! -path "*/node_modules/*" ! -path "*/obj/*" 2>/dev/null | while read -r b; do rel="${b#${root}/}"; dest="/out/build/${prefix}/${rel}"; dest_parent="${dest%/*}"; mkdir -p "$dest_parent"; rm -rf "$dest"; cp -a "$b" "${dest_parent}/"; done; }; copy_bins /src-ui "$UI_PREFIX"; copy_bins /src-biz "$BIZ_PREFIX"'

$podmanArgs = @(
    'run', '--rm', '--pull=missing',
    '-e', 'UI_PREFIX=User Interface/Linx.Framework.BV',
    '-e', 'BIZ_PREFIX=Business/Linx.Framework.BV',
    '-v', "${hostOut}:/out",
    '-v', "${appDir}:/src-ui:ro",
    '-v', "${businessBvDir}:/src-biz:ro",
    'docker.io/library/alpine:3.19',
    'sh', '-c', $innerSh
)
& podman @podmanArgs

if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

Sync-HostOutputToNamedVolume -HostOut $hostOut -VolumeName $vol
Write-PodmanHostOutputSummary -HostOut $hostOut -VolumeName $vol -Detail "  /out/build = Release bin folders from BV projects"

exit $LASTEXITCODE
