<#
    Cria um administrador no mesmo grupo economico de um usuario, chamando o Service.

    Sem -Confirmar o script so le e mostra o que gravaria.
    Com -Confirmar ele grava o login, os acessos de administrador, os modulos da
    empresa e o acesso total aos modulos do aplicativo de cada ambiente.

    Exemplo:
      .\criar-admin-mesmo-gpecon.ps1 -LoginAlvo teste.franqueado
      .\criar-admin-mesmo-gpecon.ps1 -LoginAlvo teste.franqueado -Confirmar
#>
[CmdletBinding()]
param(
    [string]$LoginAlvo,
    [string]$LoginAdmin,
    [string]$NomeAdmin,
    [string]$Email,
    [string]$Senha = 'P@ssw0rd1920398',
    [string]$ServiceUrl = 'http://127.0.0.1:1710',
    [int]$TimeoutSec = 120,
    [switch]$Confirmar
)

$ErrorActionPreference = 'Stop'
$ServiceUrl = $ServiceUrl.TrimEnd('/')

if ([string]::IsNullOrWhiteSpace($LoginAlvo)) {
    $LoginAlvo = Read-Host 'Login do usuario alvo'
}
$LoginAlvo = $LoginAlvo.Trim()
if ([string]::IsNullOrWhiteSpace($LoginAlvo)) {
    throw 'Informe o login do usuario alvo.'
}
if ([string]::IsNullOrWhiteSpace($LoginAdmin)) {
    $LoginAdmin = 'admin.' + $LoginAlvo
}
if ($Senha.Length -lt 12 -or $Senha -cnotmatch '[A-Z]' -or $Senha -cnotmatch '[a-z]' -or $Senha -notmatch '[0-9]' -or $Senha -notmatch '[^0-9A-Za-z]') {
    throw 'A senha precisa ter 12 caracteres, com maiuscula, minuscula, numero e caractere especial.'
}

function ConvertTo-EntitySearch {
    param([string]$Expression)
    return [Uri]::EscapeDataString($Expression)
}

function Format-SearchText {
    param([string]$Value)
    return 'S' + $Value
}

function Format-SearchInt {
    param($Value)
    return 'I' + ([int64]$Value)
}

function Invoke-LinxGet {
    param(
        [string]$Controller,
        [string]$Action,
        [string]$EntitySearch,
        [hashtable]$Headers
    )
    $url = "$ServiceUrl/$Controller/$Action"
    if ($EntitySearch) {
        $url += '?jEntitySearch=' + (ConvertTo-EntitySearch $EntitySearch)
    }
    $params = @{ Uri = $url; Method = 'Get'; TimeoutSec = $TimeoutSec; UseBasicParsing = $true }
    if ($Headers) { $params.Headers = $Headers }
    try {
        $response = Invoke-WebRequest @params
    }
    catch {
        $detail = $_.Exception.Message
        if ($_.Exception.Response) {
            $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
            $raw = $reader.ReadToEnd()
            $reader.Dispose()
            if ($raw) { $detail = $raw }
        }
        throw "Falha em ${Controller}/${Action}: $detail"
    }
    $text = $response.Content
    if ([string]::IsNullOrWhiteSpace($text)) { return @() }
    $data = $text | ConvertFrom-Json
    if ($null -eq $data) { return @() }
    return @($data)
}

