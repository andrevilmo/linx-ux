param(
    [Parameter(Mandatory = $true)]
    [string] $RepoRoot,
    [switch] $ReplaceCsproj,
    [switch] $WhatIf
)

$ErrorActionPreference = 'Stop'
$pack = $PSScriptRoot
$srcMain = Join-Path $pack 'Main'
$dstMain = Join-Path $RepoRoot 'Main'
if (-not (Test-Path -LiteralPath $dstMain)) {
    throw "Main folder not found under $RepoRoot"
}

function Copy-Tree([string] $Rel) {
    $from = Join-Path $srcMain $Rel
    $to = Join-Path $dstMain $Rel
    if (-not (Test-Path -LiteralPath $from)) { throw "Missing in pack: Main\$Rel" }
    if ($WhatIf) {
        Write-Host "WHATIF  Main\$Rel"
        return
    }
    if ((Get-Item -LiteralPath $from).PSIsContainer) {
        if (-not (Test-Path -LiteralPath $to)) {
            New-Item -ItemType Directory -Force -Path $to | Out-Null
        }
        Copy-Item -Path (Join-Path $from '*') -Destination $to -Recurse -Force
    } else {
        $dir = Split-Path -Parent $to
        if (-not (Test-Path -LiteralPath $dir)) {
            New-Item -ItemType Directory -Force -Path $dir | Out-Null
        }
        Copy-Item -LiteralPath $from -Destination $to -Force
    }
    Write-Host "OK      Main\$Rel"
}

Copy-Tree 'Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer'
Copy-Tree 'Business\Linx.Framework.BV\Linx.Framework.BV\Utils\LicenseControl.cs'
Copy-Tree 'Business\Linx.License.Server.Access.Tests'

$csprojDst = Join-Path $dstMain 'Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj'
$csprojFromBranch = Join-Path $srcMain 'Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj.FROM-ux-license-server'
$fragment = Join-Path $srcMain 'Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj.LicenseServer.fragment'
$mergeCsproj = Join-Path (Split-Path -Parent $pack) 'Merge-Csproj.ps1'

if ($ReplaceCsproj) {
    if ($WhatIf) {
        Write-Host 'WHATIF  replace Linx.Framework.BV.csproj from ux-license-server'
    } else {
        Copy-Item -LiteralPath $csprojFromBranch -Destination $csprojDst -Force
        Write-Host 'OK      Linx.Framework.BV.csproj (full file from ux-license-server)'
    }
} elseif (Test-Path -LiteralPath $mergeCsproj) {
    & $mergeCsproj -CsprojPath $csprojDst -FragmentPath $fragment -WhatIf:$WhatIf
} else {
    Write-Host 'SKIP    Merge-Csproj.ps1 missing. Insert the fragment into Linx.Framework.BV.csproj:'
    Get-Content -LiteralPath $fragment | Write-Host
}

$keys = Join-Path $srcMain 'Binary\Service\Web.config.LicenseServer.keys.xml'
$web = Join-Path $dstMain 'Binary\Service\Web.config'
$merge = Join-Path (Split-Path -Parent $pack) 'Merge-AppSettings.ps1'
if ((Test-Path -LiteralPath $web) -and (Test-Path -LiteralPath $merge)) {
    & $merge -WebConfigPath $web -KeysXmlPath $keys -WhatIf:$WhatIf
} else {
    Write-Host "Merge keys from Main\Binary\Service\Web.config.LicenseServer.keys.xml into Main\Binary\Service\Web.config"
}

Write-Host ""
Write-Host "Build: MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release"
Write-Host "Then copy bin\Linx.Framework.BV.dll to IIS Service\bin (see 1-apply-iis)."
