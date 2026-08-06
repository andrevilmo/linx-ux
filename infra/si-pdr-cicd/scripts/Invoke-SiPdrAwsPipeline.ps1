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

Write-Host '===== Ensure build tools ====='
& $ensureTools

$binaryRoot = Join-Path $RepoRoot 'Main\Binary'
Write-Host '===== Ensure IIS sites (Application:8080 Portal:8081 Service:1710+8082) ====='
& $ensureIis -FrameworkRoot $FrameworkRoot -SeedFromBinary $binaryRoot

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
$publishArgs = @{
    OutRoot = $OutRoot
    BaselineRoot = $FrameworkRoot
}
if ($SkipBuild) { $publishArgs.SkipBuild = $true }
& $publish @publishArgs
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

Write-Host '===== Deploy to IIS (deploy-to-linx-framework.ps1) ====='
& $deploy -TargetRoot $FrameworkRoot -SkipBackup -Force -SkipBinarySync
if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }

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
