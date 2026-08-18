# Comparison: `original` vs `SI-PDR-CICD-AWS`

**Generated:** 2026-08-18 16:14 UTC  
**Repository:** `andrevilmo/linx-ux`  
**Merge-base:** `original` is a strict ancestor of `SI-PDR-CICD-AWS` (no unique commits on `original`).

| Item | `original` | `SI-PDR-CICD-AWS` |
|------|------------|-------------------|
| Tip SHA | `c5023c7c9da729bec16e9161fe1661790646de8d` | `647393003ac4f282695b224746f849179d60175b` |
| Tip date | 2026-07-23 | 2026-08-11 |
| Tip subject | adicionando tasks para vscode | fix(si-pdr): apply QA 3-12 Service connectionStrings for AWS IIS |
| Commits ahead of the other | 0 | **68** |
| Files changed (triple-dot) | — | **813** (585 added, 228 modified) |
| Diff size | — | **+344,406 / −6,304** lines |

This package explains every workstream that landed on `SI-PDR-CICD-AWS` after `original`, lists every affected file, maps those files to VS Code / Cursor tasks, and ships a copy of each changed file as it exists on `SI-PDR-CICD-AWS`.

## Downloadable packages

| Zip | Size | Contents |
|-----|------|----------|
| `original_vs_si-pdr-cicd-aws_README.zip` | ~205 KB | README, LEIA-ME, commit list, task map, full 813-file inventory, 121-file core list, unified patch |
| `original_vs_si-pdr-cicd-aws_core_source_files.zip` | ~1.5 MB | Same docs **plus** the 121 first-party files (source, configs, CI, VS Code) |
| `original_vs_si-pdr-cicd-aws_changed_files.zip` | ~85 MB | Same docs **plus all 813 changed files** as they exist on `SI-PDR-CICD-AWS` (including DLL/PDB/publish-output) |

Unzip the full package and open `original-vs-si-pdr-cicd-aws/README.md`. The files themselves are under `changed-files/` preserving repo paths.

## Package layout

| Path | Contents |
|------|----------|
| `README.md` | This document |
| `LEIA-ME.md` | Portuguese summary |
| `COMMITS.md` | All 68 commits |
| `TASKS.md` | VS Code / Cursor tasks and which files they use |
| `FILE_INVENTORY.md` | All 813 files with Added/Modified status, category, and theme |
| `CORE_SOURCE_FILES.md` | 121 first-party source/config files (no `bin/`, `obj/`, NuGet, node_modules, publish-output) |
| `core-source.patch` | Unified diff of those 121 files |
| `changed-files/` | Full tree of all 813 files at the `SI-PDR-CICD-AWS` tip |

How to use `changed-files/`: overlay onto a checkout of `original`, or copy subsets into an IIS install (`Application`, `Portal`, `Service`) following `TASKS.md`.

---

## What changed (work items / themes)

`SI-PDR-CICD-AWS` is `original` plus product work (footer, GpeCon, password/security, audit, Portal login, Azure AD SSO) **and** a full AWS IIS CI/CD stack.

### T1 — Application footer / layout

Commits: `533dde7e`, `cfa32ee5`, `e716d672`, `c73a3da4`.

Sticky/shared footer on Linx Internet Application (SPA shell + MVC layouts), plus LESS so the footer does not cover the side menu on zoom/resize.

**Files:** `_footer.html`, `_footer.js`, `shell.html`, `_Footer.cshtml`, `_FooterClean.cshtml`, `_Layout.cshtml`, `_LayoutClean.cshtml`, `linx-common.less`, `linx-theme-default.less`, matching `Main/Binary/Application/Views/...`.

### T2 — GpeCon code on export

Commits: `398f5903`, `6a6648a9`, merges `3c665ab4` / `7ec12310`.

Export/datatoolbar includes **código GpeCon**. Source touches include `App/widgets/datatoolbar/view.html` and user-authorization SPA/domain services; many follow-up files are compiled DLLs under `Main/Binary` and `bin/Release`.

