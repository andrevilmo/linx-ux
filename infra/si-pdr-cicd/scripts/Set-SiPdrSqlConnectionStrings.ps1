<#
.SYNOPSIS
  Rewrite SI-PDR SQL connection strings on the IIS Framework root for AWS/CI hosts.

.DESCRIPTION
  Portal login calls Service (AuthenticatePortal), which opens EF against
  FrameworkAutorizacao. Binary defaults use Integrated Security=SSPI to
  a-srv111.linx-inves.com.br - that fails on the AWS EC2 host (no domain /
  often no network path). Supply SQL auth via parameters or environment.

  Env / parameters:
    SI_PDR_SQL_PORTAL_CONNECTION  full connection string for portal/auth DB
    SI_PDR_SQL_APP_CONNECTION     full connection string for app DB
  Or build from parts:
    SI_PDR_SQL_DATA_SOURCE        e.g. a-srv111.linx-inves.com.br\sql2017
    SI_PDR_SQL_USER / SI_PDR_SQL_PASSWORD
    SI_PDR_SQL_PORTAL_CATALOG     default DEV-UX-Portal-Main
    SI_PDR_SQL_APP_CATALOG        default DEV-UX-App-Main
#>
[CmdletBinding()]
param(
    [string] $FrameworkRoot = $(if ($env:LINX_IIS_ROOT) { $env:LINX_IIS_ROOT } else { 'C:\Linx Program Files\Linx Framework 6.0.0' }),
    [string] $PortalConnection = $env:SI_PDR_SQL_PORTAL_CONNECTION,
    [string] $AppConnection = $env:SI_PDR_SQL_APP_CONNECTION,
    [string] $DataSource = $env:SI_PDR_SQL_DATA_SOURCE,
    [string] $SqlUser = $env:SI_PDR_SQL_USER,
    [string] $SqlPassword = $env:SI_PDR_SQL_PASSWORD,
    [string] $PortalCatalog = $(if ($env:SI_PDR_SQL_PORTAL_CATALOG) { $env:SI_PDR_SQL_PORTAL_CATALOG } else { 'DEV-UX-Portal-Main' }),
    [string] $AppCatalog = $(if ($env:SI_PDR_SQL_APP_CATALOG) { $env:SI_PDR_SQL_APP_CATALOG } else { 'DEV-UX-App-Main' }),
    [string] $ServiceUrl = $env:SI_PDR_SERVICE_URL,
    # Only override when SI_PDR_* env is set - Binary web.configs are authoritative.
    [string] $ShellMode = $env:SI_PDR_SHELL_MODE,
    [string] $LocalServiceBusMode = $env:SI_PDR_LOCAL_SERVICEBUS_MODE,
    [string] $SsoClientSecret = $env:SI_PDR_SSO_CLIENT_SECRET
)

$ErrorActionPreference = 'Stop'
New-Item -ItemType Directory -Force -Path C:\Linx-Build | Out-Null
$log = 'C:\Linx-Build\set-sql-connection.log'
function Write-Log([string]$Message) {
    $line = "{0} {1}" -f (Get-Date -Format o), $Message
    $line | Tee-Object -FilePath $log -Append
}

function New-SqlAuthConnectionString {
    param([string]$Server, [string]$Catalog, [string]$User, [string]$Password)
    return ("Data Source={0};Initial Catalog={1};User ID={2};Password={3};" -f $Server, $Catalog, $User, $Password)
}

function Set-XmlConnectionString {
    param(
        [Parameter(Mandatory = $true)][string] $Path,
        [Parameter(Mandatory = $true)][hashtable] $Values
    )
    if (-not (Test-Path -LiteralPath $Path)) {
        Write-Log "SKIP missing $Path"
        return
    }
    [xml]$xml = Get-Content -LiteralPath $Path -Raw
    $csNode = $xml.SelectSingleNode('/configuration/connectionStrings')
    if (-not $csNode) {
        Write-Log "SKIP no connectionStrings in $Path"
        return
    }
    $changed = $false
    foreach ($name in $Values.Keys) {
        $add = $csNode.SelectNodes("add[@name='$name']") | Select-Object -First 1
        if (-not $add) {
            $add = $xml.CreateElement('add')
            $add.SetAttribute('name', $name)
            $add.SetAttribute('providerName', 'System.Data.SqlClient')
            [void]$csNode.AppendChild($add)
        }
        $add.SetAttribute('connectionString', $Values[$name])
        if (-not $add.HasAttribute('providerName')) {
            $add.SetAttribute('providerName', 'System.Data.SqlClient')
        }
        $changed = $true
        Write-Log ("Updated {0} :: {1}" -f $Path, $name)
    }
    if ($changed) {
        $xml.Save($Path)
    }
}

