# Consumo das APIs de login (SSO + MFA) em aplicação desktop .NET

Este texto é o **cookbook** do guia [login-mfa-sso-api.md](login-mfa-sso-api.md). Destina-se a um cliente **WPF / WinForms / console** em .NET Framework 4.8 ou .NET 6+, falando **direto com o Service** (`:1710`). Não use o cookie Forms do Portal.

O desktop **orquestra as mesmas APIs** que o Portal:

1. Provar identidade (senha **ou** Microsoft).
2. Listar ambientes / GPECON.
3. Cumprir MFA Linx (QR + 6 dígitos, ou skip).
4. Abrir sessão de Application (`AuthenticateUser`) com o ticket.

Não existe um POST único “login completo”.

---

## 1. O que o desktop faz de diferente do Portal

| Portal (browser) | Desktop .NET |
|------------------|--------------|
| MSAL **confidential** (client secret) + redirect `/Account/SsoCallback` | MSAL **public client** (sem secret), `AcquireTokenInteractive` |
| Cookie Forms depois do 1º fator | Guarde `NomeAutenticacao` em memória |
| Redirect HTTP para Application | Chame `AuthenticateUser` e use o `LoginInfo` / `Token` nas chamadas seguintes |

Regras iguais:

- SSO Azure **não** substitui TOTP Linx.
- MFA só depois de conhecido o `IdLinxGpecon`.
- `AuthenticatePortalSso` **não** recebe o access token. Só o login local (UPN antes de `@`).
- Referencie `Linx.Security.Cryptography` (assembly do Portal / `Linx.Desktop.Tools`). **Não** reimplemente o algoritmo.

No Entra ID, o app desktop precisa de plataforma **Mobile and desktop** (public client), redirect típico `http://localhost` ou `https://login.microsoftonline.com/common/oauth2/nativeclient`. O registro Web do Portal (com secret) **não** serve sozinho para MSAL desktop.

---

## 2. Pacotes e config

POC compilável: `samples/LinxUxAuthDesktopPoc/` (`dotnet run -- --libs`). No host AWS Windows a pipeline copia docs + sample para `C:\Sample-SSO-MFA`.

| Biblioteca | Versão | Obrigatória? |
|------------|--------|----------------|
| `Newtonsoft.Json` | 13.0.3 | sim |
| `Microsoft.Identity.Client` | 4.83.0 | só SSO |
| `System.Net.Http` | BCL | sim |
| `Linx.Security.Cryptography` | produto | sim para senha (`AuthenticatePortal`) |

A POC **linka** `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/Cryptography.cs`. Em um exe Windows de produção, referencie `Linx.Tools.dll` (GAC Binary).

```xml
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="Microsoft.Identity.Client" Version="4.83.0" />
```

```json
{
  "LinxServiceUrl": "http://localhost:1710/",
  "AzureAd": {
    "ClientId": "<PUBLIC_CLIENT_ID>",
    "TenantId": "<DIRECTORY_TENANT_ID>",
    "RedirectUri": "http://localhost",
    "Scopes": [ "User.Read" ]
  }
}
```

`AcessoLocal` em `PortalUserAccess`: `true` só se o Service estiver na mesma máquina que o cliente (equivalente a `Request.IsLocal` no Portal). Contra o IIS remoto, use `false`.

---

## 3. Contratos JSON (DTOs)

