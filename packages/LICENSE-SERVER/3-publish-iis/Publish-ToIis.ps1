param(
    [string] $FrameworkRoot = 'C:\Linx Program Files\Linx Framework 6.0.0',
    [string] $ServicePool = 'SI-PDR-Service',
    [switch] $WhatIf
)

$ErrorActionPreference = 'Stop'
$pack = $PSScriptRoot
$kit = Split-Path -Parent $pack
$merge = Join-Path $kit 'Merge-AppSettings.ps1'
$service = Join-Path $FrameworkRoot 'Service'
$web = Join-Path $service 'Web.config'
$dllSrc = Join-Path $pack 'Service\bin\Linx.Framework.BV.dll'
$dllDst = Join-Path $service 'bin\Linx.Framework.BV.dll'
$keys = Join-Path $pack 'Service\Web.config.keys.xml'
$stamp = Get-Date -Format 'yyyyMMdd-HHmmss'

Write-Host "Package : $pack"
Write-Host "IIS root: $FrameworkRoot"
if (-not (Test-Path -LiteralPath $web)) {
    throw "Service Web.config not found under $FrameworkRoot. Pass -FrameworkRoot to the Linx Framework 6.0.0 folder."
}

function Backup-File([string] $Path) {
    if (-not (Test-Path -LiteralPath $Path)) { return }
    $bak = "$Path.bak-$stamp"
    if ($WhatIf) {
        Write-Host "WHATIF  backup $Path -> $bak"
        return
    }
    Copy-Item -LiteralPath $Path -Destination $bak -Force
    Write-Host "BAK     $bak"
}

Backup-File $web
Backup-File $dllDst

if (Test-Path -LiteralPath $dllSrc) {
    if ($WhatIf) {
        Write-Host "WHATIF  Service\bin\Linx.Framework.BV.dll"
    } else {
        $binDir = Split-Path -Parent $dllDst
        if (-not (Test-Path -LiteralPath $binDir)) {
            New-Item -ItemType Directory -Force -Path $binDir | Out-Null
        }
        Copy-Item -LiteralPath $dllSrc -Destination $dllDst -Force
        Write-Host "OK      Service\bin\Linx.Framework.BV.dll"
    }
} else {
    Write-Warning "Linx.Framework.BV.dll is not in this pack. Compile 2-overwrite-main, copy the DLL into 3-publish-iis\Service\bin, and re-run."
}

if (Test-Path -LiteralPath $merge) {
    & $merge -WebConfigPath $web -KeysXmlPath $keys -WhatIf:$WhatIf
} else {
    Write-Warning "Merge-AppSettings.ps1 not found; merge Service\Web.config.keys.xml into Service\Web.config by hand."
}

function Recycle-Pool([string] $Name) {
    try {
        Import-Module WebAdministration -ErrorAction Stop
    } catch {
        Write-Warning "WebAdministration not loaded (run as Administrator). Recycle $Name by hand."
        return
    }
    $poolPath = "IIS:\AppPools\$Name"
    if (-not (Test-Path $poolPath)) {
        Write-Warning "App pool $Name not found. Recycle the Service pool in IIS Manager."
        return
    }
    if ($WhatIf) {
        Write-Host "WHATIF  recycle $Name"
        return
    }
    $state = (Get-WebAppPoolState -Name $Name).Value
    if ($state -eq 'Started') {
        Restart-WebAppPool -Name $Name
        Write-Host "OK      recycled $Name"
    } else {
        Start-WebAppPool -Name $Name
        Write-Host "OK      started $Name (was $state)"
    }
}

Recycle-Pool $ServicePool
Write-Host ""
Write-Host "Done. Next: Test-Iis.ps1 then Portal login at http://localhost:8172/"
Write-Host "Guide: INSTALAR-IIS.md / INSTALL-IIS.md"
Write-Host "Reuse on disk (do not replace): RestSharp.dll, Newtonsoft.Json.dll, Linx.Tools.dll"
