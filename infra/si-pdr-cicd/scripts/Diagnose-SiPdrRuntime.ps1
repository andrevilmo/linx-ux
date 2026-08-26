<#
.SYNOPSIS
  Post-deploy diagnostics for SI-PDR IIS + SQL reachability (AWS Windows host).
#>
[CmdletBinding()]
param(
    [string] $FrameworkRoot = $(if ($env:LINX_IIS_ROOT) { $env:LINX_IIS_ROOT } else { 'C:\Linx Program Files\Linx Framework 6.0.0' })
)

$ErrorActionPreference = 'Continue'
New-Item -ItemType Directory -Force -Path C:\Linx-Build | Out-Null
$log = 'C:\Linx-Build\diagnose-si-pdr.log'
function Write-Log([string]$Message) {
    $line = "{0} {1}" -f (Get-Date -Format o), $Message
    $line | Tee-Object -FilePath $log -Append
}

Write-Log "===== Diagnose-SiPdrRuntime FrameworkRoot=$FrameworkRoot ====="

try {
    Import-Module WebAdministration -ErrorAction Stop
} catch {
    Write-Log ("WebAdministration import failed: {0}" -f $_.Exception.Message)
}

foreach ($name in @('Application', 'Service', 'Portal')) {
    try {
        $site = Get-Website -Name $name -ErrorAction Stop
        $binds = @($site.bindings.Collection | ForEach-Object { $_.bindingInformation }) -join ', '
        $pool = $site.applicationPool
        $poolState = (Get-WebAppPoolState -Name $pool -ErrorAction SilentlyContinue).Value
        Write-Log ("IIS {0}: state={1} pool={2} poolState={3} path={4} binds=[{5}]" -f `
            $name, $site.state, $pool, $poolState, $site.physicalPath, $binds)
    } catch {
        Write-Log ("IIS {0}: MISSING ({1})" -f $name, $_.Exception.Message)
    }
}

foreach ($port in 1710, 8082, 8172, 8174, 8080, 8081) {
    try {
        $listeners = Get-NetTCPConnection -LocalPort $port -State Listen -ErrorAction SilentlyContinue |
            Select-Object -First 3 LocalAddress, OwningProcess
        if ($listeners) {
            foreach ($l in $listeners) {
                $proc = Get-Process -Id $l.OwningProcess -ErrorAction SilentlyContinue
                Write-Log ("LISTEN :{0} addr={1} pid={2} name={3}" -f $port, $l.LocalAddress, $l.OwningProcess, $proc.ProcessName)
            }
        } else {
            Write-Log ("LISTEN :{0} (none)" -f $port)
        }
    } catch {
        Write-Log ("LISTEN :{0} check failed: {1}" -f $port, $_.Exception.Message)
    }
}

try {
    $os = Get-CimInstance Win32_OperatingSystem
    $freeMb = [math]::Round($os.FreePhysicalMemory / 1024, 0)
    $totalMb = [math]::Round($os.TotalVisibleMemorySize / 1024, 0)
    Write-Log ("Memory free={0}MB total={1}MB" -f $freeMb, $totalMb)
} catch {
    Write-Log ("Memory check failed: {0}" -f $_.Exception.Message)
}

# Parse FrameworkAutorizacao from Service web.config (do not log password)
$serviceCfg = Join-Path $FrameworkRoot 'Service\web.config'
$sqlHost = $null
$sqlPort = 1433
$sqlUser = $null
$sqlCatalog = $null
$sqlPassword = $null
if (Test-Path -LiteralPath $serviceCfg) {
    try {
        [xml]$xml = Get-Content -LiteralPath $serviceCfg -Raw
        $add = $xml.SelectSingleNode("/configuration/connectionStrings/add[@name='FrameworkAutorizacao']")
        if ($add) {
            $cs = [string]$add.GetAttribute('connectionString')
            if ($cs -match '(?i)(?:data source|server)\s*=\s*([^;]+)') {
                $ds = $Matches[1].Trim()
                $ds = $ds -replace '(?i)^tcp:', ''
                if ($ds -match '^([^,\\]+)[,\\](\d+)$') {
                    $sqlHost = $Matches[1]
                    $sqlPort = [int]$Matches[2]
                } else {
                    $sqlHost = ($ds -split '\\')[0]
                }
            }
            if ($cs -match '(?i)(?:initial catalog|database)\s*=\s*([^;]+)') { $sqlCatalog = $Matches[1].Trim() }
            if ($cs -match '(?i)(?:user id|uid)\s*=\s*([^;]+)') { $sqlUser = $Matches[1].Trim() }
            if ($cs -match '(?i)(?:password|pwd)\s*=\s*([^;]+)') { $sqlPassword = $Matches[1].Trim() }
            $hasTimeout = $cs -match '(?i)connect\s*timeout\s*='
            $authMode = if ($cs -match '(?i)integrated\s+security\s*=\s*(sspi|true)') { 'SSPI' } else { 'SQL' }
            Write-Log ("Service FrameworkAutorizacao host={0} port={1} catalog={2} user={3} auth={4} connectTimeoutSet={5}" -f `
                $sqlHost, $sqlPort, $sqlCatalog, $sqlUser, $authMode, $hasTimeout)
        } else {
            Write-Log 'Service FrameworkAutorizacao connection string not found'
        }
    } catch {
        Write-Log ("Parse Service web.config failed: {0}" -f $_.Exception.Message)
    }
} else {
    Write-Log "Missing $serviceCfg"
}

