<#
.SYNOPSIS
  Evidências dos 3 pontos de hardening: CORS allowlist, erro sem stack, headers sem fingerprint .NET.

.PARAMETER ServiceBaseUrl
  Ex.: http://18.231.155.145:8082

.PARAMETER AllowedOrigin
  Origin que deve ser aceita (default: Application SI-PDR).

.PARAMETER DeniedOrigin
  Origin que deve ser rejeitada.

.PARAMETER OutDir
  Pasta para gravar evidências (headers.txt, cors-*.txt, error-500.json, summary.csv).
#>
[CmdletBinding()]
param(
    [string] $ServiceBaseUrl = 'http://127.0.0.1:8082',
    [string] $AllowedOrigin = 'http://18.231.155.145:8080',
    [string] $DeniedOrigin = 'https://evil.example',
    [string] $OutDir = '.\security-hardening-evidence',
    [int] $TimeoutSec = 60
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

$base = $ServiceBaseUrl.TrimEnd('/')
if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Path $OutDir -Force | Out-Null }

$results = New-Object System.Collections.Generic.List[object]

function Add-Result {
    param($Id, $Ok, $Detail)
    $results.Add([pscustomobject]@{ CaseId = $Id; Passed = [bool]$Ok; Detail = $Detail }) | Out-Null
    $mark = if ($Ok) { 'PASS' } else { 'FAIL' }
    Write-Host ("[{0}] {1} — {2}" -f $mark, $Id, $Detail)
}

function Get-WebExBody {
    param($Exception)
    try {
        if ($Exception.Response) {
            $stream = $Exception.Response.GetResponseStream()
            if ($stream) {
                $reader = New-Object System.IO.StreamReader($stream)
                try { return $reader.ReadToEnd() } finally { $reader.Dispose() }
            }
        }
    } catch {}
    return ''
}

Write-Host ("Security hardening evidence against {0}" -f $base)
Write-Host ""

# --- 1) Headers (fingerprint) ---
try {
    $headersResp = Invoke-WebRequest -Uri "$base/SecurityHardening/Ping" -Method GET -UseBasicParsing -TimeoutSec $TimeoutSec
    $hdrText = ($headersResp.Headers.GetEnumerator() | ForEach-Object { "{0}: {1}" -f $_.Key, ($_.Value -join ',') }) -join "`n"
    Set-Content -Path (Join-Path $OutDir 'headers-response.txt') -Value $hdrText -Encoding UTF8

    $joined = $hdrText
    $hasPowered = $joined -match '(?i)X-Powered-By'
    $hasAspNet = $joined -match '(?i)X-AspNet-Version'
    $hasServerIis = $joined -match '(?i)Server:\s*Microsoft-IIS'
    $okHeaders = (-not $hasPowered) -and (-not $hasAspNet)
    Add-Result -Id 'HEADERS-NO-DOTNET' -Ok $okHeaders -Detail ("X-Powered-By={0}; X-AspNet-Version={1}; Server-IIS={2}" -f $hasPowered, $hasAspNet, $hasServerIis)
}
catch {
    Add-Result -Id 'HEADERS-NO-DOTNET' -Ok $false -Detail $_.Exception.Message
}

# --- 2) CORS allow ---
try {
    $req = [System.Net.HttpWebRequest]::Create("$base/SecurityHardening/Ping")
    $req.Method = 'GET'
    $req.Headers.Add('Origin', $AllowedOrigin)
    $req.Timeout = $TimeoutSec * 1000
    $resp = $req.GetResponse()
    $allow = $resp.Headers['Access-Control-Allow-Origin']
    $cred = $resp.Headers['Access-Control-Allow-Credentials']
    $corsAllowText = ("Origin={0}`nAccess-Control-Allow-Origin={1}`nAccess-Control-Allow-Credentials={2}`nStatus={3}" -f $AllowedOrigin, $allow, $cred, [int]$resp.StatusCode)
    Set-Content -Path (Join-Path $OutDir 'cors-allowed.txt') -Value $corsAllowText -Encoding UTF8
    $resp.Close()
    $ok = ($allow -eq $AllowedOrigin)
    Add-Result -Id 'CORS-ALLOWED-ORIGIN' -Ok $ok -Detail $corsAllowText.Replace("`n", ' | ')
}
catch {
    Add-Result -Id 'CORS-ALLOWED-ORIGIN' -Ok $false -Detail $_.Exception.Message
}

