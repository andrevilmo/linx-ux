param(
    [string]$OutputRoot = '',
    [string]$BaseBranch = 'original',
    [string]$PackageName = 'SI-PDR'
)

$ErrorActionPreference = 'Stop'

# ---------------------------------------------------------------------------
# Paths
# ---------------------------------------------------------------------------

$repoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..')).Path
$workspace = if (Test-Path (Join-Path $repoRoot 'Main\Application')) {
    Join-Path $repoRoot 'Main'
}
else {
    $repoRoot
}

if (-not $OutputRoot) {
    $OutputRoot = [Environment]::GetFolderPath('Desktop')
}

$packageRoot = Join-Path $OutputRoot $PackageName
$inventoryPath = Join-Path $OutputRoot 'DEPLOY_ITEMS_SEGURANCA.MD'
$leiaMePath = Join-Path $packageRoot 'LEIA-ME.md'

function Get-GitValue {
    param([string]$ArgsLine)
    try {
        Push-Location $repoRoot
        $out = & git $ArgsLine.Split(' ') 2>$null
        if ($LASTEXITCODE -eq 0 -and $out) {
            if ($out -is [array]) { return ($out | Select-Object -First 1).ToString().Trim() }
            return $out.ToString().Trim()
        }
    }
    catch {
    }
    finally {
        Pop-Location -ErrorAction SilentlyContinue
    }
    return '(unknown)'
}

$gitBranch = Get-GitValue 'rev-parse --abbrev-ref HEAD'
$gitCommit = Get-GitValue 'rev-parse --short HEAD'
$stamp = Get-Date -Format 'yyyy-MM-dd'
$stampFull = Get-Date -Format 'yyyy-MM-dd HH:mm:ss'

Write-Host "Workspace : $workspace"
Write-Host "Output    : $packageRoot"
Write-Host "Branch    : $gitBranch ($gitCommit)"
Write-Host "Base      : $BaseBranch"
Write-Host ''

# ---------------------------------------------------------------------------
# Helpers
# ---------------------------------------------------------------------------

function Ensure-Dir {
    param([string]$Path)
    if (-not (Test-Path -LiteralPath $Path)) {
        New-Item -ItemType Directory -Force -Path $Path | Out-Null
    }
}

function Copy-Newest {
    param(
        [string[]]$Candidates,
        [string]$DestRelative
    )

    $dest = Join-Path $packageRoot $DestRelative
    $found = @(
        $Candidates |
            Where-Object { $_ -and (Test-Path -LiteralPath $_) } |
            ForEach-Object { Get-Item -LiteralPath $_ } |
            Sort-Object LastWriteTime -Descending
    )

    if ($found.Count -eq 0) {
        Write-Warning "MISSING: $DestRelative"
        return $false
    }

    Ensure-Dir (Split-Path -Parent $dest)
    Copy-Item -LiteralPath $found[0].FullName -Destination $dest -Force
    Write-Host ("OK  {0}  [{1:yyyy-MM-dd HH:mm:ss}]" -f $DestRelative, $found[0].LastWriteTime)
    return $true
}

$script:missing = 0
function Require-Copy {
    param([string[]]$Candidates, [string]$DestRelative)
    if (-not (Copy-Newest -Candidates $Candidates -DestRelative $DestRelative)) {
        $script:missing++
    }
}

# ---------------------------------------------------------------------------
# Rebuild package tree
# ---------------------------------------------------------------------------

Write-Host '=== Rebuild SI-PDR package ===' -ForegroundColor Cyan

if (Test-Path -LiteralPath $packageRoot) {
    Remove-Item -LiteralPath $packageRoot -Recurse -Force
}

@(
    'Application\bin'
    'Application\Views\LIA'
    'Application\Views\Shared'
    'Application\App\viewmodels\shared'
    'Application\App\widgets\datatoolbar'
    'Application\App\views'
    'Application\App\viewmodels'
    'Application\App\services'
    'Application\App\resources'
    'Service\bin\Extension'
    'Portal\bin'
    'Portal\Views\Account'
    'Portal\assets\css'
    'DB'
) | ForEach-Object { Ensure-Dir (Join-Path $packageRoot $_) }