### T3 — Password flow, lockout, email, unlock

Commits: `eab1cb71`, `0c03f834`, `e6e4d0cd`, `73ba4c4f`, `e6087027`, `328112f0`, `87557883`, `a802e617`, `d036b366`.

- Lock after **5** invalid attempts; unlock button on user admin
- Forgot/reset password via **email link** (expiry **5 minutes** on later commits)
- SMTP / `SendEmailSettings` (Linx SMTP, later Office365 on AWS QA configs)
- Remaining-attempts messages on login
- Unlock event when the user changes their own password
- After expired-password change, return to login (`modalChangePassword.js`)
- `LinxMail.cs` / `LinxErrorConstants.cs` on Tools

**DB scripts:** `TCS_LOG_ACESSO_AUTH.sql`, `INDICA_USUARIO_SERVICO.sql`, `Disable_Update_aspnet_Membership_Trigger.sql`.

### T4 — Auth audit (`LX_TCS.TCS_LOG_ACESSO_AUTH`)

Commits: `707d0560`, `87557883`, `d036b366`.

Writes auth access audit including `ID_LINX` / service-user flag (`INDICA_USUARIO_SERVICO`). Domain: `Autorizacao.AuthAccessAudit.Operations.cs`, `Autorizacao.AuthorizationServices.Operations.cs`.

### T5 — Portal login UI (forget-password, Bloqueado)

Commits: `d036b366` and password-flow commits.

`Login.cshtml` + `portal.css`: forget-password control at the bottom; **Bloqueado** filter on user grids (`CadastroUsuarioLocal` / `CadastroUsuarioAutenticacao`).

### T6 — Azure AD SSO (MSAL) for Portal

Commits: `58aa47a0`, `b0e913cf`, `35be3b44`, `ca8192bb`, `74a5488d`.

Confidential-client MSAL login for Portal (OmniPOS-style guide). New types under `Linx.Portal/Authentication/` (`MsalAuthenticationService`, `SsoLoginHelper`, token cache, Azure AD options). Docs: `docs/sso-azure-ad-msal-guide.md`, `docs/si-pdr-portal-sso.md`.

### T7 — AWS IIS CI/CD for Application / Service / Portal

Commits from `14da089e` (2026-08-05) through `64739300` (2026-08-11).

GitHub Actions → S3 → SSM on shared Windows EC2 → MSBuild publish → IIS:

| Site | Primary port | Alias |
|------|----------------|-------|
| Application | 8174 | 8080 |
| Portal | 8172 | 8081 |
| Service | 1710 | 8082 |

Workspace `C:\lx\si-pdr`, skip_build when only Binary/config changes, QA 3-12 connection strings, `ShellMode=PROD`, `LocalServiceBus` PROD so Portal user headers apply.

**New tree:** `.github/workflows/si-pdr-aws-iis.yml`, `infra/si-pdr-cicd/scripts/*.ps1`, `docs/si-pdr-aws-iis.md`.

### T8 — VS Code / Cursor tasks

On `original` the root tasks already covered Build / Publish / Stack / Deploy.

`SI-PDR-CICD-AWS` adds:

| Task label | Script |
|------------|--------|
| **Build Linx.Tools** | `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1` |
| **Update SI-PDR package (inventory + LEIA-ME + package)** | `.vscode/pack-si-pdr.ps1` |

Existing tasks were **not removed**. Scripts `stack-to-publish.ps1`, `stack-to-deploy.ps1`, `deploy-to-linx-framework.ps1`, `tasks.json`, and `TASKS.README.md` were updated (AWS notes, Linx.Tools, package task). See `TASKS.md`.

### T9 — QA AWS web.config / SQL / ShellMode / ServiceBus

Binary + source `Web.config` for Application, Portal, Service: QA 3-12 SQL (`tcp:10.16.0.4`), IIS Express ports 8172/8174/1710, Office365 email keys, membership lockout, `Set-SiPdrSqlConnectionStrings.ps1` optional overrides.

### T10 — Binary / publish / obj artifacts

