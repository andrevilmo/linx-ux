param(
    [string]$FrameworkRoot = 'C:\Linx Program Files\Linx Framework 6.0.0',
    [switch]$WhatIf
)

$ErrorActionPreference = 'Stop'
$pack = $PSScriptRoot

function Copy-PackFile {
    param([string]$Rel)
    $src = Join-Path $pack $Rel
    $dst = Join-Path $FrameworkRoot $Rel
    if (-not (Test-Path -LiteralPath $src)) {
        Write-Warning "Missing in package: $Rel"
        return
    }
    $dir = Split-Path -Parent $dst
    if (-not $WhatIf -and -not (Test-Path -LiteralPath $dir)) {
        New-Item -ItemType Directory -Force -Path $dir | Out-Null
    }
    if ($WhatIf) {
        Write-Host "WHATIF  $Rel"
        return
    }
    Copy-Item -LiteralPath $src -Destination $dst -Force
    Write-Host "OK      $Rel"
}

Write-Host "Package : $pack"
Write-Host "IIS root: $FrameworkRoot"
Write-Host "Does not copy Web.config or DB\."
Write-Host ""

if (-not (Test-Path -LiteralPath (Join-Path $FrameworkRoot 'Portal\Web.config'))) {
    throw "Portal Web.config not found under $FrameworkRoot. Pass -FrameworkRoot to the folder that contains Portal, Service, Application."
}

$files = @(
    'Portal\bin\Linx.Portal.dll'
    'Portal\bin\Microsoft.Identity.Client.dll'
    'Portal\bin\Microsoft.IdentityModel.Abstractions.dll'
    'Portal\Views\Account\Login.cshtml'
    'Portal\Views\Mfa\Challenge.cshtml'
    'Service\bin\Linx.Framework.BV.dll'
    'Service\bin\Linx.Framework.BV.WebAPI.DS.dll'
    'Application\bin\Linx.Internet.Application.dll'
    'Application\bin\Linx.Framework.BV.SPA.dll'
    'Application\App\views\CadastroUsuario.html'
    'Application\App\views\CadastroUsuarioLocal.html'
    'Application\App\views\CadastroUsuarioAutenticacao.html'
    'Application\App\viewmodels\CadastroUsuario.js'
    'Application\App\viewmodels\CadastroUsuarioLocal.js'
    'Application\App\viewmodels\CadastroUsuarioAutenticacao.js'
    'Application\App\resources\CadastroUsuario_pt-br.js'
    'Application\App\resources\CadastroUsuarioLocal_pt-br.js'
    'Application\App\resources\CadastroUsuarioAutenticacao_pt-br.js'
    'Application\App\widgets\datatoolbar\view.html'
)

foreach ($f in $files) { Copy-PackFile $f }

Write-Host ""
Write-Host "Done. Recycle IIS pools Service, Portal, Application."
Write-Host "SQL: run DB\APPLY_SSO_MFA.sql on the Portal catalog (not done by this script)."
