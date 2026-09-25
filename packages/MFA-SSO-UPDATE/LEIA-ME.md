# Pacote MFA + SSO — atualização de instalação existente

Caminhos **relativos à pasta onde está o `Web.config`** de cada site IIS
(`Portal\`, `Service\`, `Application\`).

Instalação típica:

```text
C:\Linx Program Files\Linx Framework 6.0.0\
  Portal\Web.config
  Service\Web.config
  Application\Web.config
```

Cole cada pasta do pacote **em cima** da pasta do site correspondente.
Não sobrescreva `Web.config`. Recicle os Application Pools no final.

Banco: execute os `.sql` no catálogo **Portal / FrameworkAutorizacao**
(o mesmo de `LX_TCS.TCS_USUARIO_AUTENTICACAO`). **Não** rode no catálogo da Application.

---

## 1. Banco (obrigatório)

No SSMS, conectado ao catálogo Portal, rode **só este**:

```text
DB\APPLY_SSO_MFA.sql
```

Idempotente. Cria / altera se faltar:

| Objeto | Uso |
|--------|-----|
| `TCS_USUARIO_AUTENTICACAO.INDICA_UTILIZA_SSO` | Botão Microsoft |
| `TCS_USUARIO_AUTENTICACAO.INDICA_UTILIZA_MFA` | MFA (`NULL`/`1` = ligado, `0` = pula TOTP) |
| `TCS_USUARIO_AUTENTICACAO.INDICA_USUARIO_SERVICO` | Usuário de serviço (API ok, Portal bloqueia) |
| `TCS_LOG_ACESSO_AUTH` | Auditoria login / MFA / SSO (`I` = passo SSO) |
| `TCS_GPECON_MFA` | Política MFA da empresa (sem linha = MFA ligado) |
| `TCS_USUARIO_MFA` | Secret TOTP |
| `TCS_USUARIO_MFA_DISPOSITIVO` | Dispositivo confiável |
| `TCS_USUARIO_SSO_VINCULO` | Última conta Azure (OID+UPN) / Revogar SSO |

Opcionais (mesmo schema, um objeto por arquivo — use só se não rodar o APPLY):

- `DB\TCS_LOG_ACESSO_AUTH.sql`
- `DB\TCS_MFA.sql`
- `DB\TCS_USUARIO_SSO_VINCULO.sql`
- `DB\INDICA_USUARIO_SERVICO.sql`

Dados de exemplo (edite o login antes): `DB\APPLY_SSO_MFA_OPTIONAL_DATA.sql`

---

## 2. DLLs e arquivos — de qual site

### Portal (`Portal\Web.config`)

| Arquivo no pacote | Colar em |
|-------------------|----------|
| `Portal\bin\Linx.Portal.dll` | `bin\Linx.Portal.dll` |
| `Portal\bin\Microsoft.Identity.Client.dll` | `bin\Microsoft.Identity.Client.dll` |
| `Portal\bin\Microsoft.IdentityModel.Abstractions.dll` | `bin\Microsoft.IdentityModel.Abstractions.dll` |
| `Portal\Views\Account\Login.cshtml` | `Views\Account\Login.cshtml` |
| `Portal\Views\Mfa\Challenge.cshtml` | `Views\Mfa\Challenge.cshtml` |

SSO (MSAL, CONTINUAR, `login_hint`, vínculo Azure, tela TOTP).

### Service (`Service\Web.config`)

| Arquivo no pacote | Colar em |
|-------------------|----------|
| `Service\bin\Linx.Framework.BV.dll` | `bin\Linx.Framework.BV.dll` |
| `Service\bin\Linx.Framework.BV.WebAPI.DS.dll` | `bin\Linx.Framework.BV.WebAPI.DS.dll` |

APIs MFA/SSO: `AuthenticatePortalSso`, `GetMfaStatus`, `LogPortalSsoProcess`,
`CheckPortalSsoVinculo`, `BindPortalSsoVinculo`, `RevokePortalSsoVinculo`, TOTP.

Não precisa copiar `Linx.Tools.dll` nem `Linx.Framework.Autorizacao.BM.dll`
só por MFA/SSO (schema vai pelo SQL).

### Application (`Application\Web.config`)

| Arquivo no pacote | Colar em |
|-------------------|----------|
| `Application\bin\Linx.Internet.Application.dll` | `bin\Linx.Internet.Application.dll` |
| `Application\App\views\CadastroUsuarioLocal.html` | `App\views\CadastroUsuarioLocal.html` |
| `Application\App\views\CadastroUsuarioAutenticacao.html` | `App\views\CadastroUsuarioAutenticacao.html` |
| `Application\App\viewmodels\CadastroUsuarioLocal.js` | `App\viewmodels\CadastroUsuarioLocal.js` |
| `Application\App\viewmodels\CadastroUsuarioAutenticacao.js` | `App\viewmodels\CadastroUsuarioAutenticacao.js` |
| `Application\App\resources\CadastroUsuarioLocal_pt-br.js` | `App\resources\CadastroUsuarioLocal_pt-br.js` |
| `Application\App\resources\CadastroUsuarioAutenticacao_pt-br.js` | `App\resources\CadastroUsuarioAutenticacao_pt-br.js` |
| `Application\App\widgets\datatoolbar\view.html` | `App\widgets\datatoolbar\view.html` |

`Linx.Internet.Application.dll` = gate `mfaTicket` no login da Application (~51.7 MB).  
Cadastros soltos = **Revoga MFA** e **Revoga SSO** (sempre visíveis no formulário, um ao lado do outro, e **Revoga SSO** sempre na toolbar desses cadastros).

`Linx.Framework.BV.SPA.dll` **não** é obrigatório se você colar os `App\` acima.

Datas/hashes: `VERSIONS.txt`. Cópia automática: `Copy-ToIis.ps1` (não toca `Web.config`).

---

## 3. Ordem

1. `DB\APPLY_SSO_MFA.sql` no catálogo Portal  
2. Copiar `Service\bin\*.dll`  
3. Copiar `Portal\bin\*.dll` + `Portal\Views\...`  
4. Copiar `Application\bin\Linx.Internet.Application.dll` + `Application\App\...`  
5. Reciclar pools Service, Portal, Application (ou tocar cada `Web.config`)

---

## 4. Não copiar

- `Web.config` de nenhum site (SSO client secret, SQL, SMTP ficam no ambiente)
- DLLs de Application além de `Linx.Internet.Application.dll`
- Pasta `DB\` para o IIS
