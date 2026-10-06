# Pacote de código — Main/ vs master

Atualização de **fonte** (não é overlay IIS de DLLs).  
Use `packages/INSTALL_MFA_SSO` quando o destino for só copiar binários no IIS.

| | |
|--|--|
| Branch origem | `feature/implementacao-completa-ss-mfa-5-10-2026` |
| Commit origem | `8be30b711cab1962a8889100842bce7fad4e2b70` |
| Base | `master` `b8832b9e093ce55804aecc0fbfda6d6071e48cda` |
| Gerado | 2026-10-06 18:24:38 UTC |
| Arquivos copiados | 137 |
| Deletados em relação ao master | 4 |
| Omitidos (build/vendor/binários) | 770 |

Estrutura: os caminhos em `Main\...` são os mesmos do repositório. Cole a pasta `Main\` deste pacote **em cima** de um checkout `master`.

## O que este pacote traz

Implementações em `Main/` da branch `feature/implementacao-completa-ss-mfa-5-10-2026` que não estão em `master`:

- MFA TOTP (Service, Portal `/Mfa/Challenge`, ticket no Application)
- SSO Azure / MSAL (identifier-first, vínculo OID/UPN, Revoga SSO)
- Cadastros UX: Utiliza MFA/SSO, Revoga MFA, Revoga SSO
- Lookup de Ambientes (usuário editado + IdLinx) em `Events.cs` + SPA
- Schema SQL `APPLY_SSO_MFA.sql` e scripts por objeto
- Pacotes NuGet MSAL (`Microsoft.Identity.Client` 4.54.1)

## O que não entra

Ver `OMITIDOS.txt`. Resumo: `obj/`, `node_modules`, `publish-output`, `.pdb`, DLLs compiladas do produto (exceto MSAL), imagens vendor SelfHost/Mobile.

Para publicar IIS **sem rebuild**, use `packages/INSTALL_MFA_SSO` e depois aplique o fonte do lookup (`UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP.Events.cs`) via rebuild ou patch Cecil.

## Como aplicar

1. Checkout `master` (ou árvore equivalente).
2. Copie `Main\` deste pacote sobre `Main\` do destino (substituir arquivos).
3. Apague os arquivos de `DELETED.txt` se ainda existirem.
4. **Não** sobrescreva `Web.config` de produção: mescle só as seções MFA/SSO (`azureAd`, flags). Ajuste SMTP, connection strings e secrets do ambiente.
5. No SSMS, catálogo **Portal / FrameworkAutorizacao**, rode `DB\APPLY_SSO_MFA.sql` (idempotente). Não rode no catálogo Application.
6. Restaure NuGet do Portal (MSAL já está em `Main\Application\Linx.Portal\packages\`).
7. Compile Portal, Service (`Linx.Framework.BV` + `Linx.Framework.BV.WebAPI.DS`) e Application / SPA.
8. Recicle os pools IIS.

## SQL (atalho)

Os scripts também estão em `DB\` na raiz do pacote:

- `DB\\APPLY_SSO_MFA.sql`
- `DB\\APPLY_SSO_MFA_OPTIONAL_DATA.sql`
- `DB\\INDICA_USUARIO_SERVICO.sql`
- `DB\\TCS_LOG_ACESSO_AUTH.sql`
- `DB\\TCS_MFA.sql`
- `DB\\TCS_USUARIO_SSO_VINCULO.sql`

## Arquivos por área

### Portal
- `M` `Main/Application/Linx.Portal/.vscode/msbuild-build.ps1` (1161 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/AuthenticatedUser.cs` (302 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/AuthenticationResultModel.cs` (359 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/AzureAdOptions.cs` (811 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/FileTokenCacheStore.cs` (2427 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/IAuthenticationService.cs` (841 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/ITokenCacheStore.cs` (199 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/MsalAuthenticationService.cs` (8884 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/PortalSsoAudit.cs` (1439 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Authentication/SsoLoginHelper.cs` (12891 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Controllers/AccountController.cs` (28681 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Controllers/HomeController.cs` (8080 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Controllers/MfaController.cs` (6522 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Linx.Portal.csproj` (13720 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Models/Models.cs` (4572 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Utils/PortalMfaClient.cs` (5460 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Utils/Utils.cs` (4749 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Views/Account/Login.cshtml` (20574 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Views/Home/Index.cshtml` (6602 bytes)
- `A` `Main/Application/Linx.Portal/Linx.Portal/Views/Mfa/Challenge.cshtml` (4855 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/Web.config` (9410 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/assets/css/portal.css` (29965 bytes)
- `M` `Main/Application/Linx.Portal/Linx.Portal/packages.config` (2369 bytes)
- `A` `Main/Application/Linx.Portal/packages/Microsoft.Identity.Client.4.54.1/Microsoft.Identity.Client.nuspec` (9257 bytes)
- `A` `Main/Application/Linx.Portal/packages/Microsoft.Identity.Client.4.54.1/lib/net461/Microsoft.Identity.Client.dll` (1644504 bytes)
- `A` `Main/Application/Linx.Portal/packages/Microsoft.Identity.Client.4.54.1/lib/net461/Microsoft.Identity.Client.xml` (1570753 bytes)
- `A` `Main/Application/Linx.Portal/packages/Microsoft.IdentityModel.Abstractions.6.22.0/Microsoft.IdentityModel.Abstractions.nuspec` (1243 bytes)
- `A` `Main/Application/Linx.Portal/packages/Microsoft.IdentityModel.Abstractions.6.22.0/lib/net461/Microsoft.IdentityModel.Abstractions.dll` (18832 bytes)
- `A` `Main/Application/Linx.Portal/packages/Microsoft.IdentityModel.Abstractions.6.22.0/lib/net461/Microsoft.IdentityModel.Abstractions.xml` (16405 bytes)

### Application
- `M` `Main/Application/Linx.Internet.Application/.vscode/msbuild-build.ps1` (1679 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Framework.BV.SPA/App_Start/ModuleConfig.cs` (78 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shared/modalChangePassword.js` (9535 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shell/_footer.js` (6287 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell.html` (665 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell/_footer.html` (599 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/widgets/datatoolbar/view.html` (14496 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/App_Start/BundleConfig.cs` (18843 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Controllers/AppCacheController.cs` (3205 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Controllers/LIAController.cs` (32469 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/LIA/ForgotPassword.cshtml` (3794 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/LIA/ResetPassword.cshtml` (5250 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/Authentication.cshtml` (243 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/Unauthorized.cshtml` (2756 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_Footer.cshtml` (1810 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_FooterClean.cshtml` (509 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_Layout.cshtml` (4199 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_LayoutClean.cshtml` (2082 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/Web.config` (22950 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/bin/Linx.Internet.Application.dll.config` (22825 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-common.less` (161126 bytes)
- `M` `Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-theme-default.less` (17688 bytes)

### Service
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/AutorizacaoDomainService.UserExtension.cs` (3689 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/Linx.Framework.BV.AuthenticateUserExtension.csproj` (3981 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.Implementations/Linx.Framework.BV.Implementations.csproj` (4109 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.Reports/Linx.Framework.BV.Reports.csproj` (9387 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/.vscode/msbuild-build.ps1` (1191 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacao.cs` (27063 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacaoAutoGen.cs` (79114 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkEmpresaAutoGen.cs` (115231 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkModulo.cs` (28136 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkObjeto.cs` (8307 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkParametro.cs` (7351 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkPerfilAutoGen.cs` (153433 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioAutorizacao.cs` (10868 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioAutorizacaoAutoGen.cs` (135014 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioFranquiaAutoGen.cs` (58685 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUtilitariosAutoGen.cs` (18139 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Linx.Framework.BV.WebAPI.DS.csproj` (18269 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI/Linx.Framework.BV.WebAPI.csproj` (8910 bytes)
- `A` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthAccessAudit.Operations.cs` (25940 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthorizationServices.Operations.cs` (58588 bytes)
- `A` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.Mfa.Operations.cs` (41987 bytes)
- `A` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.SsoVinculo.Operations.cs` (21343 bytes)
- `A` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Help For Accessing/README.txt` (68 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Linx.Framework.BV.csproj` (141674 bytes)
- `A` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Modulo.TcsVersao.Operations.cs` (3868 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/TcsAutorizacao.AuthorizationServices.Operations.cs` (18703 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.DomainService.cs` (823334 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.TcsUsuarioAutenticacaoAcessoP.Events.cs` (9287 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.UsuarioAutorizacaoDomainService.Operations.cs` (6550 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.DomainService.cs` (289067 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.TcsUsuarioAutenticacao.Events.cs` (2487 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.ead` (556256 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/Web.config` (987 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/bin/Debug/Linx.Framework.Autorizacao.BM.dll.config` (1243 bytes)
- `M` `Main/Business/Linx.Framework.BV/Linx.Framework.BV/bin/Release/Linx.Framework.Autorizacao.BM.dll.config` (1243 bytes)

### SPA (User Interface)
- `M` `Main/User Interface/.vscode/msbuild-build.ps1` (1038 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1` (1610 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioAutenticacao_pt-br.js` (9464 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioLocal_pt-br.js` (7660 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuario_pt-br.js` (9156 bytes)
- `A` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/LookUpTcsAmbiente2.test.js` (4597 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioAutorizacaoContext.js` (468891 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioFranquiaContext.js` (292964 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuario.js` (137479 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioAutenticacao.js` (157556 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioLocal.js` (150441 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuario.html` (127808 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioAutenticacao.html` (159935 bytes)
- `M` `Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioLocal.html` (153270 bytes)
- `A` `Main/User Interface/Linx.Framework.BV/docker-framework-bv/Dockerfile` (170 bytes)
- `A` `Main/User Interface/Linx.Framework.BV/docker-framework-bv/docker-compose.yml` (1526 bytes)

### BM / SQL
- `M` `Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Autorizacao.bmd` (63073 bytes)
- `M` `Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Migrations/Configuration.cs` (37376 bytes)
- `M` `Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.cs` (151746 bytes)
- `M` `Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.tt` (36046 bytes)
- `A` `Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Scripts/APPLY_SSO_MFA.md` (1703 bytes)

### Binary (views/config publicados)
- `A` `Main/Binary/Application/Views/LIA/ForgotPassword.cshtml` (3794 bytes)
- `A` `Main/Binary/Application/Views/LIA/ResetPassword.cshtml` (5250 bytes)
- `M` `Main/Binary/Application/Views/Shared/Authentication.cshtml` (332 bytes)
- `M` `Main/Binary/Application/Views/Shared/Unauthorized.cshtml` (2860 bytes)
- `M` `Main/Binary/Application/Views/Shared/_Footer.cshtml` (1810 bytes)
- `M` `Main/Binary/Application/Views/Shared/_FooterClean.cshtml` (509 bytes)
- `M` `Main/Binary/Application/Views/Shared/_Layout.cshtml` (4199 bytes)
- `M` `Main/Binary/Application/Views/Shared/_LayoutClean.cshtml` (2082 bytes)
- `M` `Main/Binary/Application/Web.config` (23176 bytes)
- `M` `Main/Binary/Library/Business Model/Linx.Framework.Autorizacao.BM.dll.config` (1256 bytes)
- `M` `Main/Binary/Library/Business Model/Linx.Framework.ControleSistema.BM.dll.config` (1262 bytes)
- `A` `Main/Binary/Library/Common/Microsoft/Identity/Microsoft.Identity.Client.dll` (1644504 bytes)
- `A` `Main/Binary/Library/Common/Microsoft/Identity/Microsoft.IdentityModel.Abstractions.dll` (18832 bytes)
- `M` `Main/Binary/Portal/Views/Account/Login.cshtml` (20574 bytes)
- `M` `Main/Binary/Portal/Web.config` (9414 bytes)
- `M` `Main/Binary/Portal/assets/css/portal.css` (29896 bytes)
- `A` `Main/Binary/Portal/bin/Microsoft.Identity.Client.dll` (1644504 bytes)
- `A` `Main/Binary/Portal/bin/Microsoft.IdentityModel.Abstractions.dll` (18832 bytes)
- `A` `Main/Binary/Service/SqlScripts/Disable_Update_aspnet_Membership_Trigger.sql` (841 bytes)
- `M` `Main/Binary/Service/Web.config` (68639 bytes)

### Common
- `A` `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1` (1364 bytes)
- `M` `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxErrorConstants.cs` (9399 bytes)
- `M` `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxMail.cs` (3647 bytes)
- `M` `Main/Common/Linx.Tools.Library/Desktop/Linx.Tools.Core/LinxErrorConstants.cs` (9399 bytes)

### Outros
_nenhum_

## Arquivos deletados vs master

Ver `DELETED.txt`.

Inventário com SHA256: `FILE-LIST.txt`.
