# POC desktop — login Linx UX (senha / SSO + MFA)

Console .NET 8 que executa o esqueleto de `docs/login-mfa-sso-desktop-dotnet.md`.

## Bibliotecas

| Pacote / peça | Versão | Para quê |
|---------------|--------|----------|
| `Newtonsoft.Json` | 13.0.3 | JSON do Service |
| `Microsoft.Identity.Client` | 4.83.0 | SSO Entra (MSAL public client) |
| `System.Net.Http` | BCL | `HttpClient` |
| `Linx.Security.Cryptography` | fonte do produto | Envelope de `AuthenticatePortal` (a mesma classe do Portal) |

Em produção Windows, em vez do `.cs` linkado, referencie `Linx.Tools.dll` (`Binary/Library/Common/Linx/Desktop/GAC/`).

Não use o **client secret** do Portal. Desktop = app Entra **Mobile and desktop**.

## Rodar

```bash
dotnet run --project samples/LinxUxAuthDesktopPoc -- --libs

dotnet run --project samples/LinxUxAuthDesktopPoc -- \
  --service http://localhost:1710/ \
  --user SEU_LOGIN \
  --password 'SUA_SENHA'
```

SSO (abre o browser da Microsoft):

```bash
dotnet run --project samples/LinxUxAuthDesktopPoc -- \
  --service http://localhost:1710/ \
  --sso --client-id <PUBLIC_CLIENT_ID> --tenant-id <TENANT_ID>
```

Senha e TOTP também podem vir de `LINX_USER`, `LINX_PASSWORD`, `LINX_TOTP`, `LINX_SERVICE_URL`.
