param(
    [string] $FrameworkRoot = 'C:\Linx Program Files\Linx Framework 6.0.0',
    [switch] $WhatIf
)

$ErrorActionPreference = 'Stop'
$pack = $PSScriptRoot
$merge = Join-Path (Split-Path -Parent $pack) 'Merge-AppSettings.ps1'
$service = Join-Path $FrameworkRoot 'Service'
$web = Join-Path $service 'Web.config'
$dllSrc = Join-Path $pack 'Service\bin\Linx.Framework.BV.dll'
$dllDst = Join-Path $service 'bin\Linx.Framework.BV.dll'
$keys = Join-Path $pack 'Service\Web.config.keys.xml'

Write-Host "Package : $pack"
Write-Host "IIS root: $FrameworkRoot"
if (-not (Test-Path -LiteralPath $web)) {
    throw "Service Web.config not found under $FrameworkRoot. Pass -FrameworkRoot."
}

if (Test-Path -LiteralPath $dllSrc) {
    if ($WhatIf) {
        Write-Host "WHATIF  Service\bin\Linx.Framework.BV.dll"
    } else {
        Copy-Item -LiteralPath $dllSrc -Destination $dllDst -Force
        Write-Host "OK      Service\bin\Linx.Framework.BV.dll"
    }
} else {
    Write-Warning "Linx.Framework.BV.dll is not in this pack. Compile 2-overwrite-main then copy the DLL into 1-apply-iis\Service\bin and re-run."
}

if (Test-Path -LiteralPath $merge) {
    & $merge -WebConfigPath $web -KeysXmlPath $keys -WhatIf:$WhatIf
} else {
    Write-Warning "Merge-AppSettings.ps1 not found; merge Service\Web.config.keys.xml into Service\Web.config by hand."
}

Write-Host ""
Write-Host "Done. Recycle the Service application pool (SI-PDR-Service / SI-PDR-Application as used on the host)."
Write-Host "Already on disk and reused: RestSharp.dll, Newtonsoft.Json.dll, Linx.Tools.dll (do not replace)."
