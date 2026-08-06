<#
.SYNOPSIS
  Build, publish, and deploy SI-PDR (Application / Service / Portal) on the AWS Windows host.
#>
[CmdletBinding()]
param(
    [string] $RepoRoot = (Get-Location).Path,
    [string] $FrameworkRoot = $(if ($env:LINX_IIS_ROOT) { $env:LINX_IIS_ROOT } else { 'C:\Linx Program Files\Linx Framework 6.0.0' }),
    [string] $OutRoot = 'C:\Linx Workspace\out\toPublish',
    [switch] $SkipBuild
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

Write-Host '===== Ensure build tools ====='
Invoke-Ps1File -FilePath $ensureTools

$binaryRoot = Join-Path $RepoRoot 'Main\Binary'
Write-Host '===== Ensure IIS sites (Application:8080 Portal:8081 Service:1710+8082) ====='
Invoke-Ps1File -FilePath $ensureIis -ArgumentList @(
    '-FrameworkRoot', $FrameworkRoot,
    '-SeedFromBinary', $binaryRoot
)

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

Write-Host '===== Publish package (stack-to-publish.ps1) ====='
$publishArgs = @('-OutRoot', $OutRoot, '-BaselineRoot', $FrameworkRoot)
if ($SkipBuild) { $publishArgs += '-SkipBuild' }
Invoke-Ps1File -FilePath $publish -ArgumentList $publishArgs

Write-Host '===== Deploy to IIS (deploy-to-linx-framework.ps1) ====='
Invoke-Ps1File -FilePath $deploy -ArgumentList @(
    '-TargetRoot', $FrameworkRoot,
    '-SkipBackup',
    '-Force',
    '-SkipBinarySync'
)

Write-Host '===== Smoke HTTP ====='
$urls = @(
    'http://127.0.0.1:8080/',
    'http://127.0.0.1:8081/',
    'http://127.0.0.1:1710/',
    'http://127.0.0.1:8082/'
)
foreach ($url in $urls) {
    try {
        $resp = Invoke-WebRequest -Uri $url -UseBasicParsing -TimeoutSec 30
        Write-Host ("OK {0} status={1} len={2}" -f $url, [int]$resp.StatusCode, ($resp.RawContentLength))
    } catch {
        Write-Warning ("Smoke failed for {0}: {1}" -f $url, $_.Exception.Message)
    }
}

Write-Host 'SI-PDR AWS pipeline succeeded.'
Write-Host "IIS root: $FrameworkRoot"
Write-Host 'Sites: Application http://<host>:8080  Portal http://<host>:8081  Service http://<host>:1710 (also :8082)'
exit 0
