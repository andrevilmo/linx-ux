function Get-PodmanHostOutputDir {
    param(
        [Parameter(Mandatory = $true)]
        [string] $AppDir
    )

    return Join-Path $AppDir 'podman-volume-output'
}

function Ensure-PodmanHostOutputDir {
    param(
        [Parameter(Mandatory = $true)]
        [string] $HostOut
    )

    New-Item -ItemType Directory -Force -Path $HostOut | Out-Null
}

function Sync-HostOutputToNamedVolume {
    param(
        [Parameter(Mandatory = $true)]
        [string] $HostOut,

        [Parameter(Mandatory = $false)]
        [string] $VolumeName = 'Linx.Portal-output'
    )

    if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
        return
    }

    & podman volume exists $VolumeName 2>&1 | Out-Null
    if ($LASTEXITCODE -ne 0) {
        & podman volume create $VolumeName | Out-Null
    }

    # tar tolerates missing symlinks better than cp -a on mixed Windows/Linux trees.
    $innerSh = 'cd /from && tar cf - . 2>/dev/null | (cd /to && tar xf - 2>/dev/null); exit 0'

    & podman run --rm --pull=missing `
        -v "${HostOut}:/from:ro" `
        -v "${VolumeName}:/to" `
        docker.io/library/alpine:3.19 `
        sh -c $innerSh
}

function Write-PodmanHostOutputSummary {
    param(
        [Parameter(Mandatory = $true)]
        [string] $HostOut,

        [Parameter(Mandatory = $false)]
        [string] $VolumeName = 'Linx.Portal-output',

        [Parameter(Mandatory = $false)]
        [string] $Detail = ''
    )

    Write-Host "Host output: '${HostOut}'"
    if ($Detail) {
        Write-Host $Detail
    }
    Write-Host "Named Podman volume (for containers): '${VolumeName}'"
}
