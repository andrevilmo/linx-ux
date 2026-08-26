<#
.SYNOPSIS
  Build, publish, and deploy SI-PDR (Application / Service / Portal) on the AWS Windows host.
#>
[CmdletBinding()]
param(
    [string] $RepoRoot = (Get-Location).Path,
    [string] $FrameworkRoot = $(if ($env:LINX_IIS_ROOT) { $env:LINX_IIS_ROOT } else { 'C:\Linx Program Files\Linx Framework 6.0.0' }),
    [string] $OutRoot = 'C:\Linx Workspace\out\toPublish',
    [switch] $SkipBuild,
    [switch] $SkipHeavySeed
)

$ErrorActionPreference = 'Stop'
$env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
            [System.Environment]::GetEnvironmentVariable('Path', 'User')
$env:SKIP_PODMAN_SYNC = '1'
$env:LINX_IIS_ROOT = $FrameworkRoot

$scriptsRoot = $PSScriptRoot
$ensureTools = Join-Path $scriptsRoot 'Ensure-BuildTools.ps1'
$ensureIis = Join-Path $scriptsRoot 'Ensure-IisSiPdr.ps1'
$publish = Join-Path $RepoRoot '.vscode\stack-to-publish.ps1'
$deploy = Join-Path $RepoRoot '.vscode\deploy-to-linx-framework.ps1'

if (-not (Test-Path -LiteralPath $publish)) { throw "Missing $publish" }
if (-not (Test-Path -LiteralPath $deploy)) { throw "Missing $deploy" }

function Write-Phase([string] $Name) {
    Write-Host ("===== {0} @ {1} =====" -f $Name, (Get-Date -Format o))
}

function Reset-LastExitCode {
    # robocopy / native tools leave non-zero success codes that poison later checks
    cmd.exe /c "exit /b 0" | Out-Null
    $global:LASTEXITCODE = 0
}

function Stop-SiPdrAppPools {
    Write-Host 'Stopping IIS app pools so MSBuild/deploy can overwrite Framework DLLs'
    try {
        Import-Module WebAdministration -ErrorAction Stop
    } catch {
        Write-Warning ("WebAdministration not available to stop pools: {0}" -f $_.Exception.Message)
        return
    }
    foreach ($poolName in @('SI-PDR-Service', 'SI-PDR-Portal', 'SI-PDR-Application')) {
        try {
            $state = (Get-WebAppPoolState -Name $poolName -ErrorAction Stop).Value
            if ($state -ne 'Stopped') {
                Stop-WebAppPool -Name $poolName
            }
        } catch {
            Write-Warning ("Could not stop {0}: {1}" -f $poolName, $_.Exception.Message)
        }
    }
    $deadline = (Get-Date).AddSeconds(45)
    foreach ($poolName in @('SI-PDR-Service', 'SI-PDR-Portal', 'SI-PDR-Application')) {
        $state = $null
        do {
            try { $state = (Get-WebAppPoolState -Name $poolName -ErrorAction SilentlyContinue).Value } catch { $state = $null }
            if ($state -eq 'Stopped' -or (Get-Date) -gt $deadline) { break }
            Start-Sleep -Seconds 2
        } while ($true)
        Write-Host ("App pool {0} state={1}" -f $poolName, $state)
    }
}

function Start-SiPdrAppPools {
    # Restart-WebAppPool is a no-op on Stopped pools and leaves IIS returning 503.
    Write-Host 'Starting IIS app pools after deploy'
    try {
        Import-Module WebAdministration -ErrorAction Stop
    } catch {
        Write-Warning ("WebAdministration not available to start pools: {0}" -f $_.Exception.Message)
        return
    }
    foreach ($siteName in @('Application', 'Service', 'Portal')) {
        try {
            $site = Get-Website -Name $siteName -ErrorAction Stop
            if ($site.state -ne 'Started') {
                Start-Website -Name $siteName
                Write-Host "Started website $siteName"
            }
        } catch {
            Write-Warning ("Could not start website {0}: {1}" -f $siteName, $_.Exception.Message)
        }
    }
    foreach ($poolName in @('SI-PDR-Service', 'SI-PDR-Portal', 'SI-PDR-Application')) {
        try {
            $state = (Get-WebAppPoolState -Name $poolName -ErrorAction Stop).Value
            if ($state -eq 'Stopped') {
                Start-WebAppPool -Name $poolName
                Write-Host "Started app pool $poolName"
            } elseif ($state -ne 'Started') {
                Start-WebAppPool -Name $poolName -ErrorAction SilentlyContinue
                Write-Host "Start requested for app pool $poolName (was $state)"
            } else {
                Restart-WebAppPool -Name $poolName
                Write-Host "Restarted app pool $poolName"
            }
        } catch {
            Write-Warning ("Could not start {0}: {1}" -f $poolName, $_.Exception.Message)
        }
    }
    $deadline = (Get-Date).AddSeconds(60)
    foreach ($poolName in @('SI-PDR-Service', 'SI-PDR-Portal', 'SI-PDR-Application')) {
        $state = $null
        do {
            try { $state = (Get-WebAppPoolState -Name $poolName -ErrorAction SilentlyContinue).Value } catch { $state = $null }
            if ($state -eq 'Started' -or (Get-Date) -gt $deadline) { break }
            Start-Sleep -Seconds 2
        } while ($true)
        Write-Host ("App pool {0} state={1}" -f $poolName, $state)
        if ($state -ne 'Started') {
            throw "App pool $poolName not Started (state=$state) — IIS would return 503"
        }
    }
}

