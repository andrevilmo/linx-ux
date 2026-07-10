$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'podman-volume-helpers.ps1')

$appDir = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$hostOut = Get-PodmanHostOutputDir -AppDir $appDir
$vol = 'Linx.Framework.BV.WebAPI.DS-output'
$publishStaging = Join-Path $appDir 'publish-output'
$csproj = Join-Path $appDir 'Linx.Framework.BV.WebAPI.DS.csproj'
$assemblyName = 'Linx.Framework.BV.WebAPI.DS.dll'

if (-not (Test-Path $csproj)) {
    Write-Error "Project not found: $csproj"
    exit 1
}

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    Write-Warning "podman not on PATH; skipping deploy to host output '$hostOut'."
    exit 0
}

Ensure-PodmanHostOutputDir -HostOut $hostOut

$skipPublish = $env:SKIP_MSBUILD_PUBLISH -eq '1'

if (-not $skipPublish) {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    if (-not (Test-Path $vswhere)) {
        Write-Error "vswhere not found at $vswhere. Install Visual Studio or Build Tools with MSBuild."
        exit 1
    }
    $msbuild = & $vswhere -latest -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
    if (-not $msbuild) {
        Write-Error 'MSBuild not found. Install the MSBuild workload (e.g. .NET desktop build tools).'
        exit 1
    }

    if (Test-Path $publishStaging) {
        Remove-Item $publishStaging -Recurse -Force
    }
    New-Item -ItemType Directory -Force -Path $publishStaging | Out-Null

    & $msbuild $csproj `
        /t:Rebuild `
        /p:Configuration=Release `
        /v:minimal /nologo /m

    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }

    $releaseBin = Join-Path $appDir 'bin\Release'
    if (-not (Test-Path $releaseBin)) {
        Write-Error "Release build output not found under '$releaseBin'."
        exit 1
    }

    Copy-Item -Path (Join-Path $releaseBin '*') -Destination $publishStaging -Recurse -Force

    if (-not (Test-Path (Join-Path $publishStaging $assemblyName))) {
        Write-Error "Publish output looks invalid ('$assemblyName' missing under '$publishStaging')."
        exit 1
    }
}
else {
    if (-not (Test-Path (Join-Path $publishStaging $assemblyName))) {
        Write-Error "SKIP_MSBUILD_PUBLISH=1 but '$publishStaging' is missing or incomplete (no $assemblyName)."
        exit 1
    }
}

# deploy = full workspace copy; publish = Release bin output only.
$innerSh = 'mkdir -p "/out/deploy" "/out/publish"; rm -rf "/out/deploy"/* "/out/publish"/*; cp -a /src/. "/out/deploy/"; cp -a /pub/. "/out/publish/"'

& podman run --rm --pull=missing `
    -v "${hostOut}:/out" `
    -v "${appDir}:/src:ro" `
    -v "${publishStaging}:/pub:ro" `
    docker.io/library/alpine:3.19 `
    sh -c $innerSh

if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

Sync-HostOutputToNamedVolume -HostOut $hostOut -VolumeName $vol
Write-PodmanHostOutputSummary -HostOut $hostOut -VolumeName $vol -Detail "  /out/deploy = workspace; /out/publish = publish-output ('${publishStaging}')"

exit $LASTEXITCODE
