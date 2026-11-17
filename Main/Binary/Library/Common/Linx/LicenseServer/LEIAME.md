# Linx.LicenseServer.Client — versão 1.0.1

Biblioteca de licenciamento para aplicações .NET. **Esta entrega é um arquivo único:**
`Linx.LicenseServer.Client.dll` já contém todas as dependências internamente
(Newtonsoft.Json, LiteDB, DPAPI). Não há pacote NuGet, não há `restore`, não há
dependência para copiar junto — e o Newtonsoft que a sua aplicação usa não conflita com o
nosso, porque o nosso está internalizado.

## O que muda nesta versão (1.0.1)

Correção de comportamento na **liberação de licença** (chamada `Licensing/Revoke` ao License
Server). **Não há mudança de API**: nenhum tipo, método, propriedade ou valor de enum foi adicionado,
removido ou alterado.

**O problema corrigido:** na 1.0.0, quando a liberação falhava, a DLL repetia a chamada a cada
minuto, sem limite — inclusive quando o servidor respondia com erro definitivo (ex.: HTTP 400
"Licença de cliente não encontrada."). Uma aplicação com dado de licença errado chamava o servidor
indefinidamente.

| Resposta do servidor ao liberar | 1.0.0 | 1.0.1 |
|---|---|---|
| Confirma ("Licença Revogada.") | liberada | liberada (sem mudança) |
| Recusa com HTTP 4xx (ex.: 400) ou "Revogação não se aplica." | `RevocationPending`, repetindo a cada minuto para sempre | **liberada na primeira resposta**, sem repetir |
| Falha temporária (rede, HTTP 5xx, 401/403/408/429) | `RevocationPending`, repetindo a cada minuto para sempre | `RevocationPending`, repetindo com **espera crescente** (1, 1, 1, 2, 4, 8… até 512 min) |
| Falha temporária que persiste | — | após **13 tentativas** (~17h) a liberação é **concluída localmente** e a DLL para de chamar |

O contador de tentativas sobrevive ao reinício da aplicação e volta a zero quando a licença é
registrada de novo (`RegisterLicenseAsync`) ou liberada de novo (`ReleaseLicenseAsync`).

Correção adicional: uma licença liberada por inatividade cuja confirmação só chegava numa repetição
terminava com `Reason = Revoked`; agora mantém `IdleTimeoutExpired`.

### Impacto para quem integra

1. **A liberação passa a terminar em casos que antes ficavam pendentes.** Um `ReleaseLicenseAsync`
   que recebe HTTP 400 agora devolve `Reason = Revoked`, onde a 1.0.0 devolvia `RevocationPending`.
   Se o seu código avisa o operador ou tenta de novo quando a liberação fica pendente, esse caminho
   passa a ocorrer só em falha temporária.
2. **Liberada não significa mais, sempre, confirmada pelo servidor.** O status final distingue:

   | Como terminou | `ServerError` | `ServerData` |
   |---|---|---|
   | Servidor confirmou | nulo | nulo |
   | Servidor recusou com HTTP 4xx | status + texto | nulo |
   | Servidor respondeu "Revogação não se aplica." | nulo | mensagem do servidor |
   | 13 tentativas sem resposta; a última foi erro HTTP | status + texto | nulo |
   | 13 tentativas sem resposta; a última foi falha de rede | nulo | nulo (só o log interno registra) |

   Recusa por 4xx costuma indicar dado de licença errado na aplicação (CNPJ, chave, terminal).
3. **Cache local compatível.** Licenças gravadas pela 1.0.0 carregam normalmente; uma que estava
   pendente de liberação continua com até 13 tentativas novas, e um 400 a conclui na primeira.

### O que precisa ser feito após atualizar a DLL?

