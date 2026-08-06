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

if (-not (Get-Command aws -ErrorAction SilentlyContinue)) {
    Write-Log 'Installing AWS CLI v2'
    choco install -y awscli --no-progress
    $env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
                [System.Environment]::GetEnvironmentVariable('Path', 'User')
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
      --package-parameters "--add Microsoft.VisualStudio.Workload.MSBuildTools --add Microsoft.VisualStudio.Workload.ManagedDesktopBuildTools --add Microsoft.VisualStudio.Workload.WebBuildTools --add Microsoft.Net.Component.4.8.SDK --add Microsoft.Net.Component.4.8.TargetingPack --add Microsoft.Net.Component.4.6.1.SDK --add Microsoft.Net.Component.4.6.1.TargetingPack --quiet --norestart --wait" `
      --no-progress
    $env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
                [System.Environment]::GetEnvironmentVariable('Path', 'User')
    if (Test-Path -LiteralPath $vswhere) {
        $msbuild = & $vswhere -latest -products * -requires Microsoft.Component.MSBuild -find 'MSBuild/**/Bin/MSBuild.exe' |
            Select-Object -First 1
    }
}

# A partial v4.6.1 folder (XML docs only, no mscorlib.dll) still fails MSBuild.
# Prefer the standalone Developer Pack; VS component adds alone are unreliable here.
$ref461Dll = 'C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\mscorlib.dll'
if (-not (Test-Path -LiteralPath $ref461Dll)) {
    Write-Log '.NET Framework 4.6.1 reference assemblies incomplete; installing netfx-4.6.1-devpack'
    choco install -y netfx-4.6.1-devpack --no-progress
    if (-not (Test-Path -LiteralPath $ref461Dll)) {
        throw 'Missing .NET Framework 4.6.1 mscorlib.dll after netfx-4.6.1-devpack install'
    }
    Write-Log 'Installed .NET Framework 4.6.1 developer / targeting pack'
}

# ASP.NET / SPA csproj imports Microsoft.WebApplication.targets (Web Build Tools).
$webTargets = 'C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Microsoft\VisualStudio\v17.0\WebApplications\Microsoft.WebApplication.targets'
if (-not (Test-Path -LiteralPath $webTargets)) {
    Write-Log 'Microsoft.WebApplication.targets missing; installing WebBuildTools workload'
    # Chocolatey workload package is more reliable than setup.exe modify arg parsing here.
    choco install -y visualstudio2022-workload-webbuildtools --package-parameters '--includeRecommended --quiet --norestart' --no-progress
    $env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
                [System.Environment]::GetEnvironmentVariable('Path', 'User')
    if (-not (Test-Path -LiteralPath $webTargets)) {
        $setup = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\setup.exe'
        $btPath = 'C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools'
        if ((Test-Path -LiteralPath $setup) -and (Test-Path -LiteralPath $btPath)) {
            Write-Log 'Chocolatey workload missing targets; retrying via vs_installer config'
            $config = 'C:\Linx-Build\webbuildtools.vsconfig'
            @'
{
  "version": "1.0",
  "components": [
    "Microsoft.VisualStudio.Workload.WebBuildTools"
  ]
}
'@ | Set-Content -Path $config -Encoding UTF8
            $p = Start-Process -FilePath $setup -ArgumentList @(
                'modify',
                '--installPath', $btPath,
                '--config', $config,
                '--quiet', '--wait'
            ) -Wait -PassThru
            Write-Log "VS config modify exit=$($p.ExitCode)"
        }
    }
    if (-not (Test-Path -LiteralPath $webTargets)) {
        throw "Still missing WebApplication.targets at $webTargets"
    }
    Write-Log 'WebBuildTools / WebApplication.targets ready'
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
exit 0
