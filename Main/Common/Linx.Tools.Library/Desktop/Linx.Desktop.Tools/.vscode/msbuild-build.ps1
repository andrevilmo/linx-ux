$ErrorActionPreference = 'Stop'
Set-Location (Split-Path -Parent $PSScriptRoot)
$csproj = Join-Path (Get-Location) 'Linx.Desktop.Tools.csproj'
if (-not (Test-Path $csproj)) {
    Write-Error "Project not found: $csproj"
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
& $msbuild $csproj /p:Configuration=Release /m /v:minimal /nologo
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

# Keep IIS / Program Files GAC in sync when present (BV used to reference this path).
$built = Join-Path (Get-Location) 'bin\Release\Linx.Tools.dll'
$programFilesGac = 'C:\Linx Program Files\Linx Framework 6.0.0\Library\Common\Linx\Desktop\GAC'
if ((Test-Path -LiteralPath $built) -and (Test-Path -LiteralPath $programFilesGac)) {
    Copy-Item -LiteralPath $built -Destination (Join-Path $programFilesGac 'Linx.Tools.dll') -Force
    Write-Host "Updated: $programFilesGac\Linx.Tools.dll"
}

exit 0