function ConvertTo-JsonLiteral {
    param($Value)
    if ($null -eq $Value) { return 'null' }
    if ($Value -is [bool]) { if ($Value) { return 'true' } else { return 'false' } }
    if ($Value -is [byte] -or $Value -is [int] -or $Value -is [int64] -or $Value -is [double] -or $Value -is [decimal] -or $Value -is [single]) {
        return ([string]$Value).Replace(',', '.')
    }
    if ($Value -is [string]) {
        $escaped = $Value.Replace('\', '\\').Replace('"', '\"').Replace("`r", '\r').Replace("`n", '\n').Replace("`t", '\t')
        return '"' + $escaped + '"'
    }
    if ($Value -is [System.Collections.IDictionary]) {
        $parts = New-Object System.Collections.Generic.List[string]
        foreach ($key in @($Value.Keys)) {
            [void]$parts.Add((ConvertTo-JsonLiteral ([string]$key)) + ':' + (ConvertTo-JsonLiteral $Value[$key]))
        }
        return '{' + ($parts -join ',') + '}'
    }
    if ($Value -is [System.Collections.IEnumerable]) {
        $parts = New-Object System.Collections.Generic.List[string]
        foreach ($item in @($Value)) {
            [void]$parts.Add((ConvertTo-JsonLiteral $item))
        }
        return '[' + ($parts -join ',') + ']'
    }
    $parts = New-Object System.Collections.Generic.List[string]
    foreach ($prop in $Value.PSObject.Properties) {
        [void]$parts.Add((ConvertTo-JsonLiteral $prop.Name) + ':' + (ConvertTo-JsonLiteral $prop.Value))
    }
    return '{' + ($parts -join ',') + '}'
}

function Invoke-LinxSave {
    param(
        [string]$Controller,
        [array]$Entities,
        [hashtable]$Headers
    )
    $bundle = @{
        entities = @($Entities)
        saveOptions = @{ allowConcurrentSaves = $false; tag = $null }
    }
    $body = ConvertTo-JsonLiteral $bundle
    $params = @{
        Uri = "$ServiceUrl/$Controller/SaveChanges"
        Method = 'Post'
        TimeoutSec = $TimeoutSec
        UseBasicParsing = $true
        ContentType = 'application/json; charset=utf-8'
        Body = [System.Text.Encoding]::UTF8.GetBytes($body)
    }
    if ($Headers) { $params.Headers = $Headers }
    try {
        $response = Invoke-WebRequest @params
    }
    catch {
        $detail = $_.Exception.Message
        if ($_.Exception.Response) {
            $reader = New-Object System.IO.StreamReader($_.Exception.Response.GetResponseStream())
            $raw = $reader.ReadToEnd()
            $reader.Dispose()
            if ($raw) { $detail = $raw }
        }
        $detail = $detail -replace '(?i)(ConfirmacaoUsuario1?"\s*:\s*")[^"]*', '$1[redigido]'
        throw "Falha em $Controller/SaveChanges: $detail"
    }
    return ($response.Content | ConvertFrom-Json)
}

function New-Aspect {
    param(
        [string]$TypeName,
        [string]$State,
        [string]$KeyProperty
    )
    $aspect = [ordered]@{
        entityTypeName = $TypeName
        entityState = $State
        originalValuesMap = @{}
    }
    if ($State -eq 'Added' -and $KeyProperty) {
        $aspect.autoGeneratedKey = @{
            propertyName = $KeyProperty
            autoGeneratedKeyType = 'Identity'
        }
    }
    return $aspect
}

function New-AcessoAdmin {
    param($Ambiente, $IdUsuario, $TempId, $PadraoId, $AcessosDoAlvo)
    $acessoAlvo = @($AcessosDoAlvo) | Where-Object { [int]$_.IdTcsAmbiente -eq $Ambiente.IdTcsAmbiente } | Select-Object -First 1
    $multi = $false
    if ($acessoAlvo -and $acessoAlvo.IndicaMultiGpecon -eq $true) { $multi = $true }
    return [ordered]@{
        '$type' = 'Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso, Linx.Framework.BV'
        IdTcsUsuarioAcesso = $TempId
        IdUsuario = $IdUsuario
        IdTcsAmbiente = $Ambiente.IdTcsAmbiente
        IndicaAdministrador = $true
        IndicaMultiGpecon = $multi
        IndicaAcessoPadrao = ($Ambiente.IdTcsAmbiente -eq $PadraoId)
        entityAspect = New-Aspect -TypeName 'TcsUsuarioAcesso:#Linx.Framework.BV.UsuarioAutorizacao' -State 'Added' -KeyProperty 'IdTcsUsuarioAcesso'
    }
}

Write-Host "Service: $ServiceUrl"
Write-Host "Alvo: $LoginAlvo"

$usuarios = Invoke-LinxGet -Controller 'LinxFrameworkUsuarioAutorizacao' -Action 'GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioAutenticacao{NomeAutenticacao#==#" + (Format-SearchText $LoginAlvo) + "}")
$alvo = $usuarios | Where-Object { $_.NomeAutenticacao -eq $LoginAlvo } | Select-Object -First 1
if (-not $alvo) {
    throw "Usuario alvo nao encontrado: $LoginAlvo"
}

$idGpecon = [int]$alvo.IdLinx
$uidEmpresa = [string]$alvo.UidEmpresa
$nomeEmpresa = [string]$alvo.NomeEmpresa
if (-not $NomeAdmin) { $NomeAdmin = 'Admin ' + [string]$alvo.NomeUsuario }
if (-not $Email) { $Email = ($LoginAdmin -replace '[^a-zA-Z0-9\.\-]', '') + '@example.com' }
if ($Email -notmatch '^[^@\s]+@[^@\s]+\.[^@\s]+$') {
    throw "E-mail invalido para o Membership: $Email. Informe -Email com um endereco valido."
}

$jaExiste = Invoke-LinxGet -Controller 'LinxFrameworkUsuarioAutorizacao' -Action 'GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioAutenticacao{NomeAutenticacao#==#" + (Format-SearchText $LoginAdmin) + "}")
if ($jaExiste | Where-Object { $_.NomeAutenticacao -eq $LoginAdmin }) {
    throw "O login $LoginAdmin ja existe."
}

Write-Host "Empresa: $idGpecon - $nomeEmpresa"
Write-Host "Novo admin: $LoginAdmin"
Write-Host "E-mail: $Email"

$empresas = New-Object System.Collections.Generic.List[int]
[void]$empresas.Add($idGpecon)
$vinculos = @(Invoke-LinxGet -Controller 'LinxFrameworkEmpresa' -Action 'GetTcsEmpresaGpeconByEntitySearchNoAssociations' -EntitySearch ("TcsEmpresaGpecon{IdGrupoEconomico#==#" + (Format-SearchInt $idGpecon) + "}"))
foreach ($vinculo in $vinculos) {
    $idMembro = 0
    [void][int]::TryParse("$($vinculo.IdLinxGpecon)", [ref]$idMembro)
    if ($idMembro -gt 0 -and -not $empresas.Contains($idMembro)) {
        [void]$empresas.Add($idMembro)
    }
}

$ambientes = New-Object System.Collections.Generic.List[object]
foreach ($idEmpresa in $empresas) {
    $lista = @(Invoke-LinxGet -Controller 'LinxFrameworkEmpresa' -Action 'GetTcsAmbienteByEntitySearchNoAssociations' -EntitySearch ("TcsAmbiente{IdLinx#==#" + (Format-SearchInt $idEmpresa) + "}"))
    foreach ($ambiente in $lista) {
        if ($ambiente.EmDesenvolvimento -eq $true) { continue }
        if (-not ($ambientes | Where-Object { $_.IdTcsAmbiente -eq $ambiente.IdTcsAmbiente })) {
            [void]$ambientes.Add($ambiente)
        }
    }
}
if ($ambientes.Count -eq 0) {
    throw "Nenhum ambiente ativo encontrado para a empresa $idGpecon."
}

$ambientesSql = @()
foreach ($ambiente in ($ambientes | Sort-Object IdTcsAmbiente)) {
    $filtroConexao = "TcsAmbienteConexao{IdTcsAmbiente#==#" + (Format-SearchInt $ambiente.IdTcsAmbiente) + ";NomeConexao#==#SControleSistema}"
    $conexao = @(Invoke-LinxGet -Controller 'LinxFrameworkConexao' -Action 'GetTcsAmbienteConexaoByEntitySearchNoAssociations' -EntitySearch $filtroConexao) | Select-Object -First 1
    $servidor = ''
    $banco = ''
    $temString = $false
    $analitico = $false
    if ($conexao) {
        $servidor = [string]$conexao.NomeServidor
        $banco = [string]$conexao.NomeBanco
        $texto = (([string]$conexao.StringConexao) + ' ' + $servidor + ' ' + $banco)
        $temString = -not [string]::IsNullOrWhiteSpace([string]$conexao.StringConexao)
        $analitico = $texto -match 'asazure'
    }
    $item = [pscustomobject]@{
        IdTcsAmbiente = [int]$ambiente.IdTcsAmbiente
        Descricao = [string]$ambiente.DescricaoAmbiente
        Servidor = $servidor
        Banco = $banco
        ReplicaAplicacao = ($conexao -and $temString -and -not $analitico)
    }
    $ambientesSql += $item
    if ($item.ReplicaAplicacao) { $marca = 'aplicacao' }
    elseif (-not $conexao) { $marca = 'sem conexao de aplicacao' }
    elseif ($analitico) { $marca = 'banco analitico, sem regra de modulo' }
    else { $marca = 'conexao sem texto, somente portal' }
    Write-Host ("Ambiente {0} {1} [{2}] banco={3}" -f $item.IdTcsAmbiente, $item.Descricao, $marca, $item.Banco)
}

$acessosAlvo = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuarioAutorizacao' -Action 'GetTcsUsuarioAcessoByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioAcesso{IdUsuario#==#" + (Format-SearchInt $alvo.IdUsuario) + "}"))
$padraoId = $null
$comPadrao = $acessosAlvo | Where-Object { $_.IndicaAcessoPadrao -eq $true -and ($ambientesSql.IdTcsAmbiente -contains [int]$_.IdTcsAmbiente) } | Select-Object -First 1
if ($comPadrao) { $padraoId = [int]$comPadrao.IdTcsAmbiente }
if (-not $padraoId) { $padraoId = [int]($ambientesSql | Select-Object -First 1).IdTcsAmbiente }

$modulosCatalogo = @(Invoke-LinxGet -Controller 'LinxFrameworkModuloAutorizacao' -Action 'GetTcsModuloAutorizacaoNoAssociations')
$modulosEmpresa = @(Invoke-LinxGet -Controller 'LinxFrameworkEmpresa' -Action 'GetTcsEmpresaModuloByEntitySearchNoAssociations' -EntitySearch ("TcsEmpresaModulo{IdLinx#==#" + (Format-SearchInt $idGpecon) + "}"))
$idsEmpresa = @($modulosEmpresa | ForEach-Object { [int64]$_.IdModulo })
$modulosNovos = @($modulosCatalogo | Where-Object { $idsEmpresa -notcontains [int64]$_.IdModulo })
Write-Host ("Modulos do catalogo: {0}. Vinculos atuais da empresa: {1}. A liberar: {2}." -f $modulosCatalogo.Count, $modulosEmpresa.Count, $modulosNovos.Count)
Write-Host ("Acesso padrao: ambiente {0}. Acessos de administrador: {1}." -f $padraoId, $ambientesSql.Count)

foreach ($ambiente in $ambientesSql) {
    if (-not $ambiente.ReplicaAplicacao) { continue }
    $headersApp = @{
        CurrentCompany = $uidEmpresa
        EconomicGroup = $uidEmpresa
        Environment = "$($ambiente.IdTcsAmbiente)"
    }
    try {
        $modulosApp = @(Invoke-LinxGet -Controller 'LinxFrameworkModulo' -Action 'GetTcsModuloNoAssociations' -Headers $headersApp)
        $ativos = @($modulosApp | Where-Object { $_.Inativo -ne $true })
        Write-Host ("Ambiente {0}: {1} modulos ativos no banco {2}." -f $ambiente.IdTcsAmbiente, $ativos.Count, $ambiente.Banco)
    }
    catch {
        Write-Host ("Ambiente {0}: leitura dos modulos do aplicativo falhou. {1}" -f $ambiente.IdTcsAmbiente, $_.Exception.Message)
    }
}

$comReplica = @($ambientesSql | Where-Object { $_.ReplicaAplicacao })
$semReplica = @($ambientesSql | Where-Object { -not $_.ReplicaAplicacao })
if ($semReplica.Count -gt 0) {
    Write-Host 'Ambientes sem conexao de aplicacao recebem administrador no portal. A replica para o banco padrao do Service nao e disparada.'
}

if (-not $Confirmar) {
    Write-Host 'SIMULACAO: nada foi gravado. Execute de novo com -Confirmar para gravar.'
    return
}

$tempUsuario = -1
$agora = (Get-Date).ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ss.fffZ')
$uidNovo = [guid]::NewGuid().ToString()
$lxPfj = 1
if ($alvo.LxPfjFisicaJuridica) { $lxPfj = [int]$alvo.LxPfjFisicaJuridica }

$usuarioNovo = [ordered]@{
    '$type' = 'Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao, Linx.Framework.BV'
    IdUsuario = $tempUsuario
    UidUsuario = $uidNovo
    NomeAutenticacao = $LoginAdmin
    NomeCurtoUsuario = $LoginAdmin.Substring(0, [Math]::Min(60, $LoginAdmin.Length))
    NomeUsuario = $NomeAdmin
    Inativo = $false
    VigenciaInicial = '2000-01-01T00:00:00.000Z'
    VigenciaFinal = '2099-12-31T00:00:00.000Z'
    DataExpiracaoSenha = '2099-12-31T00:00:00.000Z'
    DataCadastro = $agora
    Email = $Email
    IdLinx = $idGpecon
    LxPfjFisicaJuridica = $lxPfj
    AutenticacaoWindows = $false
    IndicaAcessoSuporte = $false
    IndicaUsuarioServico = $false
    CriaUsuario = $false
    GeraSenhaUsuario = $false
    ConfirmacaoUsuario = $Senha
    ConfirmacaoUsuario1 = $Senha
    entityAspect = New-Aspect -TypeName 'TcsUsuarioAutenticacao:#Linx.Framework.BV.UsuarioAutorizacao' -State 'Added' -KeyProperty 'IdUsuario'
}

$entidades = New-Object System.Collections.Generic.List[object]
[void]$entidades.Add($usuarioNovo)
$tempAcesso = -1
foreach ($ambiente in $comReplica) {
    [void]$entidades.Add((New-AcessoAdmin -Ambiente $ambiente -IdUsuario $tempUsuario -TempId $tempAcesso -PadraoId $padraoId -AcessosDoAlvo $acessosAlvo))
    $tempAcesso--
}

$headerSessao = @{
    CurrentUser = [string]$alvo.UidUsuario
    CurrentCompany = $uidEmpresa
    EconomicGroup = $uidEmpresa
    Environment = "$padraoId"
}
Write-Host 'Gravando o login...'
[void](Invoke-LinxSave -Controller 'LinxFrameworkUsuarioAutorizacao' -Entities $entidades.ToArray() -Headers $headerSessao)
$criado = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuarioAutorizacao' -Action 'GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioAutenticacao{NomeAutenticacao#==#" + (Format-SearchText $LoginAdmin) + "}")) | Where-Object { $_.NomeAutenticacao -eq $LoginAdmin } | Select-Object -First 1
if (-not $criado) {
    throw 'O Service nao retornou o admin depois da gravacao.'
}
$idNovo = [int64]$criado.IdUsuario
Write-Host "Admin gravado. ID_USUARIO=$idNovo UID=$($criado.UidUsuario)"

if ($semReplica.Count -gt 0) {
    $paiPortal = [ordered]@{
        '$type' = 'Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao, Linx.Framework.BV'
        IdUsuario = $idNovo
        UidUsuario = [string]$criado.UidUsuario
        NomeAutenticacao = $LoginAdmin
        IdLinx = $idGpecon
        entityAspect = New-Aspect -TypeName 'TcsUsuarioAutenticacao:#Linx.Framework.BV.UsuarioAutorizacao' -State 'Unchanged'
    }
    $loteAcesso = New-Object System.Collections.Generic.List[object]
    [void]$loteAcesso.Add($paiPortal)
    foreach ($ambiente in $semReplica) {
        [void]$loteAcesso.Add((New-AcessoAdmin -Ambiente $ambiente -IdUsuario $idNovo -TempId $tempAcesso -PadraoId $padraoId -AcessosDoAlvo $acessosAlvo))
        $tempAcesso--
    }
    Write-Host "Gravando $($semReplica.Count) acessos de administrador no portal..."
    [void](Invoke-LinxSave -Controller 'LinxFrameworkUsuarioAutorizacao' -Entities $loteAcesso.ToArray() -Headers $headerSessao)
}

if ($modulosNovos.Count -gt 0) {
    $empresa = [ordered]@{
        '$type' = 'Linx.Framework.BV.Empresa.TcsEmpresaAutenticacao, Linx.Framework.BV'
        IdLinx = $idGpecon
        UidEmpresa = $uidEmpresa
        NomeEmpresa = $nomeEmpresa
        entityAspect = New-Aspect -TypeName 'TcsEmpresaAutenticacao:#Linx.Framework.BV.Empresa' -State 'Unchanged'
    }
    $lote = New-Object System.Collections.Generic.List[object]
    [void]$lote.Add($empresa)
    $tempModulo = -1
    foreach ($modulo in $modulosNovos) {
        $linha = [ordered]@{
            '$type' = 'Linx.Framework.BV.Empresa.TcsEmpresaModulo, Linx.Framework.BV'
            IdTcsEmpresaModulo = $tempModulo
            IdLinx = $idGpecon
            IdModulo = [int64]$modulo.IdModulo
            entityAspect = New-Aspect -TypeName 'TcsEmpresaModulo:#Linx.Framework.BV.Empresa' -State 'Added' -KeyProperty 'IdTcsEmpresaModulo'
        }
        [void]$lote.Add($linha)
        $tempModulo--
    }
    Write-Host "Liberando $($modulosNovos.Count) modulos para a empresa..."
    [void](Invoke-LinxSave -Controller 'LinxFrameworkEmpresa' -Entities $lote.ToArray() -Headers $headerSessao)
}

foreach ($ambiente in $ambientesSql) {
    if (-not $ambiente.ReplicaAplicacao) {
        Write-Host "Ambiente $($ambiente.IdTcsAmbiente): sem banco de aplicacao SQL, regras de modulo nao enviadas."
        continue
    }
    $headersApp = @{
        CurrentUser = [string]$criado.UidUsuario
        CurrentCompany = $uidEmpresa
        EconomicGroup = $uidEmpresa
        Environment = "$($ambiente.IdTcsAmbiente)"
    }
    try {
        $usuarioApp = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuario' -Action 'GetTcsUsuarioByEntitySearchNoAssociations' -EntitySearch ("TcsUsuario{IdUsuario#==#" + (Format-SearchInt $idNovo) + "}") -Headers $headersApp) | Where-Object { [int64]$_.IdUsuario -eq $idNovo } | Select-Object -First 1
        if (-not $usuarioApp) {
            Write-Host "Ambiente $($ambiente.IdTcsAmbiente): usuario nao replicado no banco $($ambiente.Banco)."
            continue
        }
        $modulosApp = @(Invoke-LinxGet -Controller 'LinxFrameworkModulo' -Action 'GetTcsModuloNoAssociations' -Headers $headersApp)
        $regras = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuario' -Action 'GetTcsUsuarioRegraModuloByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioRegraModulo{IdUsuario#==#" + (Format-SearchInt $idNovo) + "}") -Headers $headersApp)
        $idsRegra = @($regras | ForEach-Object { [int64]$_.IdModulo })
        $faltantes = @($modulosApp | Where-Object { $_.Inativo -ne $true -and $idsRegra -notcontains [int64]$_.IdModulo })
        if ($faltantes.Count -eq 0) {
            Write-Host "Ambiente $($ambiente.IdTcsAmbiente): acesso total ja coberto em $($modulosApp.Count) modulos."
            continue
        }
        $pai = [ordered]@{
            '$type' = 'Linx.Framework.BV.Usuario.TcsUsuario, Linx.Framework.BV'
            IdUsuario = $idNovo
            UidUsuario = [string]$criado.UidUsuario
            NomeUsuario = $NomeAdmin
            IdLinx = $idGpecon
            entityAspect = New-Aspect -TypeName 'TcsUsuario:#Linx.Framework.BV.Usuario' -State 'Unchanged'
        }
        $loteRegra = New-Object System.Collections.Generic.List[object]
        [void]$loteRegra.Add($pai)
        $tempRegra = -1
        foreach ($modulo in $faltantes) {
            $regra = [ordered]@{
                '$type' = 'Linx.Framework.BV.Usuario.TcsUsuarioRegraModulo, Linx.Framework.BV'
                IdUsuarioRegraModulo = $tempRegra
                IdUsuario = $idNovo
                IdModulo = [int64]$modulo.IdModulo
                LxRegraAcessoModulo = 2
                entityAspect = New-Aspect -TypeName 'TcsUsuarioRegraModulo:#Linx.Framework.BV.Usuario' -State 'Added' -KeyProperty 'IdUsuarioRegraModulo'
            }
            [void]$loteRegra.Add($regra)
            $tempRegra--
        }
        Write-Host "Ambiente $($ambiente.IdTcsAmbiente): gravando acesso total em $($faltantes.Count) modulos..."
        [void](Invoke-LinxSave -Controller 'LinxFrameworkUsuario' -Entities $loteRegra.ToArray() -Headers $headersApp)
    }
    catch {
        Write-Host "Ambiente $($ambiente.IdTcsAmbiente): $($_.Exception.Message)"
    }
}

Write-Host "Gravado. Entre com $LoginAdmin no ambiente $padraoId da empresa $nomeEmpresa."