```csharp
using System;
using System.Collections.Generic;

public sealed class AmbienteAcesso
{
    public int IdTcsAmbiente { get; set; }
    public string DescricaoAmbiente { get; set; }
    public Guid UidAplicacao { get; set; }
    public Guid UidEmpresa { get; set; }
    public Guid UidGrupoEconomico { get; set; }
    public Guid UidUsuario { get; set; }
    public string NomeEmpresa { get; set; }
    public string GrupoEconomico { get; set; }
    public int IdLinxGpecon { get; set; }
    public bool IndicaAcessoPadrao { get; set; }
    public string Url { get; set; }
    public string NomeAutenticacao { get; set; }
}

public sealed class MfaStatusDto
{
    public bool RequiresMfa { get; set; }
    public bool Enrolled { get; set; }
    public bool MfaLocked { get; set; }
    public string SkipReason { get; set; }
    public Guid? UidUsuario { get; set; }
    public int IdGpecon { get; set; }
    public string NomeAutenticacao { get; set; }
}

public sealed class MfaEnrollDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string OtpauthUri { get; set; }
    public string AccountLabel { get; set; }
    public string QrCodePngBase64 { get; set; }
}

public sealed class MfaTicketDto
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Ticket { get; set; }
    public DateTime? TicketExpiresUtc { get; set; }
    public bool MfaLocked { get; set; }
}

public sealed class LoginInfoDto
{
    public Guid UidUsuario { get; set; }
    public long IdUsuario { get; set; }
    public string NomeUsuario { get; set; }
    public string NomeCurtoUsuario { get; set; }
    public int IdLinxGrupoEconomico { get; set; }
    public List<AmbienteTokenDto> Ambientes { get; set; }
}

public sealed class AmbienteTokenDto
{
    public int IdTcsAmbiente { get; set; }
    public Guid Token { get; set; }
    public Guid UidAplicacao { get; set; }
    public Guid UidEmpresa { get; set; }
    public string UrlServiceBus { get; set; }
}
```

---

## 4. Cliente HTTP do Service

O Service devolve `AuthenticatePortal` / `AuthenticatePortalSso` como **string JSON** (aspas + URL-encode + `Encrypt`). MFA e `AuthenticateUser` devolvem JSON de objeto.

