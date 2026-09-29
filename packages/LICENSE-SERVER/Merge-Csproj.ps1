param(
    [Parameter(Mandatory = $true)]
    [string] $CsprojPath,
    [Parameter(Mandatory = $true)]
    [string] $FragmentPath,
    [switch] $WhatIf
)

$ErrorActionPreference = 'Stop'
if (-not (Test-Path -LiteralPath $CsprojPath)) {
    throw "csproj not found: $CsprojPath"
}
if (-not (Test-Path -LiteralPath $FragmentPath)) {
    throw "fragment not found: $FragmentPath"
}

$csproj = [System.IO.File]::ReadAllText((Resolve-Path -LiteralPath $CsprojPath))
if ($csproj -match 'LicenseServer\\LicenseServerBootstrap\.cs') {
    Write-Host 'SKIP  Linx.Framework.BV.csproj already has LicenseServer Compile items'
    return
}

$needle = '    <Compile Include="Utils\LicenseControl.cs" />'
if ($csproj.IndexOf($needle) -lt 0) {
    throw 'Cannot find Compile Include Utils\LicenseControl.cs in csproj; merge the fragment by hand.'
}

$fragment = [System.IO.File]::ReadAllText((Resolve-Path -LiteralPath $FragmentPath))
$insert = ($fragment -split "`r?`n" | Where-Object { $_ -match '<Compile Include="LicenseServer\\' }) -join "`r`n"
$insert = $insert.TrimEnd() + "`r`n"

if ($WhatIf) {
    Write-Host 'WHATIF insert 9 LicenseServer Compile items before Utils\LicenseControl.cs'
    return
}

$updated = $csproj.Replace($needle, $insert + $needle)
[System.IO.File]::WriteAllText((Resolve-Path -LiteralPath $CsprojPath), $updated)
Write-Host "OK    merged LicenseServer Compile items into $CsprojPath"
