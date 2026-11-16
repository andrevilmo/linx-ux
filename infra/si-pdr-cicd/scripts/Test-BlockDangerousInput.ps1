<#
.SYNOPSIS
  Casos de ERRO (bloqueio HTTP 400) do filtro BlockDangerousInput no Service WebAPI.DS.

.DESCRIPTION
  Demonstra o bloqueio Soft + path + InspectSavePayload para cada RoutePrefix AutoGen:
    1) GET  .../GetTemplateReport?reportPath=../...     (path traversal)
    2) GET  .../GetTemplateReport?reportPath=C:\...     (path absoluto)
    3) GET  .../AssemblyInfo?probe=<script>...          (Soft query / script)
    4) POST .../SaveChanges  body com Nome=<script>...  (payload de gravação)

  Expectativa: StatusCode = 400 e Message do filtro (não 500 de negócio).

.PARAMETER ServiceBaseUrl
  Base do Service. AWS público típico: http://<ip>:8082  | local: http://127.0.0.1:1710

.PARAMETER Controllers
  Lista opcional de RoutePrefix. Se omitido, usa todos os AutoGen BV conhecidos.

.PARAMETER SkipSaveChanges
  Não executa os POSTs SaveChanges (só path + Soft query).

.PARAMETER OutCsv
  Caminho opcional para gravar o resultado (CSV).

.EXAMPLE
  .\Test-BlockDangerousInput.ps1 -ServiceBaseUrl 'http://18.231.155.145:8082'

.EXAMPLE
  .\Test-BlockDangerousInput.ps1 -ServiceBaseUrl 'http://127.0.0.1:1710' -OutCsv C:\temp\block-dangerous.csv