```csharp
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Linx.Security;
using Newtonsoft.Json;

public sealed class LinxUxAuthClient : IDisposable
{
    public const string OriginUx = "UX";

    private readonly HttpClient _http;
    private readonly Cryptography _crypto = new Cryptography();

    public LinxUxAuthClient(string serviceBaseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(serviceBaseUrl.TrimEnd('/') + "/") };
        _http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _http.DefaultRequestHeaders.Add("X-Auth-Channel", "Desktop");
    }

    public void Dispose() => _http.Dispose();

    public async Task<string> LoginComSenhaAsync(string usuario, string senha)
    {
        string inner = _crypto.Encrypt(usuario.Trim()) + "||" + _crypto.Encrypt(senha.Trim());
        string envelope = _crypto.Encrypt(inner);

        var url = "LinxFrameworkAutorizacao/AuthenticatePortal?authenticateParameters="
                  + Uri.EscapeDataString(envelope);
        string[] parts = await GetEncryptedPartsAsync(url);
        if (DecryptPart(parts, 0) != "1")
            throw new InvalidOperationException(DecryptPart(parts, 1));
        return usuario.Trim();
    }

    public async Task<string> LoginAposSsoAsync(string nomeAutenticacao)
    {
        var url = "LinxFrameworkAutorizacao/AuthenticatePortalSso?userName="
                  + Uri.EscapeDataString(nomeAutenticacao);
        string[] parts = await GetEncryptedPartsAsync(url);
        if (DecryptPart(parts, 0) != "1")
            throw new InvalidOperationException(DecryptPart(parts, 1));
        return parts.Length > 3 ? DecryptPart(parts, 3) : nomeAutenticacao;
    }

    public async Task<List<AmbienteAcesso>> ListarAmbientesAsync(string nomeAutenticacao, bool acessoLocal = false)
    {
        var body = new
        {
            NomeAutenticacao = nomeAutenticacao,
            AcessoLocal = acessoLocal,
            Parametros = ""
        };
        using (var req = new HttpRequestMessage(HttpMethod.Post, "LinxFrameworkUsuarioAutorizacao/PortalUserAccess"))
        {
            req.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            using (HttpResponseMessage resp = await _http.SendAsync(req).ConfigureAwait(false))
            {
                string json = await EnsureJsonAsync(resp).ConfigureAwait(false);
                return JsonConvert.DeserializeObject<List<AmbienteAcesso>>(json)
                       ?? new List<AmbienteAcesso>();
            }
        }
    }

    public Task<MfaStatusDto> GetMfaStatusAsync(Guid uidUsuario, int idGpecon)
    {
        return GetJsonAsync<MfaStatusDto>(
            "LinxFrameworkAutorizacao/GetMfaStatus"
            + "?tableOrigin=" + OriginUx
            + "&idGpecon=" + idGpecon
            + "&uidUsuario=" + uidUsuario);
    }

    public Task<MfaEnrollDto> BeginEnrollAsync(Guid uidUsuario, int idGpecon)
    {
        return GetJsonAsync<MfaEnrollDto>(
            "LinxFrameworkAutorizacao/BeginMfaEnrollment"
            + "?tableOrigin=" + OriginUx
            + "&idGpecon=" + idGpecon
            + "&uidUsuario=" + uidUsuario);
    }

    public Task<MfaTicketDto> ConfirmEnrollAsync(Guid uidUsuario, int idGpecon, string code6)
    {
        return GetJsonAsync<MfaTicketDto>(
            "LinxFrameworkAutorizacao/ConfirmMfaEnrollment"
            + "?tableOrigin=" + OriginUx
            + "&idGpecon=" + idGpecon
            + "&uidUsuario=" + uidUsuario
            + "&code=" + Uri.EscapeDataString(code6 ?? ""));
    }

    public Task<MfaTicketDto> ValidateTotpAsync(Guid uidUsuario, int idGpecon, string code6)
    {
        return GetJsonAsync<MfaTicketDto>(
            "LinxFrameworkAutorizacao/ValidateMfaCode"
            + "?tableOrigin=" + OriginUx
            + "&idGpecon=" + idGpecon
            + "&uidUsuario=" + uidUsuario
            + "&code=" + Uri.EscapeDataString(code6 ?? "")
            + "&canal=" + Uri.EscapeDataString("Desktop"));
    }

    public Task<MfaTicketDto> IssueSkipTicketAsync(Guid uidUsuario, int idGpecon, string reason)
    {
        return GetJsonAsync<MfaTicketDto>(
            "LinxFrameworkAutorizacao/IssueMfaSkipTicket"
            + "?tableOrigin=" + OriginUx
            + "&idGpecon=" + idGpecon
            + "&uidUsuario=" + uidUsuario
            + "&reason=" + Uri.EscapeDataString(reason ?? "SKIP"));
    }

    public Task<MfaTicketDto> ValidateTicketAsync(string ticket)
    {
        return GetJsonAsync<MfaTicketDto>(
            "LinxFrameworkAutorizacao/ValidateMfaTicket?ticket=" + Uri.EscapeDataString(ticket ?? ""));
    }

    public Task<LoginInfoDto> AuthenticateUserAsync(AmbienteAcesso ambiente, string nomeAutenticacao)
    {
        return GetJsonAsync<LoginInfoDto>(
            "LinxFrameworkAutorizacao/AuthenticateUser"
            + "?authenticatedUser=" + Uri.EscapeDataString(nomeAutenticacao)
            + "&applicationId=" + ambiente.UidAplicacao
            + "&companyId=" + ambiente.UidEmpresa
            + "&accessGroupId=" + Guid.Empty
            + "&environmentId=" + ambiente.IdTcsAmbiente);
    }

    public async Task<MfaTicketDto> CompletarMfaAsync(
        AmbienteAcesso ambiente,
        Func<MfaEnrollDto, string> pedirCodigoComQr,
        Func<string> pedirCodigo)
    {
        MfaStatusDto status = await GetMfaStatusAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon)
            .ConfigureAwait(false);
        if (status.MfaLocked)
            throw new InvalidOperationException("MFA bloqueado por excesso de tentativas (15 min).");

        if (!status.RequiresMfa)
            return await IssueSkipTicketAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon, status.SkipReason)
                .ConfigureAwait(false);

        if (!status.Enrolled)
        {
            MfaEnrollDto enroll = await BeginEnrollAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon)
                .ConfigureAwait(false);
            if (!enroll.Success)
                throw new InvalidOperationException(enroll.Message ?? "Falha ao iniciar MFA.");
            string code = pedirCodigoComQr(enroll);
            return await ConfirmEnrollAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon, code)
                .ConfigureAwait(false);
        }

        return await ValidateTotpAsync(ambiente.UidUsuario, ambiente.IdLinxGpecon, pedirCodigo())
            .ConfigureAwait(false);
    }

    public async Task<LoginInfoDto> AbrirSessaoApplicationAsync(AmbienteAcesso ambiente, string nomeAutenticacao, string mfaTicket)
    {
        MfaTicketDto check = await ValidateTicketAsync(mfaTicket).ConfigureAwait(false);
        if (check == null || !check.Success)
            throw new InvalidOperationException(check != null ? check.Message : "Ticket MFA inválido.");

        return await AuthenticateUserAsync(ambiente, nomeAutenticacao).ConfigureAwait(false);
    }

    private async Task<string[]> GetEncryptedPartsAsync(string relativeUrl)
    {
        using (HttpResponseMessage resp = await _http.GetAsync(relativeUrl).ConfigureAwait(false))
        {
            string raw = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!resp.IsSuccessStatusCode)
                throw new InvalidOperationException((int)resp.StatusCode + " " + raw);
            raw = raw.Trim().Trim('"').Replace("\\\"", "");
            string decoded = HttpUtility.UrlDecode(raw);
            string plain = _crypto.Decrypt(decoded);
            if (string.IsNullOrEmpty(plain))
                throw new InvalidOperationException("Resposta de autenticação inválida.");
            return plain.Split(new[] { "||" }, StringSplitOptions.None);
        }
    }

    private string DecryptPart(string[] parts, int index)
    {
        if (parts == null || index >= parts.Length)
            return string.Empty;
        return _crypto.Decrypt(parts[index]);
    }

    private async Task<T> GetJsonAsync<T>(string relativeUrl)
    {
        using (HttpResponseMessage resp = await _http.GetAsync(relativeUrl).ConfigureAwait(false))
        {
            string json = await EnsureJsonAsync(resp).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

    private static async Task<string> EnsureJsonAsync(HttpResponseMessage resp)
    {
        string body = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
        if (!resp.IsSuccessStatusCode)
            throw new InvalidOperationException((int)resp.StatusCode + " " + Truncate(body, 400));
        return body;
    }

    private static string Truncate(string s, int n)
    {
        if (string.IsNullOrEmpty(s) || s.Length <= n) return s;
        return s.Substring(0, n) + "...";
    }
}
```

