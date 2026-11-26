<#
    Exclui pela API o administrador criado por criar-admin-mesmo-gpecon.ps1.

    Apaga regras, perfil, filial e a replica do usuario em cada banco de
    aplicacao, depois os acessos do portal, os registros de TCS_LOG_ERROS
    desse login e o proprio login. O Membership sai junto com a exclusao do
    login, quando nao resta acesso. Nao mexe nos modulos da empresa.

    O controlador de log so responde com o cabecalho Application do cliente
    MID-e, o mesmo identificador que o servico ja aceita.

    Sem -Confirmar o script so mostra o que apagaria.
    O login precisa comecar com admin. para nao atingir o usuario alvo.

    Exemplo:
      .\excluir-admin-mesmo-gpecon.ps1 -LoginAdmin admin.teste.franqueado
      .\excluir-admin-mesmo-gpecon.ps1 -LoginAdmin admin.teste.franqueado -ServiceUrl https://qa-ux-svc.linx.com.br/3.11-NT -Confirmar
#>
[CmdletBinding()]
param(
    [string]$LoginAdmin = 'admin.teste.franqueado',
    [string]$ServiceUrl = 'http://127.0.0.1:1710',
    [int]$TimeoutSec = 180,
    [switch]$Confirmar
)

$ErrorActionPreference = 'Stop'
$ServiceUrl = $ServiceUrl.TrimEnd('/')
$LoginAdmin = $LoginAdmin.Trim()
if ([string]::IsNullOrWhiteSpace($LoginAdmin)) {
    throw 'Informe o login do administrador.'
}
if ($LoginAdmin -notlike 'admin.*') {
    throw "O login $LoginAdmin nao comeca com admin. Este script nao exclui o usuario alvo."
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

function Get-WebErrorDetail {
    param($ErrorRecord)
    if (-not [string]::IsNullOrWhiteSpace($ErrorRecord.ErrorDetails.Message)) {
        return [string]$ErrorRecord.ErrorDetails.Message
    }
    $response = $ErrorRecord.Exception.Response
    try {
        if ($response -and $response.PSObject.Methods['GetResponseStream']) {
            $reader = New-Object System.IO.StreamReader($response.GetResponseStream())
            $raw = $reader.ReadToEnd()
            $reader.Dispose()
            if ($raw) { return $raw }
        }
        if ($response -and $response.Content) {
            return [string]$response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
        }
    }
    catch { }
    return [string]$ErrorRecord.Exception.Message
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
        throw "Falha em ${Controller}/${Action}: $(Get-WebErrorDetail $_)"
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
        $detail = (Get-WebErrorDetail $_) -replace '(?i)(ConfirmacaoUsuario1?"\s*:\s*")[^"]*', '$1[redigido]'
        throw "Falha em $Controller/SaveChanges: $detail"
    }
    if ([string]::IsNullOrWhiteSpace($response.Content)) { return $null }
    return ($response.Content | ConvertFrom-Json)
}

function New-Aspect {
    param([string]$TypeName, [string]$State)
    return [ordered]@{
        entityTypeName = $TypeName
        entityState = $State
        originalValuesMap = @{}
    }
}

function New-DeletedEntity {
    param($TypeName, $AspectName, $Row, $KeyProperty)
    $entity = [ordered]@{
        '$type' = $TypeName
    }
    foreach ($prop in @($Row.PSObject.Properties)) {
        if ($prop.Name -in @('$id', '$type', 'entityAspect', 'ConfirmacaoUsuario', 'ConfirmacaoUsuario1')) { continue }
        $valor = $prop.Value
        if ($valor -is [datetime]) {
            $entity[$prop.Name] = $valor.ToUniversalTime().ToString('yyyy-MM-ddTHH:mm:ss.fffZ')
            continue
        }
        if ($null -eq $valor) { continue }
        $escalar = $valor -is [string] -or $valor -is [bool] -or $valor -is [byte] -or $valor -is [int] -or $valor -is [int64] -or $valor -is [double] -or $valor -is [decimal] -or $valor -is [single] -or $valor -is [guid]
        if (-not $escalar) { continue }
        $entity[$prop.Name] = $valor
    }
    $entity['entityAspect'] = New-Aspect -TypeName $AspectName -State 'Deleted'
    return $entity
}