# --- 3) CORS deny ---
try {
    $req = [System.Net.HttpWebRequest]::Create("$base/SecurityHardening/Ping")
    $req.Method = 'GET'
    $req.Headers.Add('Origin', $DeniedOrigin)
    $req.Timeout = $TimeoutSec * 1000
    $resp = $req.GetResponse()
    $allow = $resp.Headers['Access-Control-Allow-Origin']
    $corsDenyText = ("Origin={0}`nAccess-Control-Allow-Origin={1}`nStatus={2}" -f $DeniedOrigin, $allow, [int]$resp.StatusCode)
    Set-Content -Path (Join-Path $OutDir 'cors-denied.txt') -Value $corsDenyText -Encoding UTF8
    $resp.Close()
    $ok = [string]::IsNullOrEmpty($allow)
    Add-Result -Id 'CORS-DENIED-ORIGIN' -Ok $ok -Detail $corsDenyText.Replace("`n", ' | ')
}
catch [System.Net.WebException] {
    $ex = $_.Exception
    $status = if ($ex.Response) { [int]$ex.Response.StatusCode } else { 0 }
    $allow = if ($ex.Response) { $ex.Response.Headers['Access-Control-Allow-Origin'] } else { '' }
    $corsDenyText = ("Origin={0}`nAccess-Control-Allow-Origin={1}`nStatus={2}`nNote=WebException" -f $DeniedOrigin, $allow, $status)
    Set-Content -Path (Join-Path $OutDir 'cors-denied.txt') -Value $corsDenyText -Encoding UTF8
    $ok = [string]::IsNullOrEmpty($allow)
    Add-Result -Id 'CORS-DENIED-ORIGIN' -Ok $ok -Detail $corsDenyText.Replace("`n", ' | ')
}
catch {
    Add-Result -Id 'CORS-DENIED-ORIGIN' -Ok $false -Detail $_.Exception.Message
}

# --- 4) CORS preflight deny ---
try {
    $req = [System.Net.HttpWebRequest]::Create("$base/SecurityHardening/Ping")
    $req.Method = 'OPTIONS'
    $req.Headers.Add('Origin', $DeniedOrigin)
    $req.Headers.Add('Access-Control-Request-Method', 'GET')
    $req.Timeout = $TimeoutSec * 1000
    try {
        $resp = $req.GetResponse()
        $status = [int]$resp.StatusCode
        $allow = $resp.Headers['Access-Control-Allow-Origin']
        $resp.Close()
    }
    catch [System.Net.WebException] {
        $status = if ($_.Exception.Response) { [int]$_.Exception.Response.StatusCode } else { 0 }
        $allow = if ($_.Exception.Response) { $_.Exception.Response.Headers['Access-Control-Allow-Origin'] } else { '' }
    }
    $txt = ("OPTIONS Origin={0} Status={1} Allow-Origin={2}" -f $DeniedOrigin, $status, $allow)
    Set-Content -Path (Join-Path $OutDir 'cors-preflight-denied.txt') -Value $txt -Encoding UTF8
    $ok = ($status -eq 403) -or [string]::IsNullOrEmpty($allow)
    Add-Result -Id 'CORS-PREFLIGHT-DENIED' -Ok $ok -Detail $txt
}
catch {
    Add-Result -Id 'CORS-PREFLIGHT-DENIED' -Ok $false -Detail $_.Exception.Message
}

# --- 5) Error 500 generic (no stack) ---
try {
    $body = ''
    $status = 0
    try {
        $resp = Invoke-WebRequest -Uri "$base/SecurityHardening/Throw" -Method GET -UseBasicParsing -TimeoutSec $TimeoutSec
        $status = [int]$resp.StatusCode
        $body = [string]$resp.Content
    }
    catch [System.Net.WebException] {
        if ($_.Exception.Response) {
            $status = [int]$_.Exception.Response.StatusCode
            $body = Get-WebExBody -Exception $_.Exception
        }
        else { throw }
    }

    Set-Content -Path (Join-Path $OutDir 'error-500.json') -Value $body -Encoding UTF8
    $hasStack = $body -match '(?i)(at\s+\w+\.|StackTrace|InvalidOperationException|System\.Web)'
    $hasGeneric = $body -match '(?i)erro interno|CorrelationId'
    $ok = ($status -eq 500) -and (-not $hasStack) -and $hasGeneric
    Add-Result -Id 'ERROR-NO-STACK' -Ok $ok -Detail ("status={0}; hasStack={1}; hasGeneric={2}; body={3}" -f $status, $hasStack, $hasGeneric, ($(if ($body.Length -gt 180) { $body.Substring(0, 180) + '...' } else { $body })))
}
catch {
    Add-Result -Id 'ERROR-NO-STACK' -Ok $false -Detail $_.Exception.Message
}

Write-Host ''
$pass = @($results | Where-Object { $_.Passed }).Count
$fail = @($results | Where-Object { -not $_.Passed }).Count
Write-Host ("Summary: PASS={0} FAIL={1} TOTAL={2}" -f $pass, $fail, $results.Count)
$csv = Join-Path $OutDir 'summary.csv'
$results | Export-Csv -Path $csv -NoTypeInformation -Encoding UTF8
Write-Host ("CSV: {0}" -f $csv)

if ($fail -gt 0) { exit 1 } else { exit 0 }