Escolha de ambiente (igual ao Portal):

```csharp
public static AmbienteAcesso EscolherAmbiente(IList<AmbienteAcesso> lista)
{
    if (lista == null || lista.Count == 0)
        throw new InvalidOperationException("Usuário sem ambientes.");
    AmbienteAcesso padrao = null;
    foreach (var a in lista)
        if (a.IndicaAcessoPadrao) { padrao = a; break; }
    if (padrao != null) return padrao;
    if (lista.Count == 1) return lista[0];
    throw new InvalidOperationException("Há vários ambientes. Peça ao usuário para escolher, ou marque INDICA_ACESSO_PADRAO.");
}
```

---

## 5. SSO Microsoft no desktop (MSAL public client)

Não encaminhe o access token ao Service. Extraia o login e chame `LoginAposSsoAsync`.

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

public static class LinxDesktopSso
{
    public static async Task<string> ObterNomeAutenticacaoAsync(
        string clientId, string tenantId, string redirectUri)
    {
        IPublicClientApplication app = PublicClientApplicationBuilder
            .Create(clientId)
            .WithAuthority("https://login.microsoftonline.com/" + tenantId)
            .WithRedirectUri(redirectUri)
            .Build();

        AuthenticationResult result = await app
            .AcquireTokenInteractive(new[] { "User.Read" })
            .WithPrompt(Prompt.ForceLogin)
            .ExecuteAsync()
            .ConfigureAwait(false);

        string upn = result.Account?.Username;
        if (string.IsNullOrWhiteSpace(upn))
            throw new InvalidOperationException("Azure não devolveu UPN.");

        int at = upn.IndexOf('@');
        return at > 0 ? upn.Substring(0, at) : upn;
    }
}
```

O `NomeAutenticacao` no cadastro Linx tem de ser igual a esse prefixo (o Service compara sem case).

---

## 6. Fluxo completo (console / WPF)

Na UI: mostre `QrCodePngBase64` (PNG) e peça 6 dígitos. Não logue `OtpauthUri` completo em produção.

```csharp
using (var client = new LinxUxAuthClient("http://localhost:1710/"))
{
    // A) Senha  — ou B) SSO:
    string login = await client.LoginComSenhaAsync("joao.silva", senhaDigitada);
    // string login = await client.LoginAposSsoAsync(
    //     await LinxDesktopSso.ObterNomeAutenticacaoAsync(clientId, tenantId, "http://localhost"));

    var ambientes = await client.ListarAmbientesAsync(login, acessoLocal: false);
    AmbienteAcesso ambiente = EscolherAmbiente(ambientes);

    MfaTicketDto mfa = await client.CompletarMfaAsync(
        ambiente,
        enroll =>
        {
            // WPF: Image.Source = BytesToBitmap(Convert.FromBase64String(enroll.QrCodePngBase64));
            // Pedir 6 dígitos na mesma janela.
            return PromptTotp("Escaneie o QR e informe o código de 6 dígitos");
        },
        () => PromptTotp("Código de 6 dígitos"));

    if (mfa == null || !mfa.Success)
        throw new InvalidOperationException(mfa != null ? mfa.Message : "MFA recusado.");

    LoginInfoDto sessao = await client.AbrirSessaoApplicationAsync(ambiente, login, mfa.Ticket);
    Guid token = sessao.Ambientes[0].Token;
    // Nas APIs de negócio, o Application/SPA manda headers CurrentUser, CurrentCompany,
    // Application, Environment, EconomicGroup, IdLinxGpecon — use os UIDs do ambiente + Token.
}
```

Ticket vale **10 minutos**. Se expirar, chame de novo `CompletarMfaAsync` (não reutilize ticket velho).

TOTP: 5 erros → bloqueio 15 minutos (`MfaLocked`).

---

## 7. Chamadas HTTP equivalentes (Postman / curl)

Substitua `{Service}` por `http://localhost:1710`.