function ConvertTo-ProcessArgument {
    param([Parameter(Mandatory = $true)][string] $Value)
    # Start-Process splits on spaces unless the token is quoted.
    if ($Value -match '[\s"]') {
        return '"' + ($Value.Replace('"', '\"')) + '"'
    }
    return $Value
}

function Invoke-Ps1File {
    param(
        [Parameter(Mandatory = $true)][string] $FilePath,
        [string[]] $ArgumentList = @()
    )
    if (-not (Test-Path -LiteralPath $FilePath)) { throw "Missing $FilePath" }
    $tokens = @(
        '-NoProfile',
        '-ExecutionPolicy', 'Bypass',
        '-File', (ConvertTo-ProcessArgument $FilePath)
    )
    foreach ($a in $ArgumentList) {
        $tokens += ,(ConvertTo-ProcessArgument $a)
    }
    $argLine = ($tokens -join ' ')
    Write-Host (">> powershell.exe {0}" -f $argLine)
    # Single ArgumentList string preserves quoted paths with spaces.
    $p = Start-Process -FilePath 'powershell.exe' -ArgumentList $argLine -Wait -PassThru -NoNewWindow
    if ($null -eq $p.ExitCode) { throw "Process produced no exit code for $FilePath" }
    if ($p.ExitCode -ne 0) {
        throw ("Script failed exit={0}: {1}" -f $p.ExitCode, $FilePath)
    }
    Reset-LastExitCode
}

$swTotal = [System.Diagnostics.Stopwatch]::StartNew()
$script:smokeFailed = $false

# Capture git Binary web.configs before MSBuild. Autorizacao.BM PostBuildEvent runs
# XmlConfigMergeConsole into Main\Binary\Service\Web.config and replaces QA
# tcp:10.16.0.4 with DEV SSPI a-srv111.
$binaryConfigBackup = Join-Path $env:TEMP ('si-pdr-webconfig-' + [guid]::NewGuid().ToString('n'))
New-Item -ItemType Directory -Force -Path $binaryConfigBackup | Out-Null
$binaryWebConfigSpecs = @(
    @{ Rel = 'Main\Binary\Service\Web.config'; Name = 'Service.Web.config' },
    @{ Rel = 'Main\Binary\Portal\Web.config'; Name = 'Portal.Web.config' },
    @{ Rel = 'Main\Binary\Application\Web.config'; Name = 'Application.Web.config' }
)
foreach ($cfg in $binaryWebConfigSpecs) {
    $src = Join-Path $RepoRoot $cfg.Rel
    if (Test-Path -LiteralPath $src) {
        Copy-Item -LiteralPath $src -Destination (Join-Path $binaryConfigBackup $cfg.Name) -Force
        Write-Host ("Backed up {0}" -f $cfg.Rel)
    } else {
        Write-Warning ("Binary web.config missing at start: {0}" -f $src)
    }
}

Write-Phase 'Ensure build tools'
$sw = [System.Diagnostics.Stopwatch]::StartNew()
Invoke-Ps1File -FilePath $ensureTools
Write-Host ("Ensure build tools done in {0:n1}s" -f $sw.Elapsed.TotalSeconds)

