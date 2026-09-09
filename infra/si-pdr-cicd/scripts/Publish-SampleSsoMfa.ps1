<#
.SYNOPSIS
  Copy MFA/SSO docs and the .NET desktop sample to C:\Sample-SSO-MFA on the AWS Windows host.

.DESCRIPTION
  Idempotent drop folder for RDP users. The sample is made standalone by copying
  Cryptography.cs next to the project (skip_build packages do not include Main/Common).
  Never copies secrets, passwords, or .sso-client-secret.
#>
[CmdletBinding()]
param(
    [string] $RepoRoot = (Get-Location).Path,
    [string] $Destination = $(if ($env:SAMPLE_SSO_MFA_ROOT) { $env:SAMPLE_SSO_MFA_ROOT } else { 'C:\Sample-SSO-MFA' })
)

$ErrorActionPreference = 'Stop'

function Test-BlockedName {
    param([string] $Name)
    $lower = $Name.ToLowerInvariant()
    if ($lower -eq '.sso-client-secret') { return $true }
    if ($lower -eq '.env') { return $true }
    return $false
}

function Copy-RepoFile {
    param(
        [Parameter(Mandatory = $true)][string] $RelativeSource,
        [Parameter(Mandatory = $true)][string] $RelativeDest
    )
    $src = Join-Path $RepoRoot $RelativeSource
    if (-not (Test-Path -LiteralPath $src)) {
        throw "Missing source file: $src"
    }
    $leaf = Split-Path -Leaf $src
    if (Test-BlockedName -Name $leaf) {
        throw "Refusing to copy blocked name: $leaf"
    }
    $dst = Join-Path $Destination $RelativeDest
    $dir = Split-Path -Parent $dst
    if (-not (Test-Path -LiteralPath $dir)) {
        New-Item -ItemType Directory -Force -Path $dir | Out-Null
    }
    Copy-Item -LiteralPath $src -Destination $dst -Force
    Write-Host ("Copied {0} -> {1}" -f $RelativeSource, $RelativeDest)
}

if (-not (Test-Path -LiteralPath $RepoRoot)) {
    throw "RepoRoot missing: $RepoRoot"
}

$sampleSrc = Join-Path (Join-Path $RepoRoot 'samples') 'LinxUxAuthDesktopPoc'
if (-not (Test-Path -LiteralPath $sampleSrc)) {
    throw "Desktop sample missing: $sampleSrc"
}

$cryptoRel = Join-Path 'Main' (Join-Path 'Common' (Join-Path 'Linx.Tools.Library' (Join-Path 'Desktop' (Join-Path 'Linx.Desktop.Tools' 'Cryptography.cs'))))
$cryptoSrc = Join-Path $RepoRoot $cryptoRel
if (-not (Test-Path -LiteralPath $cryptoSrc)) {
    throw "Cryptography.cs missing (needed for standalone sample): $cryptoSrc"
}

Write-Host ("Publishing Sample SSO/MFA -> {0}" -f $Destination)

if (Test-Path -LiteralPath $Destination) {
    Get-ChildItem -LiteralPath $Destination -Force | Remove-Item -Recurse -Force
} else {
    New-Item -ItemType Directory -Force -Path $Destination | Out-Null
}
New-Item -ItemType Directory -Force -Path $Destination | Out-Null

$docPairs = @(
    @{ Src = (Join-Path 'docs' 'login-mfa-sso-usuario.md'); Dest = (Join-Path 'docs' 'login-mfa-sso-usuario.md') },
    @{ Src = (Join-Path 'docs' 'login-mfa-sso-api.md'); Dest = (Join-Path 'docs' 'login-mfa-sso-api.md') },
    @{ Src = (Join-Path 'docs' 'login-mfa-sso-desktop-dotnet.md'); Dest = (Join-Path 'docs' 'login-mfa-sso-desktop-dotnet.md') },
    @{ Src = (Join-Path 'docs' 'si-pdr-portal-sso.md'); Dest = (Join-Path 'docs' 'si-pdr-portal-sso.md') },
    @{ Src = (Join-Path 'docs' 'sso-azure-ad-msal-guide.md'); Dest = (Join-Path 'docs' 'sso-azure-ad-msal-guide.md') }
)
foreach ($pair in $docPairs) {
    Copy-RepoFile -RelativeSource $pair.Src -RelativeDest $pair.Dest
}