function Set-AppSetting {
    param(
        [string]$Path,
        [string]$SectionXPath,
        [string]$Key,
        [string]$Value,
        [switch]$Required
    )
    if (-not (Test-Path -LiteralPath $Path)) {
        if ($Required) { throw "Missing config file $Path" }
        return
    }
    [xml]$xml = Get-Content -LiteralPath $Path -Raw
    $section = $xml.SelectSingleNode($SectionXPath)
    if (-not $section) {
        if ($Required) { throw "Missing section $SectionXPath in $Path" }
        return
    }
    $node = $section.SelectSingleNode("add[@key='$Key']")
    if (-not $node) {
        $node = $xml.CreateElement('add')
        $node.SetAttribute('key', $Key)
        [void]$section.AppendChild($node)
    }
    $node.SetAttribute('value', $Value)
    $xml.Save($Path)
    if ($Key -match '(?i)PASSWORD|SECRET') {
        Write-Log ("{0} {1} => (set, len={2})" -f $Path, $Key, $Value.Length)
    }
    else {
        Write-Log ("{0} {1} => {2}" -f $Path, $Key, $Value)
    }
}

# Load optional overrides file dropped into the CI package by GitHub Actions
$overrideFile = Join-Path $PSScriptRoot 'sql-overrides.psd1'
if (Test-Path -LiteralPath $overrideFile) {
    Write-Log "Loading $overrideFile"
    $ov = Import-PowerShellDataFile -Path $overrideFile
    if ($ov.PortalConnection) { $PortalConnection = [string]$ov.PortalConnection }
    if ($ov.AppConnection) { $AppConnection = [string]$ov.AppConnection }
    if ($ov.DataSource) { $DataSource = [string]$ov.DataSource }
    if ($ov.SqlUser) { $SqlUser = [string]$ov.SqlUser }
    if ($ov.SqlPassword) { $SqlPassword = [string]$ov.SqlPassword }
    if ($ov.PortalCatalog) { $PortalCatalog = [string]$ov.PortalCatalog }
    if ($ov.AppCatalog) { $AppCatalog = [string]$ov.AppCatalog }
    if ($ov.ServiceUrl) { $ServiceUrl = [string]$ov.ServiceUrl }
}

if (-not $PortalConnection -and $DataSource -and $SqlUser -and $SqlPassword) {
    $PortalConnection = New-SqlAuthConnectionString -Server $DataSource -Catalog $PortalCatalog -User $SqlUser -Password $SqlPassword
}
if (-not $AppConnection -and $DataSource -and $SqlUser -and $SqlPassword) {
    $AppConnection = New-SqlAuthConnectionString -Server $DataSource -Catalog $AppCatalog -User $SqlUser -Password $SqlPassword
}

$portalPath = Join-Path $FrameworkRoot 'Portal\web.config'
$appPath = Join-Path $FrameworkRoot 'Application\web.config'
$servicePath = Join-Path $FrameworkRoot 'Service\web.config'
if ($ServiceUrl) {
    Set-AppSetting -Path $portalPath -SectionXPath '/configuration/PortalSettings' -Key 'authorizationServiceAddress' -Value $ServiceUrl
    Set-AppSetting -Path $appPath -SectionXPath '/configuration/appSettings' -Key 'ServiceBus' -Value $ServiceUrl
}
if ($ShellMode) {
    Set-AppSetting -Path $appPath -SectionXPath '/configuration/appSettings' -Key 'ShellMode' -Value $ShellMode
}
if ($LocalServiceBusMode) {
    Set-AppSetting -Path $servicePath -SectionXPath '/configuration/LocalServiceBusSettings' -Key 'mode' -Value $LocalServiceBusMode
}

# Azure AD confidential-client secret is not stored in git (GitHub push protection).
# Resolve from (1) env SI_PDR_SSO_CLIENT_SECRET, (2) sidecar next to this script,
# (3) S3 object used by SI-PDR CI. Never log the value.
function Resolve-SsoClientSecret {
    param([string] $FromEnv)
    if (-not [string]::IsNullOrWhiteSpace($FromEnv)) {
        return @{ Value = $FromEnv.Trim(); Source = 'env' }
    }
    $sidecar = Join-Path $PSScriptRoot '.sso-client-secret'
    if (Test-Path -LiteralPath $sidecar) {
        $raw = (Get-Content -LiteralPath $sidecar -Raw -ErrorAction SilentlyContinue)
        if (-not [string]::IsNullOrWhiteSpace($raw)) {
            return @{ Value = $raw.Trim(); Source = 'sidecar' }
        }
    }
    $s3Uri = if ($env:SI_PDR_SSO_CLIENT_SECRET_S3) {
        $env:SI_PDR_SSO_CLIENT_SECRET_S3
    } else {
        's3://omnipos-cicd-253957900820-sa-east-1/linx-ux/secrets/SSO_CLIENT_SECRET'
    }
    try {
        $tmp = Join-Path $env:TEMP ('si-pdr-sso-' + [guid]::NewGuid().ToString('n') + '.txt')
        $aws = Get-Command aws -ErrorAction SilentlyContinue
        if ($aws) {
            & aws s3 cp $s3Uri $tmp --region sa-east-1 --no-progress 2>$null | Out-Null
            if ($LASTEXITCODE -eq 0 -and (Test-Path -LiteralPath $tmp)) {
                $raw = (Get-Content -LiteralPath $tmp -Raw -ErrorAction SilentlyContinue)
                Remove-Item -LiteralPath $tmp -Force -ErrorAction SilentlyContinue
                if (-not [string]::IsNullOrWhiteSpace($raw)) {
                    return @{ Value = $raw.Trim(); Source = 's3' }
                }
            }
            Remove-Item -LiteralPath $tmp -Force -ErrorAction SilentlyContinue
        }
    } catch {
        Write-Log 'SSO client secret S3 fallback failed (non-fatal)'
    }
    return $null
}

