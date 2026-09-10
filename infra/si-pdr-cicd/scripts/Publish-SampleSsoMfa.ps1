<#
.SYNOPSIS
  Copy MFA/SSO docs and compile/publish the .NET desktop sample to C:\Sample-SSO-MFA.

.DESCRIPTION
  Idempotent drop folder for RDP users. Prefers a CI-built win-x64 self-contained
  exe (samples/LinxUxAuthDesktopPoc/publish-win-x64). If that folder is missing,
  runs dotnet publish on the host. Never copies secrets or .sso-client-secret.
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

function Find-DotNetExe {
    $cmd = Get-Command dotnet -ErrorAction SilentlyContinue
    if ($cmd -and $cmd.Source) { return $cmd.Source }
    foreach ($c in @(
            'C:\Program Files\dotnet\dotnet.exe',
            'C:\Program Files (x86)\dotnet\dotnet.exe'
        )) {
        if (Test-Path -LiteralPath $c) { return $c }
    }
    return $null
}

function Test-DotNet8Sdk {
    param([string] $DotNetExe)
    $sdks = & $DotNetExe --list-sdks 2>$null
    if (-not $sdks) { return $false }
    return [bool]($sdks | Where-Object { $_ -match '^8\.' })
}

function Get-DotNet8Sdk {
    $dotnet = Find-DotNetExe
    if ($dotnet -and (Test-DotNet8Sdk -DotNetExe $dotnet)) {
        return $dotnet
    }
    if (-not (Get-Command choco -ErrorAction SilentlyContinue)) {
        throw 'dotnet 8 SDK not found and Chocolatey is not available to install it.'
    }
    Write-Host 'Installing .NET 8 SDK (dotnet-8.0-sdk) for sample publish'
    & choco install -y dotnet-8.0-sdk --no-progress
    if ($LASTEXITCODE -ne 0) {
        throw "choco install dotnet-8.0-sdk failed exit=$LASTEXITCODE"
    }
    $env:Path = [System.Environment]::GetEnvironmentVariable('Path', 'Machine') + ';' +
                [System.Environment]::GetEnvironmentVariable('Path', 'User')
    $dotnet = Find-DotNetExe
    if (-not $dotnet -or -not (Test-DotNet8Sdk -DotNetExe $dotnet)) {
        throw 'dotnet 8 SDK still missing after Chocolatey install.'
    }
    return $dotnet
}