- **Obrigatório:** só trocar o arquivo e recompilar — passos em [Atualizando](#atualizando).
  Nenhuma alteração de código: a identidade do assembly (`AssemblyVersion 1.0.0.0`) é a mesma da
  1.0.0.
- **Recomendado:** revisar o tratamento do retorno de `ReleaseLicenseAsync`/`ShutdownAsync`
  conforme os itens 1 e 2 acima, e registrar em log `ServerError`/`ServerData` quando a liberação
  terminar com um deles preenchido — é assim que se encontram aplicações com dado de licença errado.

## Conteúdo

| Arquivo | Necessário? |
|---|---|
| `Linx.LicenseServer.Client.dll` | **Sim** — é a biblioteca |
| `Linx.LicenseServer.Client.xml` | Opcional — habilita IntelliSense no Visual Studio |
| `Linx.LicenseServer.Client.pdb` | Opcional — deixa a stack trace legível se algo falhar |

Mantenha os três na mesma pasta: o `.xml` e o `.pdb` são localizados pelo nome, ao lado da DLL.

## Como referenciar

Copie a pasta para dentro do seu repositório (ex.: `libs\`) e adicione ao `.csproj`.
**O trecho é o mesmo para .NET Framework e .NET 8:**

```xml
<ItemGroup>
  <Reference Include="Linx.LicenseServer.Client">
    <HintPath>libs\Linx.LicenseServer.Client.dll</HintPath>
    <Private>true</Private>
  </Reference>
</ItemGroup>
```

`Private=true` copia a DLL para a pasta de saída do seu build.

> Projeto .NET Framework no formato antigo (`packages.config`)? Funciona sem ajuste — não
> há dependência transitiva para o NuGet resolver. Basta a referência acima.

## Requisitos

- **Windows.** O cache local é protegido por DPAPI; não há suporte a Linux/macOS.
- **.NET Framework 4.7.2+** ou **.NET 6/8+** (a DLL é `netstandard2.0`).
- Permissão de escrita em `%ProgramData%` na primeira execução — é onde fica o cache
  cifrado da licença.

## Primeiro uso

```csharp
using Linx.LicenseServer.Client.Configuration;
using Linx.LicenseServer.Client.Licensing;

// Um singleton vivo durante toda a aplicação (recomendado).
using var manager = LicenseManagerFactory.Create(new LicenseManagerOptions
{
    ProductId   = "SEU-PRODUTO",   // fornecido pelo time do License Server
    Email       = cfg.Email,       // credencial do produto — nunca no código-fonte
    Password    = cfg.Password,
    Environment = LicenseServerEnvironment.Production,
});

manager.StatusChanged += (_, e) => { if (e.Current.IsBlocked) TratarBloqueio(e.Current.Reason); };

var status = await manager.RegisterLicenseAsync(new LicenseBinding
{
    LicenseId = 12345,
    Cnpj      = "00073196000144",
    KeyParts  = new[] { filial, terminal },
});

// No hot path: leitura local, síncrona, sem rede.
if (manager.GetLicenseStatus(binding).IsBlocked) { /* ... */ }
```

Só a fachada `ILicenseManager` é pública. O cliente HTTP e os modelos de request/response
são internos de propósito — toda a lógica de licenciamento (agenda de revalidação, cache,
integridade de relógio, fail-closed) fica na DLL.

## Conferindo a versão recebida

Botão direito na DLL → Propriedades → Detalhes, ou:

```powershell
(Get-Item Linx.LicenseServer.Client.dll).VersionInfo | Format-List FileVersion, ProductVersion
```

O `ProductVersion` traz também o commit de origem (`1.0.1+<hash>`), que identifica
exatamente o código compilado.

## Atualizando

1. Substitua `Linx.LicenseServer.Client.dll` (e o `.xml`/`.pdb`, se usar) na pasta referenciada
   pelo projeto.
2. Recompile e publique a aplicação normalmente. Não há `restore` nem referência a ajustar.
3. Confira a versão (seção acima): o `ProductVersion` deve começar com `1.0.1`.

Sem feed e sem `restore`, a versão em uso é a do arquivo que está na sua pasta — vale registrar a
versão recebida no seu controle de dependências.

---
Dúvidas ou nova versão: time do License Server.