$script:CacheUidEmpresa = @{}

function Get-UidEmpresa {
    param([int]$IdLinx)
    if ($script:CacheUidEmpresa.ContainsKey($IdLinx)) {
        return [string]$script:CacheUidEmpresa[$IdLinx]
    }
    $empresa = @(Invoke-LinxGet -Controller 'LinxFrameworkEmpresa' -Action 'GetTcsEmpresaAutenticacaoByEntitySearchNoAssociations' -EntitySearch ("TcsEmpresaAutenticacao{IdLinx#==#" + (Format-SearchInt $IdLinx) + "}")) | Where-Object { [int]$_.IdLinx -eq $IdLinx } | Select-Object -First 1
    $uid = ''
    if ($empresa) { $uid = [string]$empresa.UidEmpresa }
    $script:CacheUidEmpresa[$IdLinx] = $uid
    return $uid
}

function New-PaiModificado {
    $pai = New-DeletedEntity -TypeName 'Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao, Linx.Framework.BV' -AspectName 'TcsUsuarioAutenticacao:#Linx.Framework.BV.UsuarioAutorizacao' -Row $admin -KeyProperty 'IdUsuario'
    $pai['entityAspect'] = New-Aspect -TypeName 'TcsUsuarioAutenticacao:#Linx.Framework.BV.UsuarioAutorizacao' -State 'Modified'
    return $pai
}

function Save-FilhosDoLogin {
    param([array]$Filhos, [hashtable]$Headers)
    $lote = @((New-PaiModificado))
    foreach ($filho in @($Filhos)) {
        $lote += $filho
        if ($lote.Count -ge 40) {
            [void](Invoke-LinxSave -Controller 'LinxFrameworkUsuarioAutorizacao' -Entities $lote -Headers $Headers)
            $lote = @((New-PaiModificado))
        }
    }
    if ($lote.Count -gt 1) {
        [void](Invoke-LinxSave -Controller 'LinxFrameworkUsuarioAutorizacao' -Entities $lote -Headers $Headers)
    }
}

function New-DashLog {
    param($Row)
    $dash = New-DeletedEntity -TypeName 'Linx.Framework.BV.TratamentoErros.TcsLogErrosDash, Linx.Framework.BV' -AspectName 'TcsLogErrosDash:#Linx.Framework.BV.TratamentoErros' -Row $Row -KeyProperty 'IdTcsLogErros'
    foreach ($nome in @('EnderecoWeb', 'MensagemExcecao', 'MetodoHttp', 'NomeAcao', 'NomeControlador', 'NomeServidor', 'PilhaExcecao', 'UsuarioWindows')) {
        if (-not $dash.Contains($nome)) { $dash[$nome] = '' }
    }
    $dash['EntityUniqueKey'] = [guid]::NewGuid().ToString()
    $dash['entityAspect'] = New-Aspect -TypeName 'TcsLogErrosDash:#Linx.Framework.BV.TratamentoErros' -State 'Modified'
    return $dash
}

function Save-Logs {
    param([array]$Linhas, [hashtable]$Headers)
    $lote = @((New-DashLog -Row $Linhas[0]))
    foreach ($linha in @($Linhas)) {
        $lote += (New-DeletedEntity -TypeName 'Linx.Framework.BV.TratamentoErros.TcsLogErros, Linx.Framework.BV' -AspectName 'TcsLogErros:#Linx.Framework.BV.TratamentoErros' -Row $linha -KeyProperty 'IdTcsLogErros')
        if ($lote.Count -ge 20) {
            [void](Invoke-LinxSave -Controller 'LinxFrameworkTratamentoErros' -Entities $lote -Headers $Headers)
            $lote = @((New-DashLog -Row $Linhas[0]))
        }
    }
    if ($lote.Count -gt 1) {
        [void](Invoke-LinxSave -Controller 'LinxFrameworkTratamentoErros' -Entities $lote -Headers $Headers)
    }
}