function Invoke-WinX64Publish {
    param(
        [Parameter(Mandatory = $true)][string] $ProjectFile,
        [Parameter(Mandatory = $true)][string] $OutputDir
    )
    $dotnet = Get-DotNet8Sdk
    if (-not (Test-Path -LiteralPath $OutputDir)) {
        New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null
    }
    Write-Host ("dotnet publish {0} -> {1} (win-x64 self-contained)" -f $ProjectFile, $OutputDir)
    $publishArgs = @(
        'publish', $ProjectFile,
        '-c', 'Release',
        '-r', 'win-x64',
        '--self-contained', 'true',
        '-p:PublishSingleFile=true',
        '-p:IncludeNativeLibrariesForSelfExtract=true',
        '-p:EnableCompressionInSingleFile=true',
        '-p:DebugType=None',
        '-p:DebugSymbols=false',
        '-o', $OutputDir
    )
    & $dotnet $publishArgs
    if ($LASTEXITCODE -ne 0) {
        throw ("dotnet publish failed exit={0}: {1}" -f $LASTEXITCODE, $ProjectFile)
    }
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

$sampleDst = Join-Path $Destination 'src'
New-Item -ItemType Directory -Force -Path $sampleDst | Out-Null
$prefixLen = $sampleSrc.TrimEnd('\', '/').Length
Get-ChildItem -LiteralPath $sampleSrc -Recurse -Force | Where-Object { -not $_.PSIsContainer } | ForEach-Object {
    $full = $_.FullName
    if ($full -match '[\\/](bin|obj|publish-win-x64)([\\/]|$)') { return }
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
Write-Host "Copied sample source -> src\ (excluding bin/obj/publish-win-x64)"

$linxDir = Join-Path $sampleDst 'Linx'
New-Item -ItemType Directory -Force -Path $linxDir | Out-Null
Copy-Item -LiteralPath $cryptoSrc -Destination (Join-Path $linxDir 'Cryptography.cs') -Force
Write-Host 'Copied Cryptography.cs -> src\Linx\Cryptography.cs (standalone)'

$exeName = 'LinxUxAuthDesktopPoc.exe'
$prebuilt = Join-Path $sampleSrc 'publish-win-x64'
$prebuiltExe = Join-Path $prebuilt $exeName
if (Test-Path -LiteralPath $prebuiltExe) {
    Write-Host ("Using CI prebuilt {0}" -f $prebuilt)
    Get-ChildItem -LiteralPath $prebuilt -Force | Where-Object { -not $_.PSIsContainer } | ForEach-Object {
        if (Test-BlockedName -Name $_.Name) { return }
        Copy-Item -LiteralPath $_.FullName -Destination (Join-Path $Destination $_.Name) -Force
        Write-Host ("Copied prebuilt {0} ({1} bytes)" -f $_.Name, $_.Length)
    }
} else {
    Write-Host 'No CI prebuilt publish-win-x64; compiling on this host'
    $csprojInSrc = Join-Path $sampleDst 'LinxUxAuthDesktopPoc.csproj'
    if (-not (Test-Path -LiteralPath $csprojInSrc)) {
        throw "Sample csproj missing for host publish: $csprojInSrc"
    }
    $buildRoot = Join-Path ([System.IO.Path]::GetTempPath()) ('sample-sso-mfa-' + [guid]::NewGuid().ToString('n'))
    Copy-Item -LiteralPath $sampleDst -Destination $buildRoot -Recurse -Force
    $csproj = Join-Path $buildRoot 'LinxUxAuthDesktopPoc.csproj'
    $hostOut = Join-Path $buildRoot 'out'
    try {
        Invoke-WinX64Publish -ProjectFile $csproj -OutputDir $hostOut
        Get-ChildItem -LiteralPath $hostOut -Force | Where-Object { -not $_.PSIsContainer } | ForEach-Object {
            Copy-Item -LiteralPath $_.FullName -Destination (Join-Path $Destination $_.Name) -Force
            Write-Host ("Copied host-built {0} ({1} bytes)" -f $_.Name, $_.Length)
        }
    } finally {
        Remove-Item -LiteralPath $buildRoot -Recurse -Force -ErrorAction SilentlyContinue
    }
}

$exePath = Join-Path $Destination $exeName
if (-not (Test-Path -LiteralPath $exePath)) {
    throw "Compiled sample missing: $exePath"
}
$exeSize = (Get-Item -LiteralPath $exePath).Length
Write-Host ("SAMPLE_SSO_MFA_EXE path={0} bytes={1}" -f $exePath, $exeSize)

$readme = @"
Sample SSO + MFA (Linx UX)
==========================

Pasta publicada pela pipeline SI-PDR AWS (IIS) neste host.
Não contém senhas, client secrets nem tickets MFA.

Layout
------
LinxUxAuthDesktopPoc.exe       POC compilado (win-x64, self-contained)
docs\                          Guias MFA/SSO (usuario, API, desktop .NET)
src\                           Fonte do POC + Cryptography.cs

Service local neste host
------------------------
http://localhost:1710/

Como rodar (não precisa do SDK)
-------------------------------
$exePath --libs
$exePath --service http://localhost:1710/ --user SEU_LOGIN --password SUA_SENHA

SSO desktop usa app Entra "Mobile and desktop" (público). Não use o client secret do Portal.

Publicado: $(Get-Date -Format o)
"@

$utf8Bom = New-Object System.Text.UTF8Encoding $true
[System.IO.File]::WriteAllText((Join-Path $Destination 'README.txt'), $readme.Replace("`n", "`r`n"), $utf8Bom)

$manifestLines = New-Object System.Collections.Generic.List[string]
$manifestLines.Add("SAMPLE_SSO_MFA dest=$Destination")
$manifestLines.Add("publishedUtc=$((Get-Date).ToUniversalTime().ToString('o'))")
$manifestLines.Add("repoRoot=$RepoRoot")
$manifestLines.Add("exe=$exePath")
$manifestLines.Add("exeBytes=$exeSize")
$manifestLines.Add('')
$fileCount = 0
Get-ChildItem -LiteralPath $Destination -Recurse -Force | Where-Object { -not $_.PSIsContainer } | Sort-Object FullName | ForEach-Object {
    $rel = $_.FullName.Substring($Destination.Length).TrimStart('\', '/')
    $manifestLines.Add(("{0}`t{1}" -f $rel, $_.Length))
    $fileCount++
    Write-Host ("  {0} ({1} bytes)" -f $rel, $_.Length)
}
[System.IO.File]::WriteAllText((Join-Path $Destination 'MANIFEST.txt'), (($manifestLines -join "`r`n") + "`r`n"), $utf8Bom)

Write-Host ("SAMPLE_SSO_MFA_PUBLISHED dest={0} files={1} exe={2}" -f $Destination, $fileCount, $exePath)
exit 0
