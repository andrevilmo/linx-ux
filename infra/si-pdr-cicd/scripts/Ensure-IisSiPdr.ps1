<#
.SYNOPSIS
  Installs IIS + ASP.NET and creates Application / Service / Portal sites for SI-PDR.

.PARAMETER FrameworkRoot
  IIS content root (LINX_IIS_ROOT). Default: C:\Linx Program Files\Linx Framework 6.0.0

.PARAMETER SeedFromBinary
  Optional path to Main\Binary (Portal/Service/Application) used to seed site folders on first run.
#>
[CmdletBinding()]
param(
    [string] $FrameworkRoot = $(if ($env:LINX_IIS_ROOT) { $env:LINX_IIS_ROOT } else { 'C:\Linx Program Files\Linx Framework 6.0.0' }),
    [string] $SeedFromBinary = ''
)

$ErrorActionPreference = 'Stop'
New-Item -ItemType Directory -Force -Path C:\Linx-Build | Out-Null
$log = 'C:\Linx-Build\ensure-iis.log'
function Write-Log([string]$Message) {
    $line = "{0} {1}" -f (Get-Date -Format o), $Message
    $line | Tee-Object -FilePath $log -Append
}

Write-Log "Ensure-IisSiPdr start FrameworkRoot=$FrameworkRoot"

$features = @(
    'IIS-WebServerRole',
    'IIS-WebServer',
    'IIS-CommonHttpFeatures',
    'IIS-StaticContent',
    'IIS-DefaultDocument',
    'IIS-DirectoryBrowsing',
    'IIS-HttpErrors',
    'IIS-ApplicationDevelopment',
    'IIS-ASPNET45',
    'IIS-NetFxExtensibility45',
    'IIS-ISAPIExtensions',
    'IIS-ISAPIFilter',
    'IIS-HealthAndDiagnostics',
    'IIS-HttpLogging',
    'IIS-Security',
    'IIS-RequestFiltering',
    'IIS-Performance',
    'IIS-HttpCompressionStatic',
    'IIS-WebServerManagementTools',
    'IIS-ManagementConsole',
    'NetFx4Extended-ASPNET45'
)

foreach ($f in $features) {
    $state = Get-WindowsOptionalFeature -Online -FeatureName $f -ErrorAction SilentlyContinue
    if ($state -and $state.State -eq 'Enabled') {
        Write-Log "Feature already enabled: $f"
        continue
    }
    Write-Log "Enabling feature: $f"
    Enable-WindowsOptionalFeature -Online -FeatureName $f -All -NoRestart -ErrorAction Continue | Out-Null
}

Import-Module WebAdministration -ErrorAction Stop

# Several .csproj files compile AssemblyInfoShared.cs from
# C:\Linx Program Files\Linx Framework 6.0.0\information\ (absolute via deep relative paths).
# Seed it from Main\Binary when missing so CI builds work on a fresh host.
if ($SeedFromBinary) {
    $infoDir = Join-Path $FrameworkRoot 'information'
    $infoTarget = Join-Path $infoDir 'AssemblyInfoShared.cs'
    $infoSources = @(
        (Join-Path $SeedFromBinary 'Library\Common\Linx\AssemblyInfoShared\AssemblyInfoShared.cs'),
        (Join-Path $SeedFromBinary 'Library\Common\Linx\Information\AssemblyInfoShared.cs')
    )
    if (-not (Test-Path -LiteralPath $infoTarget)) {
        foreach ($src in $infoSources) {
            if (Test-Path -LiteralPath $src) {
                New-Item -ItemType Directory -Force -Path $infoDir | Out-Null
                Copy-Item -LiteralPath $src -Destination $infoTarget -Force
                Write-Log "Seeded $infoTarget from $src"
                break
            }
        }
    }
    if (-not (Test-Path -LiteralPath $infoTarget)) {
        Write-Log 'WARNING: AssemblyInfoShared.cs not found under Binary; BV builds may fail'
    }
}

foreach ($port in 8080, 8081, 8082) {
    $rule = "SI-PDR-IIS-$port"
    if (-not (Get-NetFirewallRule -DisplayName $rule -ErrorAction SilentlyContinue)) {
        New-NetFirewallRule -DisplayName $rule -Direction Inbound -Protocol TCP -LocalPort $port -Action Allow | Out-Null
        Write-Log "Firewall allow TCP $port"
    }
}

$sites = @(
    @{ Name = 'Application'; Port = 8080; Relative = 'Application' }
    @{ Name = 'Service'; Port = 8082; Relative = 'Service' }
    @{ Name = 'Portal'; Port = 8081; Relative = 'Portal' }
)

foreach ($site in $sites) {
    $phys = Join-Path $FrameworkRoot $site.Relative
    $bin = Join-Path $phys 'bin'
    New-Item -ItemType Directory -Force -Path $bin | Out-Null
    if ($site.Name -ne 'Service') {
        New-Item -ItemType Directory -Force -Path (Join-Path $phys 'Views') | Out-Null
    }

    if ($SeedFromBinary) {
        $seedBin = Join-Path $SeedFromBinary ($site.Relative + '\bin')
        if (Test-Path -LiteralPath $seedBin) {
            Write-Log "Seeding $($site.Name) bin from $seedBin"
            Copy-Item -Path (Join-Path $seedBin '*') -Destination $bin -Recurse -Force -ErrorAction SilentlyContinue
        }
        $seedViews = Join-Path $SeedFromBinary ($site.Relative + '\Views')
        if (($site.Name -ne 'Service') -and (Test-Path -LiteralPath $seedViews)) {
            Write-Log "Seeding $($site.Name) Views from $seedViews"
            Copy-Item -Path (Join-Path $seedViews '*') -Destination (Join-Path $phys 'Views') -Recurse -Force -ErrorAction SilentlyContinue
        }
    }

    # Minimal web.config so the site can start even before full deploy
    $webConfig = Join-Path $phys 'web.config'
    if (-not (Test-Path -LiteralPath $webConfig)) {
        @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.web>
    <compilation targetFramework="4.6.1" />
    <httpRuntime targetFramework="4.6.1" />
    <customErrors mode="Off" />
  </system.web>
  <system.webServer>
    <directoryBrowse enabled="false" />
  </system.webServer>
</configuration>
"@ | Set-Content -Path $webConfig -Encoding UTF8
    }

    $existing = Get-Website -Name $site.Name -ErrorAction SilentlyContinue
    if (-not $existing) {
        Write-Log "Creating IIS site $($site.Name) on port $($site.Port) -> $phys"
        New-Website -Name $site.Name -Port $site.Port -PhysicalPath $phys -ApplicationPool 'DefaultAppPool' | Out-Null
    } else {
        Write-Log "IIS site $($site.Name) already exists"
        Set-ItemProperty "IIS:\Sites\$($site.Name)" -Name physicalPath -Value $phys
    }

    Start-Website -Name $site.Name -ErrorAction SilentlyContinue
}

[Environment]::SetEnvironmentVariable('LINX_IIS_ROOT', $FrameworkRoot, 'Machine')
$env:LINX_IIS_ROOT = $FrameworkRoot
Write-Log "LINX_IIS_ROOT=$FrameworkRoot"
Write-Log 'Ensure-IisSiPdr done'
Write-Output $FrameworkRoot