167 `bin`/`obj`/DLL/PDB files (~335 MB) and 272 `publish-output` / `.vs` files (~86 MB). These are build outputs of T1–T6, not separate features. Prefer rebuilding from core source unless you are copying a hot DLL into IIS.

### T11 — Vendor / noise

NuGet `packages/`, `node_modules`, SelfHost Infragistics/Flexmonster images, PhoneGap BarcodeScanner Java, `Main/NLOG.log`. Treat as incidental unless you are reproducing a full working tree.

---

## File counts by category

| Category | Files | Notes |
| --- | --- | --- |
| core_source | 121 | First-party source, scripts, docs, configs (this is the review set) |
| binaries_obj | 167 | Compiled DLL/PDB/obj/cache |
| publish_output | 272 | MSBuild publish-output and Visual Studio .vs |
| vendor_packages | 137 | NuGet packages / node_modules |
| bundled_vendor_assets | 116 | SelfHost deps, barcode plugin, WebBundleResources |
| **Total** | 813 | 585 added, 228 modified |

## File counts by theme

| Theme | Files |
| --- | --- |
| T0 Other product source | 30 |
| T1 Application footer / layout | 13 |
| T10 Binary deploy artifacts | 404 |
| T11 Noise / generated | 233 |
| T2 GpeCon export filter | 2 |
| T3 Password flow / lock / email | 68 |
| T4 Auth audit (TCS_LOG_ACESSO_AUTH) | 3 |
| T5 Portal login / Bloqueado / forget-password | 5 |
| T6 Azure AD SSO (MSAL) | 10 |
| T7 AWS IIS CI/CD | 10 |
| T8 VS Code tasks / SI-PDR package | 18 |
| T9 QA AWS web.config / ports / SQL | 17 |

## Top-level paths

| Prefix | Files |
|--------|------:|
| `Main/` | 791 |
| `.vscode/` | 9 |
| `infra/` | 8 |
| `docs/` | 3 |
| `.github/` | 1 |
| `AGENTS.md` | 1 |

---

## Core source files (121) — status and theme