#>
[CmdletBinding()]
param(
    [Parameter()]
    [string] $ServiceBaseUrl = 'http://127.0.0.1:8082',

    [Parameter()]
    [string[]] $Controllers,

    [Parameter()]
    [switch] $SkipSaveChanges,

    [Parameter()]
    [string] $OutCsv,

    [Parameter()]
    [int] $TimeoutSec = 60
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Continue'

# RoutePrefix dos *AutoGen* em Linx.Framework.BV.WebAPI.DS (GetTemplateReport + SaveChanges).
$DefaultControllers = @(
    'LinxFrameworkAmbiente',
    'LinxFrameworkAplicacao',
    'LinxFrameworkAplicativo',
    'LinxFrameworkAuditoria',
    'LinxFrameworkAutorizacao',
    'LinxFrameworkConexao',
    'LinxFrameworkConfiguracao',
    'LinxFrameworkEmpresa',
    'LinxFrameworkExecutaImportacao',
    'LinxFrameworkFiltro',
    'LinxFrameworkGrupoEconomico',
    'LinxFrameworkIndicadorMedida',
    'LinxFrameworkLayout',
    'LinxFrameworkLayoutArquivo',
    'LinxFrameworkLoja',
    'LinxFrameworkLojaParametro',
    'LinxFrameworkLojaVendedor',
    'LinxFrameworkMensagem',
    'LinxFrameworkModulo',
    'LinxFrameworkModuloAutorizacao',
    'LinxFrameworkModuloLoja',
    'LinxFrameworkMultimidia',
    'LinxFrameworkMultimidiaAutorizacao',
    'LinxFrameworkObjeto',
    'LinxFrameworkObjetoAutorizacao',
    'LinxFrameworkParametro',
    'LinxFrameworkParametroAutorizacao',
    'LinxFrameworkPastaUsuario',
    'LinxFrameworkPerfil',
    'LinxFrameworkRede',
    'LinxFrameworkTabelaAutorizacao',
    'LinxFrameworkTransacao',
    'LinxFrameworkTransacaoAutorizacao',
    'LinxFrameworkTransacaoTag',
    'LinxFrameworkTratamentoErros',
    'LinxFrameworkUsuario',
    'LinxFrameworkUsuarioAutorizacao',
    'LinxFrameworkUsuarioExterno',
    'LinxFrameworkUsuarioFranquia',
    'LinxFrameworkUtilitarios'
)

if (-not $Controllers -or $Controllers.Count -eq 0) {
    $Controllers = $DefaultControllers
}

$base = $ServiceBaseUrl.TrimEnd('/')
$results = New-Object System.Collections.Generic.List[object]

function Get-ResponseBodyText {
    param($Response)
    try {
        if ($null -eq $Response) { return '' }
        $stream = $Response.GetResponseStream()
        if ($null -eq $stream) { return '' }
        $reader = New-Object System.IO.StreamReader($stream)
        try { return $reader.ReadToEnd() } finally { $reader.Dispose() }
    }
    catch { return $_.Exception.Message }
}

function Invoke-BlockCase {
    param(
        [string] $CaseId,
        [string] $Controller,
        [string] $Kind,
        [string] $Method,
        [string] $Url,
        [string] $BodyJson,
        [string] $ExpectedMessageSubstring
    )

    $status = 0
    $body = ''
    $ok = $false
    $errorText = ''

    try {
        $headers = @{ Accept = 'application/json' }
        if ($Method -eq 'GET') {
            $resp = Invoke-WebRequest -Uri $Url -Method GET -Headers $headers -UseBasicParsing -TimeoutSec $TimeoutSec
            $status = [int]$resp.StatusCode
            $body = [string]$resp.Content
        }
        else {
            $resp = Invoke-WebRequest -Uri $Url -Method POST -Headers $headers -ContentType 'application/json; charset=utf-8' `
                -Body $BodyJson -UseBasicParsing -TimeoutSec $TimeoutSec
            $status = [int]$resp.StatusCode
            $body = [string]$resp.Content
        }
    }
    catch [System.Net.WebException] {
        $ex = $_.Exception
        if ($ex.Response) {
            $status = [int]$ex.Response.StatusCode
            $body = Get-ResponseBodyText -Response $ex.Response
        }
        else {
            $errorText = $ex.Message
        }
    }
    catch {
        $errorText = $_.Exception.Message
    }

    $messageMatch = $false
    if ($ExpectedMessageSubstring -and $body) {
        $messageMatch = $body -match [regex]::Escape($ExpectedMessageSubstring)
    }

    # Bloqueio do filtro = 400 + mensagem conhecida (ou 400 genérico se substring vazia).
    $ok = ($status -eq 400) -and ($messageMatch -or [string]::IsNullOrEmpty($ExpectedMessageSubstring))

    $row = [pscustomobject]@{
        CaseId           = $CaseId
        Controller       = $Controller
        Kind             = $Kind
        Method           = $Method
        Url              = $Url
        StatusCode       = $status
        BlockedAsExpected = $ok
        MessageMatch     = $messageMatch
        BodySnippet      = if ($body.Length -gt 220) { $body.Substring(0, 220) + '...' } else { $body }
        Error            = $errorText
    }
    $results.Add($row) | Out-Null

    $mark = if ($ok) { 'PASS' } else { 'FAIL' }
    Write-Host ("[{0}] {1} {2} {3} status={4}" -f $mark, $CaseId, $Controller, $Kind, $status)
    if (-not $ok) {
        if ($errorText) { Write-Host ("       error: {0}" -f $errorText) -ForegroundColor Yellow }
        if ($body) { Write-Host ("       body: {0}" -f $row.BodySnippet) -ForegroundColor Yellow }
    }
}

Write-Host ("BlockDangerousInput ERROR cases against {0}" -f $base)
Write-Host ("Controllers: {0}" -f $Controllers.Count)
Write-Host ''

# --- Casos extras só em Autorizacao (query Soft com param conhecido pelo binding) ---
$authSoftCases = @(
    @{ Id = 'SOFT-SCRIPT';  Q = 'nomeEmpresa=<script>alert(1)</script>'; Sub = 'marcadores de script/HTML' },
    @{ Id = 'SOFT-JS';      Q = 'nomeEmpresa=javascript:alert(1)';       Sub = 'marcadores de script/HTML' },
    @{ Id = 'SOFT-IFRAME';  Q = 'nomeEmpresa=<iframe src=x>';            Sub = 'marcadores de script/HTML' },
    @{ Id = 'SOFT-ONERROR'; Q = 'nomeEmpresa=<img onerror=alert(1)>';  Sub = 'marcadores de script/HTML' }
)

foreach ($c in $authSoftCases) {
    $url = '{0}/LinxFrameworkAutorizacao/GetMfaStatus?tableOrigin=UX&idGpecon=0&uidUsuario=00000000-0000-0000-0000-000000000001&{1}' -f $base, $c.Q
    Invoke-BlockCase -CaseId $c.Id -Controller 'LinxFrameworkAutorizacao' -Kind 'SoftQuery' `
        -Method 'GET' -Url $url -BodyJson $null -ExpectedMessageSubstring $c.Sub
}

# --- Por controller: path + save ---
$pathPayloads = @(
    @{ Id = 'PATH-DOTDOT';  Path = '../Web.config';              Sub = 'path traversal' },
    @{ Id = 'PATH-DOTDOT2'; Path = '..\..\Windows\win.ini';        Sub = 'path traversal' },
    @{ Id = 'PATH-ABS';     Path = 'C:\Windows\win.ini';          Sub = 'absoluto' },
    @{ Id = 'PATH-URLENC';  Path = '..%2fWeb.config';             Sub = 'path traversal' }
)

$saveBody = @{
    entities = @(
        @{
            Nome = '<script>alert(1)</script>'
            Descricao = 'probe'
            entityAspect = @{
                entityTypeName = 'Probe:#Test'
                entityState = 'Added'
            }
        }
    )
    saveOptions = @{}
} | ConvertTo-Json -Depth 8 -Compress

foreach ($prefix in $Controllers) {
    foreach ($p in $pathPayloads) {
        # Uri EscapeDataString for safety except PATH-URLENC which already encodes ..
        $pathValue = $p.Path
        if ($p.Id -ne 'PATH-URLENC') {
            $pathValue = [uri]::EscapeDataString($p.Path)
        }
        $url = '{0}/{1}/GetTemplateReport?reportPath={2}' -f $base, $prefix, $pathValue
        Invoke-BlockCase -CaseId $p.Id -Controller $prefix -Kind 'PathTraversal' `
            -Method 'GET' -Url $url -BodyJson $null -ExpectedMessageSubstring $p.Sub
    }

    # Soft probe via AssemblyInfo extra query (não é skip list)
    $softUrl = '{0}/{1}/AssemblyInfo?probe=<script>alert(1)</script>' -f $base, $prefix
    Invoke-BlockCase -CaseId 'SOFT-PROBE' -Controller $prefix -Kind 'SoftQuery' `
        -Method 'GET' -Url $softUrl -BodyJson $null -ExpectedMessageSubstring 'marcadores de script/HTML'

    if (-not $SkipSaveChanges) {
        $saveUrl = '{0}/{1}/SaveChanges' -f $base, $prefix
        Invoke-BlockCase -CaseId 'SAVE-SCRIPT' -Controller $prefix -Kind 'SavePayload' `
            -Method 'POST' -Url $saveUrl -BodyJson $saveBody -ExpectedMessageSubstring 'marcadores de script/HTML'
    }
}

Write-Host ''
$pass = @($results | Where-Object { $_.BlockedAsExpected }).Count
$fail = @($results | Where-Object { -not $_.BlockedAsExpected }).Count
Write-Host ("Summary: PASS={0} FAIL={1} TOTAL={2}" -f $pass, $fail, $results.Count)

if ($OutCsv) {
    $dir = Split-Path -Parent $OutCsv
    if ($dir -and -not (Test-Path $dir)) { New-Item -ItemType Directory -Path $dir -Force | Out-Null }
    $results | Export-Csv -Path $OutCsv -NoTypeInformation -Encoding UTF8
    Write-Host ("CSV: {0}" -f $OutCsv)
}

# Exit 1 se algum caso esperado de bloqueio falhou
if ($fail -gt 0) { exit 1 } else { exit 0 }