$binaryRoot = Join-Path $RepoRoot 'Main\Binary'
Write-Phase 'Ensure IIS sites (Application:8174 Portal:8172 Service:1710)'
$sw.Restart()
$ensureArgs = @()
if ($SkipHeavySeed) { $ensureArgs += '-SkipHeavySeed' }
$ensureArgs += @('-FrameworkRoot', $FrameworkRoot, '-SeedFromBinary', $binaryRoot)
Invoke-Ps1File -FilePath $ensureIis -ArgumentList $ensureArgs
Write-Host ("Ensure IIS done in {0:n1}s" -f $sw.Elapsed.TotalSeconds)

# PostBuildEvent xcopy targets under Main\Binary (Service\Help, Library\Business View, ...)
@(
    (Join-Path $binaryRoot 'Service\bin'),
    (Join-Path $binaryRoot 'Service\Help'),
    (Join-Path $binaryRoot 'Library\Business View'),
    (Join-Path $RepoRoot 'Main\Business\Linx.Framework.BV\Linx.Framework.BV\Help For Accessing')
) | ForEach-Object {
    if (-not (Test-Path -LiteralPath $_)) {
        New-Item -ItemType Directory -Force -Path $_ | Out-Null
        Write-Host "Created $_"
    }
}
$helpPlaceholder = Join-Path $RepoRoot 'Main\Business\Linx.Framework.BV\Linx.Framework.BV\Help For Accessing\README.txt'
if (-not (Test-Path -LiteralPath $helpPlaceholder)) {
    Set-Content -LiteralPath $helpPlaceholder -Value 'CI placeholder for PostBuildEvent xcopy.' -Encoding ASCII
}

Stop-SiPdrAppPools

Write-Phase ("Publish package (stack-to-publish.ps1) SkipBuild={0}" -f [bool]$SkipBuild)
$sw.Restart()
$publishArgs = @('-OutRoot', $OutRoot, '-BaselineRoot', $FrameworkRoot)
if ($SkipBuild) { $publishArgs += '-SkipBuild' }
Invoke-Ps1File -FilePath $publish -ArgumentList $publishArgs
Write-Host ("Publish done in {0:n1}s" -f $sw.Elapsed.TotalSeconds)

Write-Phase 'Deploy to IIS (deploy-to-linx-framework.ps1)'
$sw.Restart()
$deployArgs = @(
    '-TargetRoot', $FrameworkRoot,
    '-SkipBackup',
    '-Force',
    '-SkipBinarySync'
)
if ($SkipBuild) { $deployArgs += '-KeepExistingIisDlls' }
Invoke-Ps1File -FilePath $deploy -ArgumentList $deployArgs
Write-Host ("Deploy done in {0:n1}s" -f $sw.Elapsed.TotalSeconds)

Write-Phase 'Restore Binary web.config onto IIS (QA SQL)'
$iisWebConfigMap = @{
    'Service.Web.config'     = @(
        (Join-Path $RepoRoot 'Main\Binary\Service\Web.config'),
        (Join-Path $FrameworkRoot 'Service\Web.config')
    )
    'Portal.Web.config'      = @(
        (Join-Path $RepoRoot 'Main\Binary\Portal\Web.config'),
        (Join-Path $FrameworkRoot 'Portal\Web.config')
    )
    'Application.Web.config' = @(
        (Join-Path $RepoRoot 'Main\Binary\Application\Web.config'),
        (Join-Path $FrameworkRoot 'Application\Web.config')
    )
}
foreach ($name in $iisWebConfigMap.Keys) {
    $backup = Join-Path $binaryConfigBackup $name
    if (-not (Test-Path -LiteralPath $backup)) {
        Write-Warning "No backup for $name; left current web.config"
        continue
    }
    foreach ($dst in $iisWebConfigMap[$name]) {
        $dstDir = Split-Path -Parent $dst
        if (-not (Test-Path -LiteralPath $dstDir)) {
            New-Item -ItemType Directory -Force -Path $dstDir | Out-Null
        }
        Copy-Item -LiteralPath $backup -Destination $dst -Force
        Write-Host ("Restored {0} -> {1}" -f $name, $dst)
    }
}

# Portal login -> Service AuthenticatePortal -> EF SQL. Binary defaults use SSPI to
# corporate SQL; AWS EC2 needs SI_PDR_SQL_* env (or sql-overrides.psd1) with SQL auth.
$sqlOverride = Join-Path $scriptsRoot 'Set-SiPdrSqlConnectionStrings.ps1'
if (Test-Path -LiteralPath $sqlOverride) {
    Write-Phase 'Sync Business Model dll.config from Binary'
    $bmSrc = Join-Path $RepoRoot 'Main\Binary\Library\Business Model'
    $bmDst = Join-Path $FrameworkRoot 'Library\Business Model'
    if ((Test-Path -LiteralPath $bmSrc) -and (Test-Path -LiteralPath $bmDst)) {
        Copy-Item -Path (Join-Path $bmSrc '*.dll.config') -Destination $bmDst -Force -ErrorAction SilentlyContinue
        Write-Host "Synced BM dll.config -> $bmDst"
    }

    Write-Phase 'Apply SQL / auth Service URL overrides'
    Invoke-Ps1File -FilePath $sqlOverride -ArgumentList @('-FrameworkRoot', $FrameworkRoot)
}