**SSO (depois do MSAL no desktop):**

```
GET {Service}/LinxFrameworkAutorizacao/AuthenticatePortalSso?userName=joao.silva
```

**Ambientes:**

```
POST {Service}/LinxFrameworkUsuarioAutorizacao/PortalUserAccess
Content-Type: application/json

{ "NomeAutenticacao": "joao.silva", "AcessoLocal": false, "Parametros": "" }
```

**MFA:**

```
GET {Service}/LinxFrameworkAutorizacao/GetMfaStatus?tableOrigin=UX&idGpecon=123&uidUsuario={guid}
GET {Service}/LinxFrameworkAutorizacao/BeginMfaEnrollment?tableOrigin=UX&idGpecon=123&uidUsuario={guid}
GET {Service}/LinxFrameworkAutorizacao/ConfirmMfaEnrollment?tableOrigin=UX&idGpecon=123&uidUsuario={guid}&code=123456
GET {Service}/LinxFrameworkAutorizacao/ValidateMfaCode?tableOrigin=UX&idGpecon=123&uidUsuario={guid}&code=123456&canal=Desktop
GET {Service}/LinxFrameworkAutorizacao/ValidateMfaTicket?ticket={urlEncoded}
```

**Sessão Application** (`accessGroupId` = `Guid.Empty`, como o Portal):

```
GET {Service}/LinxFrameworkAutorizacao/AuthenticateUser?authenticatedUser=joao.silva&applicationId={uidApp}&companyId={uidEmpresa}&accessGroupId=00000000-0000-0000-0000-000000000000&environmentId=1
```

**Senha:** monte `authenticateParameters` com `Cryptography.Encrypt` no desktop; não dá para colar a senha em claro.

---

## 8. Erros comuns

| Sintoma | Causa |
|---------|--------|
| “sem cadastro local” no SSO | `NomeAutenticacao` ≠ prefixo do UPN |
| `AuthenticatePortalSso` OK mas MFA pede código | esperado: SSO não dispensa TOTP |
| Lista de ambientes vazia | `AcessoLocal` errado, ou usuário sem `TCS_USUARIO_ACESSO` |
| Ticket expirado | passaram 10 min; repetir TOTP |
| 500 no `AuthenticateUser` | UIDs de outro ambiente / usuário |
| MSAL AADSTS700016 / redirect | app Entra sem plataforma desktop ou URI errada |

Não chame `AuthenticatePortalSso` sem ter obtido o UPN no MSAL neste processo.