$ruleSrc = Join-Path (Join-Path '.cursor' 'rules') 'mfa-totp-framework-ux.mdc'
$ruleFull = Join-Path $RepoRoot $ruleSrc
if (Test-Path -LiteralPath $ruleFull) {
    Copy-RepoFile -RelativeSource $ruleSrc -RelativeDest (Join-Path 'docs' 'mfa-totp-framework-ux.mdc')
} else {
    Write-Warning "Cursor MFA rule not found; skipped: $ruleFull"
}

$sampleDst = Join-Path $Destination 'LinxUxAuthDesktopPoc'
New-Item -ItemType Directory -Force -Path $sampleDst | Out-Null
$prefixLen = $sampleSrc.TrimEnd('\', '/').Length
Get-ChildItem -LiteralPath $sampleSrc -Recurse -Force | Where-Object { -not $_.PSIsContainer } | ForEach-Object {
    $full = $_.FullName
    if ($full -match '[\\/](bin|obj)([\\/]|$)') { return }
    if (Test-BlockedName -Name $_.Name) {
        Write-Warning ("Skipped blocked sample file: {0}" -f $_.Name)
        return
    }
    $rel = $full.Substring($prefixLen).TrimStart('\', '/')
    $out = Join-Path $sampleDst $rel
    $outDir = Split-Path -Parent $out
    if (-not (Test-Path -LiteralPath $outDir)) {
        New-Item -ItemType Directory -Force -Path $outDir | Out-Null
    }
    Copy-Item -LiteralPath $full -Destination $out -Force
}
Write-Host "Copied samples/LinxUxAuthDesktopPoc (excluding bin/obj)"

$linxDir = Join-Path $sampleDst 'Linx'
New-Item -ItemType Directory -Force -Path $linxDir | Out-Null
Copy-Item -LiteralPath $cryptoSrc -Destination (Join-Path $linxDir 'Cryptography.cs') -Force
Write-Host 'Copied Cryptography.cs -> LinxUxAuthDesktopPoc\Linx\Cryptography.cs (standalone)'

$sampleRunDir = Join-Path $Destination 'LinxUxAuthDesktopPoc'
$readme = @"
Sample SSO + MFA (Linx UX)
==========================

Pasta publicada pela pipeline SI-PDR AWS (IIS) neste host.
Não contém senhas, client secrets nem tickets MFA.

Layout
------
docs\                          Guias MFA/SSO (usuario, API, desktop .NET)
LinxUxAuthDesktopPoc\          POC console .NET 8 (senha ou SSO + TOTP)
LinxUxAuthDesktopPoc\Linx\     Cryptography.cs (mesma classe do Portal)

Service local neste host
------------------------
http://localhost:1710/

Como rodar o sample (requer .NET 8 SDK)
----------------------------------------
cd $sampleRunDir
dotnet restore
dotnet run -- --libs
dotnet run -- --service http://localhost:1710/ --user SEU_LOGIN --password SUA_SENHA

SSO desktop usa app Entra "Mobile and desktop" (público). Não use o client secret do Portal.

Publicado: $(Get-Date -Format o)
"@

$utf8Bom = New-Object System.Text.UTF8Encoding $true
[System.IO.File]::WriteAllText((Join-Path $Destination 'README.txt'), $readme.Replace("`n", "`r`n"), $utf8Bom)

$manifestLines = New-Object System.Collections.Generic.List[string]
$manifestLines.Add("SAMPLE_SSO_MFA dest=$Destination")
$manifestLines.Add("publishedUtc=$((Get-Date).ToUniversalTime().ToString('o'))")
$manifestLines.Add("repoRoot=$RepoRoot")
$manifestLines.Add('')
$fileCount = 0
Get-ChildItem -LiteralPath $Destination -Recurse -Force | Where-Object { -not $_.PSIsContainer } | Sort-Object FullName | ForEach-Object {
    $rel = $_.FullName.Substring($Destination.Length).TrimStart('\', '/')
    $manifestLines.Add(("{0}`t{1}" -f $rel, $_.Length))
    $fileCount++
    Write-Host ("  {0} ({1} bytes)" -f $rel, $_.Length)
}
[System.IO.File]::WriteAllText((Join-Path $Destination 'MANIFEST.txt'), (($manifestLines -join "`r`n") + "`r`n"), $utf8Bom)

Write-Host ("SAMPLE_SSO_MFA_PUBLISHED dest={0} files={1}" -f $Destination, $fileCount)
exit 0