$appBin = Join-Path $workspace 'Binary\Application\bin'
$projBin = Join-Path $workspace 'Application\Linx.Internet.Application\Linx.Internet.Application\bin'
$projViews = Join-Path $workspace 'Application\Linx.Internet.Application\Linx.Internet.Application\Views'
$binViews = Join-Path $workspace 'Binary\Application\Views'
$appRoot = Join-Path $workspace 'Application\Linx.Internet.Application\Linx.Internet.Application\App'
$spaApp = Join-Path $workspace 'User Interface\Linx.Framework.BV\Linx.Framework.BV.SPA\App'
$svcBin = Join-Path $workspace 'Binary\Service\bin'
$portalProj = Join-Path $workspace 'Application\Linx.Portal\Linx.Portal'
$portalBin = Join-Path $workspace 'Binary\Portal\bin'

# Application bin
@(
    'Linx.Internet.Application.dll'
    'Linx.Internet.Application.pdb'
    'Linx.Internet.Application.Common.dll'
    'Linx.Internet.Application.Extension.dll'
    'Linx.Internet.Application.Framework.dll'
    'Linx.Internet.Application.Framework.Contracts.dll'
    'Linx.Framework.Custom.BV.SPA.dll'
) | ForEach-Object {
    Require-Copy @((Join-Path $projBin $_), (Join-Path $appBin $_)) "Application\bin\$_"
}

Require-Copy @(
    (Join-Path $workspace 'User Interface\Linx.Framework.BV\Linx.Framework.BV.SPA\bin\Linx.Framework.BV.SPA.dll')
    (Join-Path $workspace 'Binary\Library\User Interface\Linx.Framework.BV.SPA.dll')
    (Join-Path $appBin 'Linx.Framework.BV.SPA.dll')
) 'Application\bin\Linx.Framework.BV.SPA.dll'

# Application Views
@(
    'LIA\ForgotPassword.cshtml'
    'LIA\ResetPassword.cshtml'
    'Shared\_Footer.cshtml'
    'Shared\_Layout.cshtml'
) | ForEach-Object {
    Require-Copy @((Join-Path $projViews $_), (Join-Path $binViews $_)) "Application\Views\$_"
}

# Application App
@(
    'viewmodels\shared\modalChangePassword.js'
    'widgets\datatoolbar\view.html'
) | ForEach-Object {
    Require-Copy @((Join-Path $appRoot $_)) "Application\App\$_"
}

@(
    'views\CadastroUsuarioAutenticacao.html'
    'views\CadastroUsuarioLocal.html'
    'viewmodels\CadastroUsuarioAutenticacao.js'
    'viewmodels\CadastroUsuarioLocal.js'
    'services\UsuarioAutorizacaoContext.js'
    'services\UsuarioFranquiaContext.js'
    'resources\CadastroUsuarioAutenticacao_pt-br.js'
    'resources\CadastroUsuarioLocal_pt-br.js'
) | ForEach-Object {
    Require-Copy @((Join-Path $spaApp $_)) "Application\App\$_"
}

