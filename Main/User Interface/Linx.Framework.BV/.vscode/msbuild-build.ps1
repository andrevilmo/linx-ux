$ErrorActionPreference = 'Stop'
Set-Location (Split-Path -Parent $PSScriptRoot)
$sln = Join-Path (Get-Location) 'Linx.Framework.BV.sln'
if (-not (Test-Path $sln)) {
    Write-Error "Solution not found: $sln"
    exit 1
}
$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
if (-not (Test-Path $vswhere)) {
    Write-Error "vswhere not found at $vswhere. Install Visual Studio or Build Tools with MSBuild."
    exit 1
}
$msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
if (-not $msbuild) {
    Write-Error 'MSBuild not found. Install the MSBuild workload (e.g. .NET desktop build tools).'
    exit 1
}
$bvProj = Join-Path (Get-Location) '..\..\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj'
if (Test-Path $bvProj) {
    Write-Host 'Building Linx.Framework.BV first (Binary dependency for WebAPI.DS)...'
    & $msbuild $bvProj /p:Configuration=Release /v:minimal /nologo
    if ($LASTEXITCODE -ne 0) {
        exit $LASTEXITCODE
    }
}

# /m enables parallel project build (was /m:1 — serialized on the small CI host).
& $msbuild $sln /p:Configuration=Release /m /v:minimal /nologo
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

if ($env:SKIP_PODMAN_SYNC -eq '1') {
    Write-Host 'Skipping Podman volume sync (SKIP_PODMAN_SYNC=1).'
    exit 0
}

$syncScript = Join-Path $PSScriptRoot 'sync-build-to-podman-volume.ps1'
if (Test-Path $syncScript) {
    Write-Host 'Running Podman volume sync...'
    & $syncScript
    exit $LASTEXITCODE
}

exit 0
