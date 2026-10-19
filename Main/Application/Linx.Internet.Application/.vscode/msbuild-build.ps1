$ErrorActionPreference = 'Stop'
Set-Location (Split-Path -Parent $PSScriptRoot)
$sln = Join-Path (Get-Location) 'Linx.Internet.Application.sln'
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
# Pin Platform=Any CPU. Without it, MSBuild may pick Mixed Platforms / x86 and
# build WinHost (net40 ClickOnce), which is not deployed to IIS and needs a
# separate .NET 4.0 targeting pack on clean CI hosts.
# WinHost has ActiveCfg but no Build.0 for Release|Any CPU.
$msbuildArgs = @(
    $sln,
    '/p:Configuration=Release',
    '/p:Platform=Any CPU',
    '/m',
    '/v:minimal',
    '/nologo'
)
if ($env:SKIP_PODMAN_SYNC -eq '1') {
    Write-Host 'CI: building Application solution for Release|Any CPU (WinHost excluded by sln config)'
}
& $msbuild @msbuildArgs
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