function Save-Deletes {
    param([string]$Controller, [array]$Entities, [hashtable]$Headers)
    $lote = New-Object System.Collections.Generic.List[object]
    foreach ($entity in @($Entities)) {
        [void]$lote.Add($entity)
        if ($lote.Count -ge 40) {
            [void](Invoke-LinxSave -Controller $Controller -Entities $lote.ToArray() -Headers $Headers)
            $lote.Clear()
        }
    }
    if ($lote.Count -gt 0) {
        [void](Invoke-LinxSave -Controller $Controller -Entities $lote.ToArray() -Headers $Headers)
    }
}

function Remove-Consulta {
    param(
        [string]$Controller,
        [string]$Entity,
        [string]$TypeName,
        [string]$AspectName,
        [string]$KeyProperty,
        [string]$Search,
        [hashtable]$Headers,
        [string]$Rotulo,
        [hashtable]$Vistos,
        [switch]$Interromper
    )
    try {
        $linhas = @(Invoke-LinxGet -Controller $Controller -Action ("Get{0}ByEntitySearchNoAssociations" -f $Entity) -EntitySearch $Search -Headers $Headers)
    }
    catch {
        if ($Interromper) { throw "Leitura de $Entity falhou. $($_.Exception.Message)" }
        Write-Host "$Rotulo : leitura de $Entity falhou. $($_.Exception.Message)"
        return
    }
    $validas = @($linhas | Where-Object { $null -ne $_.$KeyProperty -and "$($_.$KeyProperty)" -ne '' })
    if ($Vistos) {
        $ainda = @()
        foreach ($linha in $validas) {
            $chave = $Entity + '|' + [string]$linha.$KeyProperty
            if ($Vistos.ContainsKey($chave)) { continue }
            $Vistos[$chave] = $true
            $ainda += $linha
        }
        $validas = $ainda
    }
    Write-Host ("{0}: {1} {2}." -f $Rotulo, $validas.Count, $Entity)
    if ($validas.Count -eq 0 -or -not $Confirmar) { return }
    $entidades = @($validas | ForEach-Object { New-DeletedEntity -TypeName $TypeName -AspectName $AspectName -Row $_ -KeyProperty $KeyProperty })
    Save-Deletes -Controller $Controller -Entities $entidades -Headers $Headers
}

Write-Host "Service: $ServiceUrl"
Write-Host "Login: $LoginAdmin"

