param(
    [string] $FrameworkRoot = 'C:\Linx Program Files\Linx Framework 6.0.0',
    [string] $ServiceUrl = 'http://localhost:1710/',
    [string] $PortalUrl = 'http://localhost:8172/',
    [string] $HmlHost = 'api-hml.linx.com.br'
)

$ErrorActionPreference = 'Continue'
$service = Join-Path $FrameworkRoot 'Service'
$web = Join-Path $service 'Web.config'
$bin = Join-Path $service 'bin'
$fail = 0

function Assert-Step([string] $Name, [scriptblock] $Check) {
    try {
        $ok = & $Check
        if ($ok) {
            Write-Host "PASS  $Name"
            return
        }
        Write-Host "FAIL  $Name"
        $script:fail++
    } catch {
        Write-Host "FAIL  $Name — $($_.Exception.Message)"
        $script:fail++
    }
}

Write-Host "IIS root : $FrameworkRoot"
Write-Host "Service  : $ServiceUrl"
Write-Host "Portal   : $PortalUrl"
Write-Host ""

Assert-Step "Service Web.config exists" { Test-Path -LiteralPath $web }
Assert-Step "Linx.Framework.BV.dll in Service\bin" { Test-Path -LiteralPath (Join-Path $bin 'Linx.Framework.BV.dll') }
Assert-Step "RestSharp.dll present (do not replace)" { Test-Path -LiteralPath (Join-Path $bin 'RestSharp.dll') }
Assert-Step "Newtonsoft.Json.dll present" { Test-Path -LiteralPath (Join-Path $bin 'Newtonsoft.Json.dll') }
Assert-Step "Linx.Tools.dll present" { Test-Path -LiteralPath (Join-Path $bin 'Linx.Tools.dll') }

if (Test-Path -LiteralPath $web) {
    $cfg = Get-Content -LiteralPath $web -Raw
    Assert-Step "LicenseServer.Enabled=true" { $cfg -match 'key="LicenseServer.Enabled"\s+value="true"' }
    Assert-Step "LicenseServer.BaseUrl is HML app-licensing" { $cfg -match 'api-hml\.linx\.com\.br/app-licensing' }
    Assert-Step "LicenseServer.Email present" { $cfg -match 'key="LicenseServer.Email"' }
    Assert-Step "LicenseServer.Cnpj present" { $cfg -match 'key="LicenseServer.Cnpj"' }
    Assert-Step "Web.config still looks like a live Service config (has connectionStrings or ServiceBus)" {
        ($cfg -match 'connectionStrings') -or ($cfg -match 'ServiceBus') -or ($cfg -match 'authorizationService')
    }
}

try {
    Import-Module WebAdministration -ErrorAction Stop
    $pool = Get-WebAppPoolState -Name 'SI-PDR-Service' -ErrorAction SilentlyContinue
    if ($pool) {
        Assert-Step "App pool SI-PDR-Service is Started" { $pool.Value -eq 'Started' }
    } else {
        Write-Host "SKIP  App pool SI-PDR-Service not found (name differs on this host)"
    }
} catch {
    Write-Host "SKIP  WebAdministration (run as Administrator to check the pool)"
}

Assert-Step "GET $ServiceUrl is reachable" {
    try {
        $resp = Invoke-WebRequest -Uri $ServiceUrl -UseBasicParsing -TimeoutSec 15
        Write-Host "      HTTP $([int]$resp.StatusCode)"
        return $true
    } catch {
        $ex = $_.Exception
        if ($ex.Response) {
            Write-Host "      HTTP $([int]$ex.Response.StatusCode) (site up)"
            return $true
        }
        throw
    }
}

Assert-Step "Outbound TCP 443 $HmlHost" {
    if (Get-Command Test-NetConnection -ErrorAction SilentlyContinue) {
        $t = Test-NetConnection -ComputerName $HmlHost -Port 443 -WarningAction SilentlyContinue
        return [bool]$t.TcpTestSucceeded
    }
    $tcp = New-Object System.Net.Sockets.TcpClient
    try {
        $iar = $tcp.BeginConnect($HmlHost, 443, $null, $null)
        $ok = $iar.AsyncWaitHandle.WaitOne(8000, $false) -and $tcp.Connected
        return $ok
    } finally {
        $tcp.Close()
    }
}

Write-Host ""
Write-Host "Manual: open $PortalUrl and sign in. Gate runs on Service authenticateUser."
Write-Host "HML test license may return lxStatusChave=4 (LicenseException) — that still proves the gate is live."
if ($fail -gt 0) {
    Write-Host "RESULT FAIL ($fail check(s))"
    exit 1
}
Write-Host "RESULT PASS"
exit 0
