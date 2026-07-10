$ErrorActionPreference = 'Stop'

. (Join-Path $PSScriptRoot 'podman-volume-helpers.ps1')

$appDir = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$hostOut = Get-PodmanHostOutputDir -AppDir $appDir
$vol = 'Linx.Framework.BV-output'
$publishStaging = Join-Path $appDir 'publish-output'
$csproj = Join-Path $appDir 'Linx.Framework.BV.SPA\Linx.Framework.BV.SPA.csproj'

if (-not (Test-Path $csproj)) {
    Write-Error "Web project not found: $csproj"
    exit 1
}

if (-not (Get-Command podman -ErrorAction SilentlyContinue)) {
    Write-Warning "podman not on PATH; skipping deploy to host output '$hostOut'."
    exit 0
}

Ensure-PodmanHostOutputDir -HostOut $hostOut

function Test-PublishHasWebConfig([string]$dir) {
    return (Test-Path (Join-Path $dir 'Web.config')) -or (Test-Path (Join-Path $dir 'web.config'))
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

    & $msbuild $csproj `
        /t:Rebuild `
        /p:Configuration=Release `
        /p:DeployOnBuild=true `
        /p:WebPublishMethod=FileSystem `
        /p:publishUrl="$publishStaging" `
        /p:DeleteExistingFiles=true `
        /p:LaunchSiteAfterPublish=false `
        /v:minimal /nologo /m

    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }

    $hasWebConfig = Test-PublishHasWebConfig $publishStaging
    if (-not $hasWebConfig) {
        # DeployOnBuild + FileSystem often still packages to obj\...\Package\PackageTmp only.
        $packageTmp = Join-Path $appDir 'Linx.Framework.BV.SPA\obj\Release\Package\PackageTmp'
        if (Test-PublishHasWebConfig $packageTmp) {
            Write-Host "Using MSBuild PackageTmp -> publish-output: $packageTmp"
            Copy-Item -Path (Join-Path $packageTmp '*') -Destination $publishStaging -Recurse -Force
            $hasWebConfig = Test-PublishHasWebConfig $publishStaging
        }
    }

    if (-not $hasWebConfig) {
        Write-Error "Publish output looks invalid (Web.config / web.config missing under '$publishStaging'). Check Linx.Framework.BV.SPA\obj\Release\Package\PackageTmp after build."
        exit 1
    }
}
else {
    if (-not (Test-PublishHasWebConfig $publishStaging)) {
        Write-Error "SKIP_MSBUILD_PUBLISH=1 but '$publishStaging' is missing or incomplete (no Web.config)."
        exit 1
    }
}

$innerSh = 'mkdir -p "/out/deploy" "/out/publish"; rm -rf "/out/deploy"/* "/out/publish"/*; cp -a /src/. "/out/deploy/"; cp -a /pub/. "/out/publish/"'

$podmanArgs = @(
    'run', '--rm', '--pull=missing',
    '-v', "${hostOut}:/out",
    '-v', "${appDir}:/src:ro",
    '-v', "${publishStaging}:/pub:ro",
    'docker.io/library/alpine:3.19',
    'sh', '-c', $innerSh
)
& podman @podmanArgs

if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

Sync-HostOutputToNamedVolume -HostOut $hostOut -VolumeName $vol
Write-PodmanHostOutputSummary -HostOut $hostOut -VolumeName $vol -Detail "  /out/deploy = workspace; /out/publish = publish-output ('${publishStaging}')"

exit $LASTEXITCODE