| Status | File | Theme |
| --- | --- | --- |
| A | .github/workflows/si-pdr-aws-iis.yml | T7 AWS IIS CI/CD |
| M | .vscode/TASKS.README.md | T8 VS Code tasks / SI-PDR package |
| M | .vscode/deploy-to-linx-framework.ps1 | T8 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr-INVENTORY.template.md | T8 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr-LEIA-ME.template.md | T8 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr.ps1 | T8 VS Code tasks / SI-PDR package |
| A | .vscode/settings.json | T8 VS Code tasks / SI-PDR package |
| M | .vscode/stack-to-deploy.ps1 | T8 VS Code tasks / SI-PDR package |
| M | .vscode/stack-to-publish.ps1 | T8 VS Code tasks / SI-PDR package |
| M | .vscode/tasks.json | T8 VS Code tasks / SI-PDR package |
| A | AGENTS.md | T8 VS Code tasks / SI-PDR package |
| M | Main/Application/Linx.Internet.Application/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/Application/Linx.Internet.Application/Linx.Framework.BV.SPA/App_Start/ModuleConfig.cs | T2 GpeCon export filter |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shared/modalChangePassword.js | T3 Password flow / lock / email |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shell/_footer.js | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell.html | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell/_footer.html | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/widgets/datatoolbar/view.html | T2 GpeCon export filter |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App_Start/BundleConfig.cs | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Controllers/AppCacheController.cs | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Controllers/LIAController.cs | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/SelfHost/SelfHostBase.zip | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/Authentication.cshtml | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/Unauthorized.cshtml | T0 Other product source |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_Footer.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_FooterClean.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_Layout.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Views/Shared/_LayoutClean.cshtml | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-common.less | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-theme-default.less | T1 Application footer / layout |
| M | Main/Application/Linx.Portal/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/AuthenticatedUser.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/AuthenticationResultModel.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/AzureAdOptions.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/FileTokenCacheStore.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/IAuthenticationService.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/ITokenCacheStore.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/MsalAuthenticationService.cs | T6 Azure AD SSO (MSAL) |
| A | Main/Application/Linx.Portal/Linx.Portal/Authentication/SsoLoginHelper.cs | T6 Azure AD SSO (MSAL) |
| M | Main/Application/Linx.Portal/Linx.Portal/Controllers/AccountController.cs | T5 Portal login / Bloqueado / forget-password |
| M | Main/Application/Linx.Portal/Linx.Portal/Linx.Portal.csproj | T0 Other product source |
| M | Main/Application/Linx.Portal/Linx.Portal/Utils/Utils.cs | T0 Other product source |
| M | Main/Application/Linx.Portal/Linx.Portal/Views/Account/Login.cshtml | T5 Portal login / Bloqueado / forget-password |
| M | Main/Application/Linx.Portal/Linx.Portal/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Application/Linx.Portal/Linx.Portal/assets/css/portal.css | T5 Portal login / Bloqueado / forget-password |
| M | Main/Application/Linx.Portal/Linx.Portal/packages.config | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Autorizacao.bmd | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Migrations/Configuration.cs | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.cs | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.tt | T0 Other product source |
| A | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Scripts/INDICA_USUARIO_SERVICO.sql | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| A | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Scripts/TCS_LOG_ACESSO_AUTH.sql | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| M | Main/Binary/Application/Views/Shared/_Footer.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_FooterClean.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_Layout.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_LayoutClean.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Library/Business Model/Linx.Framework.Autorizacao.BM.dll.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Library/Business Model/Linx.Framework.ControleSistema.BM.dll.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Portal/Views/Account/Login.cshtml | T5 Portal login / Bloqueado / forget-password |
| M | Main/Binary/Portal/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Binary/Portal/assets/css/portal.css | T5 Portal login / Bloqueado / forget-password |
| A | Main/Binary/Service/SqlScripts/Disable_Update_aspnet_Membership_Trigger.sql | T10 Binary deploy artifacts |
| M | Main/Binary/Service/Web.config | T9 QA AWS web.config / ports / SQL |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/AutorizacaoDomainService.UserExtension.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/Linx.Framework.BV.AuthenticateUserExtension.csproj | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.Implementations/Linx.Framework.BV.Implementations.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.Reports/Linx.Framework.BV.Reports.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacao.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacaoAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkEmpresaAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkModulo.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkObjeto.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkParametro.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkPerfilAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioAutorizacaoAutoGen.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioFranquiaAutoGen.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUtilitariosAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Linx.Framework.BV.WebAPI.DS.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI/Linx.Framework.BV.WebAPI.csproj | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthAccessAudit.Operations.cs | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthorizationServices.Operations.cs | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Help For Accessing/README.txt | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Linx.Framework.BV.csproj | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Modulo.TcsVersao.Operations.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/TcsAutorizacao.AuthorizationServices.Operations.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.DomainService.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.UsuarioAutorizacaoDomainService.Operations.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.DomainService.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.TcsUsuarioAutenticacao.Events.cs | T3 Password flow / lock / email |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Web.config | T9 QA AWS web.config / ports / SQL |
| A | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxErrorConstants.cs | T3 Password flow / lock / email |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxMail.cs | T3 Password flow / lock / email |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Tools.Core/LinxErrorConstants.cs | T3 Password flow / lock / email |
| A | Main/NLOG.log | T11 Noise / generated |
| M | Main/User Interface/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1 | T8 VS Code tasks / SI-PDR package |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioAutenticacao_pt-br.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioLocal_pt-br.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioAutorizacaoContext.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioFranquiaContext.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioAutenticacao.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioLocal.js | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioAutenticacao.html | T3 Password flow / lock / email |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioLocal.html | T3 Password flow / lock / email |
| A | Main/User Interface/Linx.Framework.BV/docker-framework-bv/Dockerfile | T8 VS Code tasks / SI-PDR package |
| A | Main/User Interface/Linx.Framework.BV/docker-framework-bv/docker-compose.yml | T8 VS Code tasks / SI-PDR package |
| A | docs/si-pdr-aws-iis.md | T7 AWS IIS CI/CD |
| A | docs/si-pdr-portal-sso.md | T6 Azure AD SSO (MSAL) |
| A | docs/sso-azure-ad-msal-guide.md | T6 Azure AD SSO (MSAL) |
| A | infra/si-pdr-cicd/README.md | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Clear-BuildWorkspace.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Diagnose-SiPdrRuntime.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Ensure-BuildTools.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Ensure-IisSiPdr.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Invoke-SiPdrAwsPipeline.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Set-SiPdrSqlConnectionStrings.ps1 | T7 AWS IIS CI/CD |
| A | infra/si-pdr-cicd/scripts/Sync-SiPdrWorkspace.ps1 | T7 AWS IIS CI/CD |