$resolvedSso = Resolve-SsoClientSecret -FromEnv $SsoClientSecret
if ($resolvedSso) {
    $portalConfigs = @($portalPath)
    try {
        Import-Module WebAdministration -ErrorAction Stop
        $portalSite = Get-Website -Name 'Portal' -ErrorAction Stop
        if ($portalSite -and $portalSite.physicalPath) {
            $iisPortalCfg = Join-Path $portalSite.physicalPath 'web.config'
            if ($portalConfigs -notcontains $iisPortalCfg) {
                $portalConfigs += $iisPortalCfg
            }
        }
    } catch {
        Write-Log ("IIS Portal path lookup skipped: {0}" -f $_.Exception.Message)
    }
    foreach ($cfg in $portalConfigs) {
        Set-AppSetting -Path $cfg -SectionXPath '/configuration/PortalSettings' -Key 'SSO_CLIENT_SECRET' -Value $resolvedSso.Value -Required
        [xml]$check = Get-Content -LiteralPath $cfg -Raw
        $node = $check.SelectSingleNode("/configuration/PortalSettings/add[@key='SSO_CLIENT_SECRET']")
        $len = 0
        if ($node -and $node.GetAttribute('value')) { $len = $node.GetAttribute('value').Length }
        if ($len -le 0) { throw "SSO_CLIENT_SECRET empty after inject in $cfg" }
        Write-Log ("Verified {0} SSO_CLIENT_SECRET len={1} source={2}" -f $cfg, $len, $resolvedSso.Source)
    }
    Write-Output 'SSO_CLIENT_SECRET_APPLIED'
}
else {
    Write-Log 'SI_PDR_SSO_CLIENT_SECRET not set; Portal SSO_CLIENT_SECRET left as in Binary web.config.'
    Write-Output 'SSO_CLIENT_SECRET_SKIPPED'
}

if (-not $PortalConnection -and -not $AppConnection) {
    Write-Log 'No SI_PDR_SQL_* overrides provided; leaving Binary SQL connection strings unchanged.'
    Write-Output 'SQL_OVERRIDES_SKIPPED'
    # Still recycle so authorizationServiceAddress / ServiceBus changes take effect
} else {
    $portalNames = @('FrameworkAutorizacao', 'LocalSqlServer')
    $appNames = @('ControleSistema', 'MiniProfiler', 'TelerikCacheStorage')

    $targets = @(
        (Join-Path $FrameworkRoot 'Service\web.config'),
        (Join-Path $FrameworkRoot 'Application\web.config'),
        (Join-Path $FrameworkRoot 'Library\Business Model\Linx.Framework.Autorizacao.BM.dll.config'),
        (Join-Path $FrameworkRoot 'Library\Business Model\Linx.Framework.ControleSistema.BM.dll.config')
    )

    foreach ($path in $targets) {
        $map = @{}
        if ($PortalConnection) {
            foreach ($n in $portalNames) { $map[$n] = $PortalConnection }
        }
        if ($AppConnection) {
            foreach ($n in $appNames) { $map[$n] = $AppConnection }
        }
        if ($path -like '*Autorizacao.BM.dll.config*' -and $PortalConnection) {
            $map = @{ FrameworkAutorizacao = $PortalConnection }
        }
        if ($path -like '*ControleSistema.BM.dll.config*' -and $AppConnection) {
            $map = @{ ControleSistema = $AppConnection }
        }
        if ($path -like '*Application\web.config*' -or $path -like '*Application/web.config*') {
            # Application site typically only has MiniProfiler (+ maybe others)
            $map = @{}
            if ($AppConnection) { $map['MiniProfiler'] = $AppConnection }
        }
        if ($map.Count -gt 0) {
            Set-XmlConnectionString -Path $path -Values $map
        }
    }
    Write-Log 'SQL connection string overrides applied.'
    Write-Output 'SQL_OVERRIDES_APPLIED'
}

# Recycle so new connection strings / Service URLs are picked up
try {
    Import-Module WebAdministration -ErrorAction Stop
    foreach ($poolName in @('SI-PDR-Service', 'SI-PDR-Portal', 'SI-PDR-Application', 'DefaultAppPool')) {
        if (Get-Item "IIS:\AppPools\$poolName" -ErrorAction SilentlyContinue) {
            Restart-WebAppPool -Name $poolName -ErrorAction SilentlyContinue
            Write-Log "Recycled $poolName"
        }
    }
} catch {
    Write-Log ("App pool recycle warning: {0}" -f $_.Exception.Message)
}

exit 0
