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
& $msbuild $sln /p:Configuration=Release /m /v:minimal /nologo
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

# Mirror Application child project bin folders into Podman volume Linx.Internet.Application-output
& (Join-Path $PSScriptRoot 'sync-build-to-podman-volume.ps1')
exit $LASTEXITCODE