Write-Phase 'Start IIS app pools'
Start-SiPdrAppPools

$diagnose = Join-Path $scriptsRoot 'Diagnose-SiPdrRuntime.ps1'
if (Test-Path -LiteralPath $diagnose) {
    Write-Phase 'Diagnose IIS / SQL reachability'
    Invoke-Ps1File -FilePath $diagnose -ArgumentList @('-FrameworkRoot', $FrameworkRoot)
}

Write-Phase 'Smoke HTTP'
# Warm Portal/App first (fast). Service cold-start on t3.small can exceed 15s;
# allow up to 60s so SQL Connect Timeout / app-start errors surface as HTTP bodies.
$urls = @(
    @{ Url = 'http://127.0.0.1:8172/'; TimeoutSec = 20 },
    @{ Url = 'http://127.0.0.1:8174/'; TimeoutSec = 30 },
    @{ Url = 'http://127.0.0.1:8081/'; TimeoutSec = 15 },
    @{ Url = 'http://127.0.0.1:8080/'; TimeoutSec = 15 },
    @{ Url = 'http://127.0.0.1:1710/'; TimeoutSec = 60 },
    @{ Url = 'http://127.0.0.1:8082/'; TimeoutSec = 30 }
)
foreach ($item in $urls) {
    $url = $item.Url
    $swSmoke = [System.Diagnostics.Stopwatch]::StartNew()
    try {
        $resp = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec $item.TimeoutSec
        Write-Host ("OK {0} status={1} len={2} in {3:n1}s" -f $url, [int]$resp.StatusCode, ($resp.RawContentLength), $swSmoke.Elapsed.TotalSeconds)
    } catch {
        $msg = $_.Exception.Message
        # Surface ASP.NET yellow-screen / JSON body for Service 500s (login AuthenticatePortal).
        try {
            if ($_.Exception.Response) {
                $stream = $_.Exception.Response.GetResponseStream()
                if ($stream) {
                    $reader = New-Object System.IO.StreamReader($stream)
                    $body = $reader.ReadToEnd()
                    if ($body) {
                        $body = ($body -replace '\s+', ' ').Trim()
                        if ($body.Length -gt 800) { $body = $body.Substring(0, 800) + '...' }
                        $msg = "$msg | body=$body"
                    }
                }
            }
        } catch { }
        Write-Warning ("Smoke failed for {0} after {1:n1}s: {2}" -f $url, $swSmoke.Elapsed.TotalSeconds, $msg)
        if ($url -match ':8172/|:8081/') {
            $script:smokeFailed = $true
        }
    }
}

