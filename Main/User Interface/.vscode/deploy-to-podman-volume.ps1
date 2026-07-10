$ErrorActionPreference = 'Stop'

$appDir = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$vol = 'Linx.Internet.Application-output'
$publishStaging = Join-Path $appDir 'publish-output'
$csproj = Join-Path $appDir 'Linx.Internet.Application\Linx.Internet.Application.csproj'

if (-not (Test-Path $csproj)) {
    Write-Error "Web project not found: $csproj"
    exit 1
}

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    Write-Warning "podman not on PATH; skipping deploy to volume '$vol'."
    exit 0
}

& podman volume exists $vol 2>&1 | Out-Null
if ($LASTEXITCODE -ne 0) {
    & podman volume create $vol | Out-Null
}

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

    # FileSystem publish: only published site files under publish-output (not full repo).
    & $msbuild $csproj `
        /t:Rebuild `
        /p:Configuration=Release `
        /p:DeployOnBuild=true `
        /p:PublishProfile=LIAShell-Release `
        /p:publishUrl="$publishStaging" `
        /p:DeleteExistingFiles=true `
        /p:LaunchSiteAfterPublish=false `
        /v:minimal /nologo /m

    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }

    if (-not (Test-Path (Join-Path $publishStaging 'Web.config'))) {
        Write-Error "Publish output looks invalid (Web.config missing under '$publishStaging')."
        exit 1
    }
}
else {
    if (-not (Test-Path (Join-Path $publishStaging 'Web.config'))) {
        Write-Error "SKIP_MSBUILD_PUBLISH=1 but '$publishStaging' is missing or incomplete (no Web.config)."
        exit 1
    }
}

# deploy = full workspace copy; publish = MSBuild publish output only.
$innerSh = 'mkdir -p "/out/deploy" "/out/publish"; rm -rf "/out/deploy"/* "/out/publish"/*; cp -a /src/. "/out/deploy/"; cp -a /pub/. "/out/publish/"'

& podman run --rm --pull=missing `
    -v "${vol}:/out" `
    -v "${appDir}:/src:ro" `
    -v "${publishStaging}:/pub:ro" `
    docker.io/library/alpine:3.19 `
    sh -c $innerSh

if ($LASTEXITCODE -eq 0) {
    Write-Host "Volume '${vol}': '/out/deploy' = workspace; '/out/publish' = publish-output only ('${publishStaging}')."
}

exit $LASTEXITCODE
