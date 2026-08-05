<#
.SYNOPSIS
  Ensures Git, .NET Framework targeting packs, and VS 2022 Build Tools (MSBuild + web) are installed.
#>
$ErrorActionPreference = 'Stop'
New-Item -ItemType Directory -Force -Path C:\Linx-Build | Out-Null
$log = 'C:\Linx-Build\ensure-tools.log'
function Write-Log([string]$Message) {
    $line = "{0} {1}" -f (Get-Date -Format o), $Message
    $line | Tee-Object -FilePath $log -Append
}

Write-Log 'Ensure-BuildTools start'
$env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
            [System.Environment]::GetEnvironmentVariable('Path', 'User')

$fsKey = 'HKLM:\SYSTEM\CurrentControlSet\Control\FileSystem'
try {
    $longPaths = (Get-ItemProperty -Path $fsKey -Name LongPathsEnabled -ErrorAction SilentlyContinue).LongPathsEnabled
    if ($longPaths -ne 1) {
        Set-ItemProperty -Path $fsKey -Name LongPathsEnabled -Value 1 -Type DWord
        Write-Log 'Enabled Windows LongPathsEnabled'
    }
} catch {
    Write-Log "Could not set LongPathsEnabled: $($_.Exception.Message)"
}

if (-not (Get-Command choco -ErrorAction SilentlyContinue)) {
    Write-Log 'Installing Chocolatey'
    Set-ExecutionPolicy Bypass -Scope Process -Force
    [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
    Invoke-Expression ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
    $env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
                [System.Environment]::GetEnvironmentVariable('Path', 'User')
}

if (-not (Get-Command git -ErrorAction SilentlyContinue)) {
    Write-Log 'Installing git'
    choco install -y git --no-progress
}

$vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
$msbuild = $null
if (Test-Path -LiteralPath $vswhere) {
    $msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -find 'MSBuild/**/Bin/MSBuild.exe' |
        Select-Object -First 1
}

if (-not $msbuild) {
    Write-Log 'Installing Visual Studio 2022 Build Tools (Managed Desktop + Web + MSBuild)'
    choco install -y visualstudio2022buildtools `
      --package-parameters "--add Microsoft.VisualStudio.Workload.MSBuildTools --add Microsoft.VisualStudio.Workload.ManagedDesktopBuildTools --add Microsoft.VisualStudio.Workload.WebBuildTools --add Microsoft.Net.Component.4.8.SDK --add Microsoft.Net.Component.4.8.TargetingPack --add Microsoft.Net.Component.4.6.1.TargetingPack --quiet --norestart --wait" `
      --no-progress
    $env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
                [System.Environment]::GetEnvironmentVariable('Path', 'User')
    if (Test-Path -LiteralPath $vswhere) {
        $msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -find 'MSBuild/**/Bin/MSBuild.exe' |
            Select-Object -First 1
    }
}

if (-not $msbuild) {
    throw 'MSBuild not found after Build Tools install. Check C:\Linx-Build\ensure-tools.log'
}

# Build Tools are not on PATH by default; msbuild-build.ps1 scripts also need
# vswhere -products * (Build Tools are excluded from the default product filter).
$msbuildDir = Split-Path -Parent $msbuild
if ($env:Path -notlike "*$msbuildDir*") {
    $env:Path = "$msbuildDir;$env:Path"
}
$machinePath = [System.Environment]::GetEnvironmentVariable('Path', 'Machine')
if ($machinePath -notlike "*$msbuildDir*") {
    [System.Environment]::SetEnvironmentVariable('Path', "$msbuildDir;$machinePath", 'Machine')
    Write-Log "Prepended MSBuild dir to Machine PATH: $msbuildDir"
}

Write-Log "MSBuild ready: $msbuild"
Write-Output $msbuild