$usuarios = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuarioAutorizacao' -Action 'GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioAutenticacao{NomeAutenticacao#==#" + (Format-SearchText $LoginAdmin) + "}"))
$admin = $usuarios | Where-Object { $_.NomeAutenticacao -eq $LoginAdmin } | Select-Object -First 1
if (-not $admin) {
    Write-Host "Login $LoginAdmin nao existe. Nada a excluir."
    return
}

$idUsuario = [int64]$admin.IdUsuario
$uidUsuario = [string]$admin.UidUsuario
$idGpecon = [int]$admin.IdLinx
$uidEmpresa = [string]$admin.UidEmpresa
if ([string]::IsNullOrWhiteSpace($uidEmpresa) -and $idGpecon -gt 0) {
    $uidEmpresa = Get-UidEmpresa -IdLinx $idGpecon
}
Write-Host "ID_USUARIO=$idUsuario empresa=$idGpecon"

$acessos = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuarioAutorizacao' -Action 'GetTcsUsuarioAcessoByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioAcesso{IdUsuario#==#" + (Format-SearchInt $idUsuario) + "}"))
Write-Host ("Acessos no portal: {0}." -f $acessos.Count)

$ambientes = @()
foreach ($acesso in $acessos) {
    $idAmbiente = 0
    [void][int]::TryParse("$($acesso.IdTcsAmbiente)", [ref]$idAmbiente)
    if ($idAmbiente -le 0) { continue }
    if ($ambientes | Where-Object { $_.IdTcsAmbiente -eq $idAmbiente }) { continue }
    $ambiente = @(Invoke-LinxGet -Controller 'LinxFrameworkEmpresa' -Action 'GetTcsAmbienteByEntitySearchNoAssociations' -EntitySearch ("TcsAmbiente{IdTcsAmbiente#==#" + (Format-SearchInt $idAmbiente) + "}")) | Select-Object -First 1
    $filtroConexao = "TcsAmbienteConexao{IdTcsAmbiente#==#" + (Format-SearchInt $idAmbiente) + ";NomeConexao#==#SControleSistema}"
    $conexao = @(Invoke-LinxGet -Controller 'LinxFrameworkConexao' -Action 'GetTcsAmbienteConexaoByEntitySearchNoAssociations' -EntitySearch $filtroConexao) | Select-Object -First 1
    $stringConexao = ''
    $nomeBanco = ''
    if ($conexao) {
        $stringConexao = [string]$conexao.StringConexao
        $nomeBanco = [string]$conexao.NomeBanco
        if ([string]::IsNullOrWhiteSpace($stringConexao)) {
            $idBanco = 0
            [void][int]::TryParse("$($conexao.IdTcsBancoServidor)", [ref]$idBanco)
            if ($idBanco -gt 0) {
                $bancoSrv = @(Invoke-LinxGet -Controller 'LinxFrameworkConexao' -Action 'GetTcsBancoServidorByEntitySearchNoAssociations' -EntitySearch ("TcsBancoServidor{IdTcsBancoServidor#==#" + (Format-SearchInt $idBanco) + "}")) | Where-Object { [int]$_.IdTcsBancoServidor -eq $idBanco } | Select-Object -First 1
                if ($bancoSrv) {
                    $stringConexao = [string]$bancoSrv.StringConexao
                    if ([string]::IsNullOrWhiteSpace($nomeBanco)) { $nomeBanco = [string]$bancoSrv.NomeBanco }
                }
            }
        }
    }
    $replica = ($conexao -and -not [string]::IsNullOrWhiteSpace($stringConexao) -and ($stringConexao + ' ' + $nomeBanco) -notmatch 'asazure')
    $idLinxAmbiente = $idGpecon
    if ($ambiente) {
        $lido = 0
        [void][int]::TryParse("$($ambiente.IdLinx)", [ref]$lido)
        if ($lido -gt 0) { $idLinxAmbiente = $lido }
    }
    $uidAmbiente = ''
    if ($idLinxAmbiente -eq $idGpecon) { $uidAmbiente = $uidEmpresa }
    else { $uidAmbiente = Get-UidEmpresa -IdLinx $idLinxAmbiente }
    if ([string]::IsNullOrWhiteSpace($uidAmbiente)) { $uidAmbiente = $uidEmpresa }
    $ambientes += [pscustomobject]@{
        IdTcsAmbiente = $idAmbiente
        ReplicaAplicacao = [bool]$replica
        IdLinx = $idLinxAmbiente
        UidEmpresa = $uidAmbiente
        Banco = $nomeBanco
        IdTcsBancoServidor = $(if ($conexao) { [int]$conexao.IdTcsBancoServidor } else { 0 })
    }
    Write-Host ("Ambiente {0} empresa {1} replica={2} banco={3}" -f $idAmbiente, $idLinxAmbiente, $replica, $nomeBanco)
}

$filhosApp = @(
    @{ Entity = 'TcsUsuarioRegraModulo'; Type = 'Linx.Framework.BV.Usuario.TcsUsuarioRegraModulo, Linx.Framework.BV'; Aspect = 'TcsUsuarioRegraModulo:#Linx.Framework.BV.Usuario'; Key = 'IdUsuarioRegraModulo'; Controller = 'LinxFrameworkUsuario' },
    @{ Entity = 'TcsUsuarioRegraTransacao'; Type = 'Linx.Framework.BV.Usuario.TcsUsuarioRegraTransacao, Linx.Framework.BV'; Aspect = 'TcsUsuarioRegraTransacao:#Linx.Framework.BV.Usuario'; Key = 'IdUsuarioRegraTransacao'; Controller = 'LinxFrameworkUsuario' },
    @{ Entity = 'TcsUsuarioRegraColuna'; Type = 'Linx.Framework.BV.Usuario.TcsUsuarioRegraColuna, Linx.Framework.BV'; Aspect = 'TcsUsuarioRegraColuna:#Linx.Framework.BV.Usuario'; Key = 'IdUsuarioRegraColuna'; Controller = 'LinxFrameworkUsuario' },
    @{ Entity = 'TcsUsuarioPerfil'; Type = 'Linx.Framework.BV.Usuario.TcsUsuarioPerfil, Linx.Framework.BV'; Aspect = 'TcsUsuarioPerfil:#Linx.Framework.BV.Usuario'; Key = 'IdTcsUsuarioPerfil'; Controller = 'LinxFrameworkUsuario' },
    @{ Entity = 'TcsUsuarioFilial'; Type = 'Linx.Framework.BV.Usuario.TcsUsuarioFilial, Linx.Framework.BV'; Aspect = 'TcsUsuarioFilial:#Linx.Framework.BV.Usuario'; Key = 'IdTcsUsuarioFilial'; Controller = 'LinxFrameworkUsuario' },
    @{ Entity = 'TcsUsuarioGpecon'; Type = 'Linx.Framework.BV.GrupoEconomico.TcsUsuarioGpecon, Linx.Framework.BV'; Aspect = 'TcsUsuarioGpecon:#Linx.Framework.BV.GrupoEconomico'; Key = 'IdUsuarioGpecon'; Controller = 'LinxFrameworkGrupoEconomico' }
)

$buscaUsuario = "IdUsuario#==#" + (Format-SearchInt $idUsuario)
$bancosTratados = @{}
foreach ($ambiente in $ambientes) {
    if (-not $ambiente.ReplicaAplicacao) { continue }
    if ($ambiente.IdTcsBancoServidor -gt 0 -and $bancosTratados.ContainsKey($ambiente.IdTcsBancoServidor)) {
        Write-Host "Ambiente $($ambiente.IdTcsAmbiente): banco $($ambiente.Banco) ja tratado em outro ambiente."
        continue
    }
    if ($ambiente.IdTcsBancoServidor -gt 0) { $bancosTratados[$ambiente.IdTcsBancoServidor] = $true }
    $vistos = @{}
    $sessoes = @([pscustomobject]@{ IdLinx = $idGpecon; Uid = $uidEmpresa })
    if ($ambiente.IdLinx -ne $idGpecon -and -not [string]::IsNullOrWhiteSpace($ambiente.UidEmpresa)) {
        $sessoes += [pscustomobject]@{ IdLinx = $ambiente.IdLinx; Uid = $ambiente.UidEmpresa }
    }
    foreach ($sessao in $sessoes) {
        $headers = @{
            CurrentUser = $uidUsuario
            CurrentCompany = [string]$sessao.Uid
            EconomicGroup = $uidEmpresa
            Environment = "$($ambiente.IdTcsAmbiente)"
        }
        $rotulo = "Ambiente $($ambiente.IdTcsAmbiente) empresa $($sessao.IdLinx)"
        foreach ($filho in $filhosApp) {
            Remove-Consulta -Controller $filho.Controller -Entity $filho.Entity -TypeName $filho.Type -AspectName $filho.Aspect -KeyProperty $filho.Key -Search ("{0}{{{1}}}" -f $filho.Entity, $buscaUsuario) -Headers $headers -Rotulo $rotulo -Vistos $vistos
        }
    }
    $headersGrupo = @{
        CurrentUser = $uidUsuario
        CurrentCompany = $uidEmpresa
        EconomicGroup = $uidEmpresa
        Environment = "$($ambiente.IdTcsAmbiente)"
    }
    $usuarioApp = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuario' -Action 'GetTcsUsuarioByEntitySearchNoAssociations' -EntitySearch ("TcsUsuario{$buscaUsuario}") -Headers $headersGrupo) | Where-Object { [int64]$_.IdUsuario -eq $idUsuario } | Select-Object -First 1
    if ($usuarioApp) {
        Write-Host "Ambiente $($ambiente.IdTcsAmbiente): replica de TcsUsuario encontrada."
        if ($Confirmar) {
            $apagado = New-DeletedEntity -TypeName 'Linx.Framework.BV.Usuario.TcsUsuario, Linx.Framework.BV' -AspectName 'TcsUsuario:#Linx.Framework.BV.Usuario' -Row $usuarioApp -KeyProperty 'IdUsuario'
            [void](Invoke-LinxSave -Controller 'LinxFrameworkUsuario' -Entities @($apagado) -Headers $headersGrupo)
            Write-Host "Ambiente $($ambiente.IdTcsAmbiente): replica excluida."
        }
    }
}

$headerPortal = @{
    CurrentUser = $uidUsuario
    CurrentCompany = $uidEmpresa
    EconomicGroup = $uidEmpresa
}
if ($ambientes.Count -gt 0) { $headerPortal.Environment = "$($ambientes[0].IdTcsAmbiente)" }

$porEmpresa = @{}
foreach ($acesso in $acessos) {
    $idEmpresaAcesso = 0
    [void][int]::TryParse("$($acesso.IdLinxEmpresa)", [ref]$idEmpresaAcesso)
    if ($idEmpresaAcesso -le 0) { $idEmpresaAcesso = $idGpecon }
    if ($porEmpresa.ContainsKey($idEmpresaAcesso)) { $porEmpresa[$idEmpresaAcesso] = @($porEmpresa[$idEmpresaAcesso]) + @($acesso) }
    else { $porEmpresa[$idEmpresaAcesso] = @($acesso) }
}
foreach ($idEmpresaAcesso in @($porEmpresa.Keys)) {
    $uidAcesso = $uidEmpresa
    if ($idEmpresaAcesso -ne $idGpecon) {
        $resolvido = Get-UidEmpresa -IdLinx ([int]$idEmpresaAcesso)
        if (-not [string]::IsNullOrWhiteSpace($resolvido)) { $uidAcesso = $resolvido }
    }
    $headersAcesso = @{
        CurrentUser = $uidUsuario
        CurrentCompany = $uidAcesso
        EconomicGroup = $uidEmpresa
    }
    if ($ambientes.Count -gt 0) { $headersAcesso.Environment = "$($ambientes[0].IdTcsAmbiente)" }
    $linhasAcesso = @($porEmpresa[$idEmpresaAcesso])
    Write-Host ("Portal: {0} TcsUsuarioAcesso na empresa {1}." -f $linhasAcesso.Count, $idEmpresaAcesso)
    if ($Confirmar -and $linhasAcesso.Count -gt 0) {
        $apagados = @($linhasAcesso | ForEach-Object { New-DeletedEntity -TypeName 'Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAcesso, Linx.Framework.BV' -AspectName 'TcsUsuarioAcesso:#Linx.Framework.BV.UsuarioAutorizacao' -Row $_ -KeyProperty 'IdTcsUsuarioAcesso' })
        Save-FilhosDoLogin -Filhos $apagados -Headers $headersAcesso
    }
}

$filhosPortal = @(
    @{ Entity = 'TcsUsuarioGpecon'; Type = 'Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioGpecon, Linx.Framework.BV'; Aspect = 'TcsUsuarioGpecon:#Linx.Framework.BV.UsuarioAutorizacao'; Key = 'IdTcsUsuarioAutGpecon' },
    @{ Entity = 'TcsIdentidadeExterna'; Type = 'Linx.Framework.BV.UsuarioAutorizacao.TcsIdentidadeExterna, Linx.Framework.BV'; Aspect = 'TcsIdentidadeExterna:#Linx.Framework.BV.UsuarioAutorizacao'; Key = 'IdIdentidadeExterna' }
)
foreach ($filho in $filhosPortal) {
    Remove-Consulta -Controller 'LinxFrameworkUsuarioAutorizacao' -Entity $filho.Entity -TypeName $filho.Type -AspectName $filho.Aspect -KeyProperty $filho.Key -Search ("{0}{{{1}}}" -f $filho.Entity, $buscaUsuario) -Headers $headerPortal -Rotulo 'Portal' -Vistos @{}
}

$headerLog = @{}
foreach ($chave in @($headerPortal.Keys)) { $headerLog[$chave] = $headerPortal[$chave] }
$headerLog.Application = 'DE02E733-A636-4C41-907F-55C8721FCCDF'
$logs = @(Invoke-LinxGet -Controller 'LinxFrameworkTratamentoErros' -Action 'GetTcsLogErrosByEntitySearchNoAssociations' -EntitySearch ("TcsLogErros{$buscaUsuario}") -Headers $headerLog)
$logs = @($logs | Where-Object { $null -ne $_.IdTcsLogErros -and "$($_.IdTcsLogErros)" -ne '' })
Write-Host "Portal: $($logs.Count) TcsLogErros."
if ($Confirmar -and $logs.Count -gt 0) {
    Save-Logs -Linhas $logs -Headers $headerLog
    $logsRestantes = @(Invoke-LinxGet -Controller 'LinxFrameworkTratamentoErros' -Action 'GetTcsLogErrosByEntitySearchNoAssociations' -EntitySearch ("TcsLogErros{$buscaUsuario}") -Headers $headerLog)
    $logsRestantes = @($logsRestantes | Where-Object { $null -ne $_.IdTcsLogErros -and "$($_.IdTcsLogErros)" -ne '' })
    if ($logsRestantes.Count -gt 0) {
        throw "Ainda restam $($logsRestantes.Count) registros de log. O login nao foi apagado."
    }
}

if (-not $Confirmar) {
    Write-Host 'SIMULACAO: nada foi excluido. Execute de novo com -Confirmar para apagar o login e permitir a inclusao.'
    return
}

$login = New-DeletedEntity -TypeName 'Linx.Framework.BV.UsuarioAutorizacao.TcsUsuarioAutenticacao, Linx.Framework.BV' -AspectName 'TcsUsuarioAutenticacao:#Linx.Framework.BV.UsuarioAutorizacao' -Row $admin -KeyProperty 'IdUsuario'
[void](Invoke-LinxSave -Controller 'LinxFrameworkUsuarioAutorizacao' -Entities @($login) -Headers $headerPortal)

$restou = @(Invoke-LinxGet -Controller 'LinxFrameworkUsuarioAutorizacao' -Action 'GetTcsUsuarioAutenticacaoByEntitySearchNoAssociations' -EntitySearch ("TcsUsuarioAutenticacao{NomeAutenticacao#==#" + (Format-SearchText $LoginAdmin) + "}")) | Where-Object { $_.NomeAutenticacao -eq $LoginAdmin }
if ($restou) {
    throw "O login $LoginAdmin continua no portal depois da exclusao."
}
try {
    [void](Invoke-WebRequest -Uri "$ServiceUrl/LinxFrameworkModulo/CleanUserModulesCache" -Method Get -TimeoutSec $TimeoutSec -UseBasicParsing)
}
catch {
    Write-Host "Cache de modulos nao limpo: $($_.Exception.Message)"
}
Write-Host "Excluido: $LoginAdmin. O script de inclusao pode gravar este login de novo."