`A` = added on `SI-PDR-CICD-AWS`, `M` = modified vs `original`.

---

## Related VS Code tasks (how to build / package / deploy these changes)

Run from Command Palette → **Tasks: Run Task**.

| Goal | Task | Touches |
|------|------|---------|
| Compile Tools (mail/errors used by password flow) | **Build Linx.Tools** | `Linx.Desktop.Tools`, `LinxMail.cs` |
| Compile SPA / BV | **Build Linx.Framework.BV** | User Interface BV SPA (user unlock, Bloqueado) |
| Compile Service API | **Build Linx.Framework.BV.WebAPI.DS** | WebAPI.DS controllers, auth domain |
| Compile Application | **Build Linx.Internet.Application** | Footer, modalChangePassword, layouts |
| Compile Portal (SSO + login) | **Build Linx.Portal** | MSAL auth, Login.cshtml |
| Everything above | **Build All** | Sequential BV → WebAPI.DS → Application → Portal |
| Stage IIS-shaped package | **Build Publish Package** | `.vscode/stack-to-publish.ps1` (AWS CI uses this) |
| Delta vs IIS | **Stack Changed to Deploy** | `.vscode/stack-to-deploy.ps1` |
| Push to local IIS Framework 6.0.0 | **Deploy to Linx Framework 6.0.0 (backup + bin + Views + AppLogin)** | `.vscode/deploy-to-linx-framework.ps1` |
| Portal views/DLLs only | **Deploy Portal (views + DLLs)** | Login.cshtml, portal.css, Linx.Portal.dll |
| Service DLLs only | **Deploy Service DLLs (updated only)** | BV / Tools / WebAPI.DS |
| Desktop security zip (Application+Service+Portal+DB) | **Update SI-PDR package (inventory + LEIA-ME + package)** | `.vscode/pack-si-pdr.ps1` (compares to branch `original` by default) |
| AWS deploy | GitHub workflow `si-pdr-aws-iis.yml` on push to `SI-PDR-CICD-AWS` | `infra/si-pdr-cicd/scripts/*` |

Password/security desktop package (narrower than this comparison): task **Update SI-PDR package** writes `Desktop/SI-PDR` + `LEIA-ME.md`. This comparison package is broader: it includes CI/CD, SSO, and every git-changed path.

---

## Suggested review order

1. Read `COMMITS.md` (chronological).
2. Review `CORE_SOURCE_FILES.md` / `core-source.patch` (ignore T10/T11 unless debugging a specific DLL).
3. For IIS: Binary `Web.config` files under `changed-files/Main/Binary/{Application,Portal,Service}/`.
4. For AWS: `.github/workflows/si-pdr-aws-iis.yml` + `infra/si-pdr-cicd/`.
5. For SSO: `Main/Application/Linx.Portal/Linx.Portal/Authentication/` + `docs/sso-azure-ad-msal-guide.md`.

## Reproduce this comparison

```bash
git fetch origin original SI-PDR-CICD-AWS
git log --oneline origin/original..origin/SI-PDR-CICD-AWS
git diff --stat origin/original...origin/SI-PDR-CICD-AWS
```
