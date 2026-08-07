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

Write-Phase ("Publish package (stack-to-publish.ps1) SkipBuild={0}" -f [bool]$SkipBuild)
$sw.Restart()
$publishArgs = @('-OutRoot', $OutRoot, '-BaselineRoot', $FrameworkRoot)
if ($SkipBuild) { $publishArgs += '-SkipBuild' }
Invoke-Ps1File -FilePath $publish -ArgumentList $publishArgs
Write-Host ("Publish done in {0:n1}s" -f $sw.Elapsed.TotalSeconds)

Write-Phase 'Deploy to IIS (deploy-to-linx-framework.ps1)'
$sw.Restart()
Invoke-Ps1File -FilePath $deploy -ArgumentList @(
    '-TargetRoot', $FrameworkRoot,
    '-SkipBackup',
    '-Force',
    '-SkipBinarySync'
)
Write-Host ("Deploy done in {0:n1}s" -f $sw.Elapsed.TotalSeconds)

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

Write-Phase 'Smoke HTTP'
# Prefer working aliases first (short timeout). Primary ports that often hang are last.
$urls = @(
    @{ Url = 'http://127.0.0.1:8080/'; TimeoutSec = 15 },
    @{ Url = 'http://127.0.0.1:8081/'; TimeoutSec = 15 },
    @{ Url = 'http://127.0.0.1:8082/'; TimeoutSec = 15 },
    @{ Url = 'http://127.0.0.1:8174/'; TimeoutSec = 8 },
    @{ Url = 'http://127.0.0.1:8172/'; TimeoutSec = 8 },
    @{ Url = 'http://127.0.0.1:1710/'; TimeoutSec = 8 }
)
foreach ($item in $urls) {
    $url = $item.Url
    try {
        $resp = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec $item.TimeoutSec
        Write-Host ("OK {0} status={1} len={2}" -f $url, [int]$resp.StatusCode, ($resp.RawContentLength))
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
                        if ($body.Length -gt 400) { $body = $body.Substring(0, 400) + '...' }
                        $msg = "$msg | body=$body"
                    }
                }
            }
        } catch { }
        Write-Warning ("Smoke failed for {0}: {1}" -f $url, $msg)
    }
}

Write-Host ("SI-PDR AWS pipeline succeeded in {0:n1}s total." -f $swTotal.Elapsed.TotalSeconds)
Write-Host "IIS root: $FrameworkRoot"
Write-Host 'Sites: Application http://<host>:8174 (also :8080)  Portal http://<host>:8172 (also :8081)  Service http://<host>:1710 (also :8082)'
exit 0
