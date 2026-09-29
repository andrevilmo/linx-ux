param(
    [Parameter(Mandatory = $true)]
    [string] $WebConfigPath,
    [Parameter(Mandatory = $true)]
    [string] $KeysXmlPath,
    [switch] $WhatIf
)

$ErrorActionPreference = 'Stop'
if (-not (Test-Path -LiteralPath $WebConfigPath)) {
    throw "Web.config not found: $WebConfigPath"
}
if (-not (Test-Path -LiteralPath $KeysXmlPath)) {
    throw "Keys file not found: $KeysXmlPath"
}

[xml] $cfg = Get-Content -LiteralPath $WebConfigPath -Encoding UTF8
$app = $cfg.configuration.appSettings
if (-not $app) {
    throw "No <appSettings> in $WebConfigPath"
}

$keys = Get-Content -LiteralPath $KeysXmlPath | Where-Object { $_ -match 'key="LicenseServer\.' }
$n = 0
foreach ($line in $keys) {
    if ($line -notmatch 'key="([^"]+)" value="([^"]*)"') { continue }
    $key = $Matches[1]
    $value = $Matches[2]
    $existing = $app.add | Where-Object { $_.key -eq $key }
    if ($WhatIf) {
        if ($existing) { Write-Host "WHATIF update $key" } else { Write-Host "WHATIF add $key" }
        continue
    }
    if ($existing) {
        $existing.value = $value
        Write-Host "UPD  $key"
    } else {
        $node = $cfg.CreateElement('add')
        $node.SetAttribute('key', $key)
        $node.SetAttribute('value', $value)
        [void] $app.AppendChild($node)
        Write-Host "ADD  $key"
    }
    $n++
}

if (-not $WhatIf) {
    $cfg.Save((Resolve-Path -LiteralPath $WebConfigPath))
    Write-Host "Saved $WebConfigPath ($n LicenseServer keys)"
}