# Portal login E2E: form POST -> Account/Login -> Service AuthenticatePortal.
# Optional env SI_PDR_SMOKE_USER / SI_PDR_SMOKE_PASSWORD (defaults match QA test user).
$smokeUser = if ($env:SI_PDR_SMOKE_USER) { $env:SI_PDR_SMOKE_USER } else { 'desenv.franqueado' }
$smokePass = if ($env:SI_PDR_SMOKE_PASSWORD) { $env:SI_PDR_SMOKE_PASSWORD } else { '@!2026Linx!@' }
if ($smokeUser -and $smokePass) {
    Write-Phase 'Smoke Portal login'
    $loginUrl = 'http://127.0.0.1:8172/Account/Login'
    $swLogin = [System.Diagnostics.Stopwatch]::StartNew()
    try {
        $session = New-Object Microsoft.PowerShell.Commands.WebRequestSession
        $form = @{
            UserName        = $smokeUser
            Password        = $smokePass
            RememberMe      = 'false'
            RecoverPassword = 'false'
            ShowEnvironments = 'false'
        }
        $resp = Invoke-WebRequest -Uri $loginUrl -Method POST -Body $form -WebSession $session `
            -UseBasicParsing -TimeoutSec 90 -MaximumRedirection 5
        $elapsed = $swLogin.Elapsed.TotalSeconds
        $body = if ($resp.Content) { ($resp.Content -replace '\s+', ' ').Trim() } else { '' }
        $hasAuthCookie = $false
        if ($session.Cookies) {
            foreach ($c in $session.Cookies.GetCookies([Uri]'http://127.0.0.1:8172/')) {
                if ($c.Name -like '*.ASPXAUTH*' -or $c.Name -like '*ASPXAUTH*') { $hasAuthCookie = $true }
            }
        }
        $looksLikeLoginError = ($body -match '(?i)validation-summary-errors|field-validation-error') -and ($resp.BaseResponse.ResponseUri.AbsolutePath -match '(?i)/Account/Login')
        if ([int]$resp.StatusCode -ge 200 -and [int]$resp.StatusCode -lt 400 -and -not $looksLikeLoginError) {
            Write-Host ("OK Portal login user={0} status={1} authCookie={2} in {3:n1}s uri={4}" -f `
                $smokeUser, [int]$resp.StatusCode, $hasAuthCookie, $elapsed, $resp.BaseResponse.ResponseUri.AbsoluteUri)
            if ($hasAuthCookie) {
                $homeResp = Invoke-WebRequest -Uri 'http://127.0.0.1:8172/Home/Index' -WebSession $session `
                    -UseBasicParsing -TimeoutSec 90 -MaximumRedirection 5
                $homeUri = $homeResp.BaseResponse.ResponseUri.AbsoluteUri
                $homeBody = if ($homeResp.Content) { ($homeResp.Content -replace '\s+', ' ').Trim() } else { '' }
                $mfaHint = if ($homeUri -match '(?i)/Mfa/' -or $homeBody -match '(?i)duas etapas|QR Code MFA|c[oó]digo') { ' mfa=yes' } else { ' mfa=no' }
                Write-Host ("OK Portal home-after-login status={0} uri={1}{2}" -f [int]$homeResp.StatusCode, $homeUri, $mfaHint)
            } else {
                Write-Warning 'Portal login returned 200 without ASPXAUTH — SQL/VPN may still be unreachable.'
            }
        } else {
            $snippet = $body
            if ($snippet.Length -gt 500) { $snippet = $snippet.Substring(0, 500) + '...' }
            Write-Warning ("Portal login failed user={0} status={1} authCookie={2} in {3:n1}s uri={4} body={5}" -f `
                $smokeUser, [int]$resp.StatusCode, $hasAuthCookie, $elapsed, $resp.BaseResponse.ResponseUri.AbsoluteUri, $snippet)
        }
    } catch {
        $msg = $_.Exception.Message
        try {
            if ($_.Exception.Response) {
                $stream = $_.Exception.Response.GetResponseStream()
                if ($stream) {
                    $reader = New-Object System.IO.StreamReader($stream)
                    $bodyEx = $reader.ReadToEnd()
                    if ($bodyEx) {
                        $bodyEx = ($bodyEx -replace '\s+', ' ').Trim()
                        if ($bodyEx.Length -gt 800) { $bodyEx = $bodyEx.Substring(0, 800) + '...' }
                        $msg = "$msg | body=$bodyEx"
                    }
                }
            }
        } catch { }
        Write-Warning ("Portal login exception user={0} after {1:n1}s: {2}" -f $smokeUser, $swLogin.Elapsed.TotalSeconds, $msg)
        if ($_.Exception.Response) {
            $script:smokeFailed = $true
        }
    }
} else {
    Write-Host 'Smoke Portal login skipped (set SI_PDR_SMOKE_USER / SI_PDR_SMOKE_PASSWORD to enable).'
}

# Re-run diagnostics after smoke so Event Log captures Service start failures.
if (Test-Path -LiteralPath $diagnose) {
    Write-Phase 'Diagnose after smoke'
    Invoke-Ps1File -FilePath $diagnose -ArgumentList @('-FrameworkRoot', $FrameworkRoot)
}

if ($script:smokeFailed) {
    Write-Host ("SI-PDR AWS pipeline finished with Portal smoke failures in {0:n1}s. IIS root: {1}" -f $swTotal.Elapsed.TotalSeconds, $FrameworkRoot)
    exit 1
}

Write-Host ("SI-PDR AWS pipeline succeeded in {0:n1}s total." -f $swTotal.Elapsed.TotalSeconds)
Write-Host "IIS root: $FrameworkRoot"
Write-Host 'Sites: Application http://<host>:8174 (also :8080)  Portal http://<host>:8172 (also :8081)  Service http://<host>:1710 (also :8082)'
exit 0
