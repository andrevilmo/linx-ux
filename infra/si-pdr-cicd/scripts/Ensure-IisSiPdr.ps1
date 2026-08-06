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

# Framework projects resolve many HintPaths under
# C:\Linx Program Files\Linx Framework 6.0.0\{Library,information}\...
# Seed those trees from Main\Binary so CI can build without a full installer.
if ($SeedFromBinary) {
    $seedLibrary = Join-Path $SeedFromBinary 'Library'
    $frameworkLibrary = Join-Path $FrameworkRoot 'Library'
    if (Test-Path -LiteralPath $seedLibrary) {
        Write-Log "Seeding Framework Library from $seedLibrary"
        New-Item -ItemType Directory -Force -Path $frameworkLibrary | Out-Null
        # /E copy subdirs including empty; /XO skip older; /NFL /NDL quieter logs
        & robocopy $seedLibrary $frameworkLibrary /E /XO /R:1 /W:1 /NFL /NDL /NJH /NJS | Out-Null
        $rc = $LASTEXITCODE
        # robocopy 0-7 = success / partial success
        if ($rc -ge 8) { throw "robocopy Library failed with exit $rc" }
        Write-Log "Library seed robocopy exit=$rc"
        cmd.exe /c "exit /b 0" | Out-Null
        $global:LASTEXITCODE = 0
    } else {
        Write-Log "WARNING: Binary Library not found at $seedLibrary"
    }

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

foreach ($port in 8080, 8081, 8082, 1710) {
    $rule = "SI-PDR-IIS-$port"
    if (-not (Get-NetFirewallRule -DisplayName $rule -ErrorAction SilentlyContinue)) {
        New-NetFirewallRule -DisplayName $rule -Direction Inbound -Protocol TCP -LocalPort $port -Action Allow | Out-Null
        Write-Log "Firewall allow TCP $port"
    }
}

$sites = @(
    # Application ServiceBus appSetting defaults to http://localhost:1710/
    @{ Name = 'Application'; Port = 8080; Relative = 'Application'; ExtraPorts = @() }
    @{ Name = 'Service'; Port = 1710; Relative = 'Service'; ExtraPorts = @(8082) }
    @{ Name = 'Portal'; Port = 8081; Relative = 'Portal'; ExtraPorts = @() }
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
        # Seed site shell files (Global.asax, areas, scripts, ...) — not only bin/
        $seedSite = Join-Path $SeedFromBinary $site.Relative
        if (Test-Path -LiteralPath $seedSite) {
            Write-Log "Seeding $($site.Name) site files from $seedSite"
            & robocopy $seedSite $phys /E /XO /R:1 /W:1 /NFL /NDL /NJH /NJS /XF web.config | Out-Null
            $rc = $LASTEXITCODE
            if ($rc -ge 8) { Write-Log "WARNING: robocopy $($site.Name) site files exit=$rc" }
            else { Write-Log "Site files seed robocopy exit=$rc" }
            cmd.exe /c "exit /b 0" | Out-Null
            $global:LASTEXITCODE = 0
        }

    }

    # Prefer Binary web.config (includes System.Web.Mvc binding redirects). A stub
    # without redirects causes FileLoadException for MVC 3.0 vs 4.0 in bin.
    $webConfig = Join-Path $phys 'web.config'
    $seedWebConfig = if ($SeedFromBinary) { Join-Path $SeedFromBinary ($site.Relative + '\web.config') } else { '' }
    $needsWebConfig = -not (Test-Path -LiteralPath $webConfig)
    $isStub = $false
    if (-not $needsWebConfig) {
        $existing = Get-Content -LiteralPath $webConfig -Raw -ErrorAction SilentlyContinue
        if ($existing -and ($existing -notmatch 'assemblyBinding' -or $existing.Length -lt 1024)) {
            $isStub = $true
        }
    }
    if (($needsWebConfig -or $isStub) -and $seedWebConfig -and (Test-Path -LiteralPath $seedWebConfig)) {
        Copy-Item -LiteralPath $seedWebConfig -Destination $webConfig -Force
        Write-Log "Seeded $($site.Name) web.config from $seedWebConfig"
    }
    elseif (-not (Test-Path -LiteralPath $webConfig)) {
        @"
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.web>
    <compilation targetFramework="4.6.1" />
    <httpRuntime targetFramework="4.6.1" />
    <customErrors mode="Off" />
  </system.web>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="System.Web.Mvc" publicKeyToken="31bf3856ad364e35" />
        <bindingRedirect oldVersion="0.0.0.0-4.0.0.0" newVersion="4.0.0.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
  <system.webServer>
    <directoryBrowse enabled="false" />
  </system.webServer>
</configuration>
"@ | Set-Content -Path $webConfig -Encoding UTF8
        Write-Log "Wrote fallback web.config with MVC binding redirect for $($site.Name)"
    }

    $existing = Get-Website -Name $site.Name -ErrorAction SilentlyContinue
    if (-not $existing) {
        Write-Log "Creating IIS site $($site.Name) on port $($site.Port) -> $phys"
        New-Website -Name $site.Name -Port $site.Port -PhysicalPath $phys -ApplicationPool 'DefaultAppPool' | Out-Null
    } else {
        Write-Log "IIS site $($site.Name) already exists"
        Set-ItemProperty "IIS:\Sites\$($site.Name)" -Name physicalPath -Value $phys
    }


    foreach ($extraPort in @($site.ExtraPorts)) {
        $bindInfo = "*:${extraPort}:"
        $has = $false
        $siteObj = Get-Website -Name $site.Name -ErrorAction SilentlyContinue
        if ($siteObj) {
            foreach ($b in $siteObj.bindings.Collection) {
                if ($b.bindingInformation -eq $bindInfo -or $b.bindingInformation -like "*:${extraPort}:") { $has = $true }
            }
        }
        if (-not $has) {
            Write-Log "Adding extra binding $($site.Name) port $extraPort"
            New-WebBinding -Name $site.Name -Protocol http -Port $extraPort -IPAddress '*' | Out-Null
        }
    }
    # Ensure primary port binding exists (site may have been created earlier on a different port)
    $primaryBind = "*:$($site.Port):"
    $siteObj = Get-Website -Name $site.Name -ErrorAction SilentlyContinue
    $hasPrimary = $false
    if ($siteObj) {
        foreach ($b in $siteObj.bindings.Collection) {
            if ($b.bindingInformation -eq $primaryBind -or $b.bindingInformation -like "*:$($site.Port):") { $hasPrimary = $true }
        }
    }
    if ($siteObj -and -not $hasPrimary) {
        Write-Log "Adding primary binding $($site.Name) port $($site.Port)"
        New-WebBinding -Name $site.Name -Protocol http -Port $site.Port -IPAddress '*' | Out-Null
    }

    Start-Website -Name $site.Name -ErrorAction SilentlyContinue
}

[Environment]::SetEnvironmentVariable('LINX_IIS_ROOT', $FrameworkRoot, 'Machine')
$env:LINX_IIS_ROOT = $FrameworkRoot
Write-Log "LINX_IIS_ROOT=$FrameworkRoot"
Write-Log 'Ensure-IisSiPdr done'
Write-Output $FrameworkRoot
exit 0