if ($sqlHost) {
    $tcpOk = $false
    $sw = [System.Diagnostics.Stopwatch]::StartNew()
    try {
        $client = New-Object System.Net.Sockets.TcpClient
        $ar = $client.BeginConnect($sqlHost, $sqlPort, $null, $null)
        $waited = $ar.AsyncWaitHandle.WaitOne(3000, $false)
        if (-not $waited) {
            Write-Log ("TCP {0}:{1} TIMEOUT after {2}ms" -f $sqlHost, $sqlPort, $sw.ElapsedMilliseconds)
            try { $client.Close() } catch { }
        } elseif ($client.Connected) {
            $tcpOk = $true
            Write-Log ("TCP {0}:{1} OK in {2}ms" -f $sqlHost, $sqlPort, $sw.ElapsedMilliseconds)
            $client.Close()
        } else {
            Write-Log ("TCP {0}:{1} FAILED (not connected) in {2}ms" -f $sqlHost, $sqlPort, $sw.ElapsedMilliseconds)
        }
    } catch {
        Write-Log ("TCP {0}:{1} ERROR: {2}" -f $sqlHost, $sqlPort, $_.Exception.Message)
    }

    if ($tcpOk -and $sqlUser -and $sqlPassword -and $sqlCatalog) {
        $sw.Restart()
        try {
            Add-Type -AssemblyName System.Data
            $builder = New-Object System.Data.SqlClient.SqlConnectionStringBuilder
            $builder['Data Source'] = ('tcp:{0},{1}' -f $sqlHost, $sqlPort)
            $builder['Initial Catalog'] = $sqlCatalog
            $builder['User ID'] = $sqlUser
            $builder['Password'] = $sqlPassword
            $builder['Connect Timeout'] = 8
            $builder['Encrypt'] = $false
            $conn = New-Object System.Data.SqlClient.SqlConnection ($builder.ConnectionString)
            $conn.Open()
            $cmd = $conn.CreateCommand()
            $cmd.CommandText = 'SELECT DB_NAME()'
            $db = $cmd.ExecuteScalar()
            Write-Log ("SQL Open OK db={0} in {1}ms" -f $db, $sw.ElapsedMilliseconds)
            $conn.Close()
        } catch {
            Write-Log ("SQL Open FAIL in {0}ms: {1}" -f $sw.ElapsedMilliseconds, $_.Exception.Message)
        }
    } elseif (-not $tcpOk) {
        Write-Log 'SQL Open skipped (TCP failed) - Service AuthenticatePortal will hang/fail until host can reach SQL.'
    }
}

try {
    $events = Get-EventLog -LogName Application -Newest 40 -ErrorAction SilentlyContinue |
        Where-Object {
            $_.EntryType -in @('Error', 'Warning') -and (
                $_.Source -match 'ASP\.NET|IIS|MSExchange|MSSQL|Windows Error|\.NET Runtime|Application Error' -or
                $_.Message -match '(?i)Service|Linx|SqlException|Timeout|w3wp|FrameworkAutorizacao'
            )
        } |
        Select-Object -First 12
    if ($events) {
        foreach ($ev in $events) {
            $msg = (($ev.Message -replace '\s+', ' ').Trim())
            $limit = 400
            if ($ev.Source -match 'ASP\.NET' -or $msg -match '(?i)compilation|CS\d{4}|yellow') {
                $limit = 2000
            }
            if ($msg.Length -gt $limit) { $msg = $msg.Substring(0, $limit) + '...' }
            Write-Log ("EventLog {0} {1}: {2}" -f $ev.TimeGenerated.ToString('o'), $ev.Source, $msg)
        }
    } else {
        Write-Log 'EventLog: no recent matching Application errors'
    }
} catch {
    Write-Log ("EventLog read failed: {0}" -f $_.Exception.Message)
}

Write-Log '===== Diagnose-SiPdrRuntime done ====='
exit 0