# Service
$serviceMap = [ordered]@{
    'Linx.Tools.dll'                        = @(
        (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll')
        (Join-Path $svcBin 'Linx.Tools.dll')
    )
    'Linx.Framework.BV.dll'                 = @(
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV\bin\Release\Linx.Framework.BV.dll')
        (Join-Path $svcBin 'Linx.Framework.BV.dll')
    )
    'Linx.Framework.BV.WebAPI.dll'          = @(
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI\bin\Release\Linx.Framework.BV.WebAPI.dll')
        (Join-Path $svcBin 'Linx.Framework.BV.WebAPI.dll')
    )
    'Linx.Framework.BV.WebAPI.DS.dll'       = @(
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.WebAPI.DS\bin\Release\Linx.Framework.BV.WebAPI.DS.dll')
        (Join-Path $svcBin 'Linx.Framework.BV.WebAPI.DS.dll')
    )
    'Linx.Framework.Autorizacao.BM.dll'     = @(
        (Join-Path $workspace 'BM\Linx.Framework.Autorizacao.BM\Linx.Framework.Autorizacao.BM\bin\Release\Linx.Framework.Autorizacao.BM.dll')
        (Join-Path $svcBin 'Linx.Framework.Autorizacao.BM.dll')
    )
    'Linx.Framework.BV.Implementations.dll' = @(
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.Implementations\bin\Release\Linx.Framework.BV.Implementations.dll')
        (Join-Path $svcBin 'Linx.Framework.BV.Implementations.dll')
    )
    'Linx.Framework.BV.Reports.dll'         = @(
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.Reports\bin\Release\Linx.Framework.BV.Reports.dll')
        (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.Reports\bin\Debug\Linx.Framework.BV.Reports.dll')
        (Join-Path $svcBin 'Linx.Framework.BV.Reports.dll')
    )
}

foreach ($dll in $serviceMap.Keys) {
    Require-Copy $serviceMap[$dll] "Service\bin\$dll"
}

Require-Copy @(
    (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.AuthenticateUserExtension\bin\Release\Linx.Framework.BV.AuthenticateUserExtension.dll')
    (Join-Path $workspace 'Business\Linx.Framework.BV\Linx.Framework.BV.AuthenticateUserExtension\bin\Debug\Linx.Framework.BV.AuthenticateUserExtension.dll')
) 'Service\bin\Extension\Linx.Framework.BV.AuthenticateUserExtension.dll'

Require-Copy @((Join-Path $workspace 'Binary\Service\Web.config')) 'Service\Web.config'

# Portal
Require-Copy @(
    (Join-Path $portalProj 'bin\Linx.Portal.dll')
    (Join-Path $portalBin 'Linx.Portal.dll')
) 'Portal\bin\Linx.Portal.dll'

Copy-Newest @(
    (Join-Path $portalProj 'bin\Linx.Portal.pdb')
    (Join-Path $portalBin 'Linx.Portal.pdb')
) 'Portal\bin\Linx.Portal.pdb' | Out-Null

Require-Copy @(
    (Join-Path $workspace 'Common\Linx.Tools.Library\Desktop\Linx.Desktop.Tools\bin\Release\Linx.Tools.dll')
    (Join-Path $portalBin 'Linx.Tools.dll')
    (Join-Path $svcBin 'Linx.Tools.dll')
) 'Portal\bin\Linx.Tools.dll'

Require-Copy @(
    (Join-Path $portalProj 'Views\Account\Login.cshtml')
    (Join-Path $workspace 'Binary\Portal\Views\Account\Login.cshtml')
) 'Portal\Views\Account\Login.cshtml'

Require-Copy @((Join-Path $portalProj 'assets\css\portal.css')) 'Portal\assets\css\portal.css'

Require-Copy @(
    (Join-Path $portalProj 'bin\Microsoft.Identity.Client.dll')
    (Join-Path $workspace 'Application\Linx.Portal\packages\Microsoft.Identity.Client.4.54.1\lib\net461\Microsoft.Identity.Client.dll')
    (Join-Path $portalBin 'Microsoft.Identity.Client.dll')
    (Join-Path $workspace 'Binary\Library\Common\Microsoft\Identity\Microsoft.Identity.Client.dll')
) 'Portal\bin\Microsoft.Identity.Client.dll'

Require-Copy @(
    (Join-Path $portalProj 'bin\Microsoft.IdentityModel.Abstractions.dll')
    (Join-Path $workspace 'Application\Linx.Portal\packages\Microsoft.IdentityModel.Abstractions.6.22.0\lib\net461\Microsoft.IdentityModel.Abstractions.dll')
    (Join-Path $portalBin 'Microsoft.IdentityModel.Abstractions.dll')
    (Join-Path $workspace 'Binary\Library\Common\Microsoft\Identity\Microsoft.IdentityModel.Abstractions.dll')
) 'Portal\bin\Microsoft.IdentityModel.Abstractions.dll'

# DB
Require-Copy @(
    (Join-Path $workspace 'BM\Linx.Framework.Autorizacao.BM\Linx.Framework.Autorizacao.BM\Scripts\TCS_LOG_ACESSO_AUTH.sql')
) 'DB\TCS_LOG_ACESSO_AUTH.sql'

Require-Copy @(
    (Join-Path $workspace 'BM\Linx.Framework.Autorizacao.BM\Linx.Framework.Autorizacao.BM\Scripts\INDICA_USUARIO_SERVICO.sql')
) 'DB\INDICA_USUARIO_SERVICO.sql'

Require-Copy @(
    (Join-Path $workspace 'Binary\Service\SqlScripts\Disable_Update_aspnet_Membership_Trigger.sql')
) 'DB\Disable_Update_aspnet_Membership_Trigger.sql'

$packedFiles = @(Get-ChildItem -LiteralPath $packageRoot -Recurse -File | Sort-Object FullName)
$fileCount = $packedFiles.Count

Write-Host ''
Write-Host "Packed files: $fileCount  Missing: $script:missing"

if ($script:missing -gt 0) {
    Write-Warning 'Package completed with missing files. Inventory will still be generated.'
}

# ---------------------------------------------------------------------------
# LEIA-ME.md + DEPLOY_ITEMS_SEGURANCA.MD (UTF-8 templates with BOM)
# ---------------------------------------------------------------------------

Write-Host ''
Write-Host '=== Write LEIA-ME.md / inventory ===' -ForegroundColor Cyan

$utf8Bom = New-Object System.Text.UTF8Encoding $true

function Read-Utf8File {
    param([string]$Path)
    if (-not (Test-Path -LiteralPath $Path)) {
        throw "Template not found: $Path"
    }
    return [System.IO.File]::ReadAllText($Path, $utf8Bom)
}

function Expand-Template {
    param(
        [string]$Template,
        [hashtable]$Tokens
    )
    $result = $Template
    foreach ($key in $Tokens.Keys) {
        $result = $result.Replace("{{$key}}", [string]$Tokens[$key])
    }
    return $result
}

function Get-RelList {
    param([string]$Prefix)
    $items = @(
        $packedFiles |
            Where-Object {
                $_.FullName.Substring($packageRoot.Length).TrimStart('\').StartsWith($Prefix, [StringComparison]::OrdinalIgnoreCase)
            } |
            ForEach-Object { $_.FullName.Substring($packageRoot.Length).TrimStart('\') }
    )
    if ($items.Count -eq 0) { return '(nenhum)' }
    return (($items | ForEach-Object { '- `' + $_ + '`' }) -join "`r`n")
}

$fileList = (($packedFiles | ForEach-Object {
            '- `' + $_.FullName.Substring($packageRoot.Length).TrimStart('\') + '`'
        }) -join "`r`n")

$flatList = (($packedFiles |
        Where-Object { -not $_.FullName.EndsWith('LEIA-ME.md') } |
        ForEach-Object { $_.FullName.Substring($packageRoot.Length).TrimStart('\') }) -join "`r`n")

$tokens = @{
    GIT_COMMIT   = $gitCommit
    GIT_BRANCH   = $gitBranch
    BASE_BRANCH  = $BaseBranch
    STAMP        = $stamp
    STAMP_FULL   = $stampFull
    PACKAGE_NAME = $PackageName
    FILE_COUNT   = $fileCount
    FILE_LIST    = $fileList
    APP_LIST     = (Get-RelList 'Application\')
    SERVICE_LIST = (Get-RelList 'Service\')
    PORTAL_LIST  = (Get-RelList 'Portal\')
    DB_LIST      = (Get-RelList 'DB\')
    FLAT_LIST    = $flatList
    MISSING      = $script:missing
}

$leiaTemplatePath = Join-Path $PSScriptRoot 'pack-si-pdr-LEIA-ME.template.md'
$invTemplatePath = Join-Path $PSScriptRoot 'pack-si-pdr-INVENTORY.template.md'

$leia = Expand-Template -Template (Read-Utf8File $leiaTemplatePath) -Tokens $tokens
$inventory = Expand-Template -Template (Read-Utf8File $invTemplatePath) -Tokens $tokens

[System.IO.File]::WriteAllText($leiaMePath, $leia, $utf8Bom)
Write-Host "Wrote $leiaMePath"

[System.IO.File]::WriteAllText($inventoryPath, $inventory, $utf8Bom)
Write-Host "Wrote $inventoryPath"

Write-Host ''
Write-Host "Done. Package=$packageRoot Files=$fileCount Missing=$($script:missing)" -ForegroundColor Green
Write-Host "Inventory: $inventoryPath"
Write-Host "LEIA-ME:   $leiaMePath"

if ($script:missing -gt 0) {
    exit 1
}

exit 0
