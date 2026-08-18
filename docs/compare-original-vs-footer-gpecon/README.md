# Comparison: `original` vs `footer-presente-colocando-filtro-codigo-gpecon-na-exportacao`

**Generated:** 2026-08-18 19:07 UTC  
**Repository:** `andrevilmo/linx-ux`

These two branches **diverged**. They do not share a linear ancestor/descendant relationship. Common ancestor is `master` (`b8832b9e`, *Carga inicial*).

| Item | `original` | `footer-presente-colocando-filtro-codigo-gpecon-na-exportacao` |
|------|------------|---------------------------------------------------------------|
| Tip SHA | `c5023c7c9da729bec16e9161fe1661790646de8d` | `a802e617634e8fc12e9d918cabe28e0d572d3ff3` |
| Tip date | 2026-07-23 | 2026-08-04 |
| Tip subject | adicionando tasks para vscode | adicionando evento de desbloqueio na troca de senha pelo proprio usuario |
| Commits not on the other branch | **1** | **17** |
| Two-dot file diff | — | **768** files (553 added, 215 modified) |
| Diff size | — | **+316,562 / −6,166** lines |

Net file list below is `git diff original footer-…` (tip vs tip). Copies under `changed-files/` are taken from the **footer** tip.

`original` is only the VS Code task move to repo root (plus matching binaries). Footer adds footer UI, GpeCon on more screens, password/lock/email/unlock, auth audit, Portal login tweaks, and the **Update SI-PDR package** task.

## Downloadable packages

| Zip | Contents |
|-----|----------|
| `original_vs_footer_gpecon_README.zip` | This README, LEIA-ME, commits, tasks, 768-file inventory, 86-file core list, unified patch |
| `original_vs_footer_gpecon_core_source_files.zip` | Docs **plus** the 86 first-party files |
| `original_vs_footer_gpecon_changed_files.zip` | Docs **plus all 768 changed files** as they exist on the footer branch |

Unzip the full package and open `original-vs-footer-gpecon/README.md`. Files are under `changed-files/` with repo paths.

## Package layout

| Path | Contents |
|------|----------|
| `README.md` | This document |
| `LEIA-ME.md` | Portuguese summary |
| `COMMITS.md` | Commits unique to each branch |
| `TASKS.md` | VS Code / Cursor tasks vs this diff |
| `FILE_INVENTORY.md` | All 768 files with Added/Modified, category, theme |
| `CORE_SOURCE_FILES.md` | 86 first-party source/config files |
| `core-source.patch` | Unified diff of those 86 files |
| `changed-files/` | Footer-tip copies of all 768 paths |

---

## What changed (work items / themes)

### T1 — Application footer / layout

Commits: `533dde7e`, `cfa32ee5`, `e716d672`, `c73a3da4`.

Sticky shared footer on Linx Internet Application (SPA shell + MVC layouts). LESS updates keep the footer from covering the side menu on zoom/resize.

**Core files:** `_footer.html`, `_footer.js`, `shell.html`, `_Footer.cshtml`, `_FooterClean.cshtml`, `_Layout.cshtml`, `_LayoutClean.cshtml`, `linx-common.less`, `linx-theme-default.less`, matching `Main/Binary/Application/Views/...`.

### T2 — GpeCon code on export / other screens

Commits: `3c665ab4` (merge of footer-presente + GpeCon feature), `6a6648a9` (*merge GpeCon em outras telas*).

The dedicated feature branch `feature/colocando-filtro-codigo-gpecon-na-exportacao` (`398f5903`) is **not** a direct ancestor of either tip; footer absorbed the work via those merges.

**Core files:** `UsuarioAutorizacao.DomainService.cs`, `UsuarioAutorizacao.UsuarioAutorizacaoDomainService.Operations.cs`, `UsuarioFranquia.TcsUsuarioAutenticacao.Events.cs`, `UsuarioFranquiaContext.js` (plus compiled DLLs under Binary / `bin`).

### T3 — Password flow, lockout, email, unlock

Commits: `eab1cb71`, `0c03f834`, `7ec12310`, `e6e4d0cd`, `73ba4c4f`, `e6087027`, `328112f0`, `87557883`, `a802e617`.

- Lock after **5** invalid attempts; unlock on user admin (datatoolbar **Desbloquear usuário**)
- Forgot/reset password via email link
- SMTP / `SendEmailSettings` (Linx SMTP)
- Remaining-attempts messages on login
- Unlock when the user changes their own password (including the latest commit)
- After expired-password change, return to login (`modalChangePassword.js`)
- `LinxMail.cs` / `LinxErrorConstants.cs`

**DB scripts:** `TCS_LOG_ACESSO_AUTH.sql`, `INDICA_USUARIO_SERVICO.sql`, `Disable_Update_aspnet_Membership_Trigger.sql`.

### T4 — Auth audit (`LX_TCS.TCS_LOG_ACESSO_AUTH`)

Commits: `707d0560`, `87557883`.

Writes auth access audit; service-user flag `INDICA_USUARIO_SERVICO`. Domain: `Autorizacao.AuthAccessAudit.Operations.cs`, `Autorizacao.AuthorizationServices.Operations.cs`.

### T5 — Portal login UI

`Login.cshtml`, `portal.css`, `AccountController.cs` — login copy/layout for lockout and password recovery.

### T6 — VS Code / Cursor tasks

`original` already has the root `.vscode` task set (29 labels). Footer **adds**:

| Task label | Script |
|------------|--------|
| **Build Linx.Tools** | `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1` |
| **Update SI-PDR package (inventory + LEIA-ME + package)** | `.vscode/pack-si-pdr.ps1` |

Also added: `pack-si-pdr-*.template.md`, `.vscode/settings.json`. Modified: `tasks.json`, `TASKS.README.md`, `deploy-to-linx-framework.ps1`.

`copy-to-out.ps1`, `stack-to-deploy.ps1`, and `stack-to-publish.ps1` are **identical** on both tips.

Commit unique to `original`: `c5023c7c` *adicionando tasks para vscode* (move tasks from `Main/.vscode` to repo root). Footer already has the root task files and extends them.

### T8 / T9 — Build outputs and vendor

163 DLL/PDB/obj files (~332 MB) and 272 `publish-output` / `.vs` files (~86 MB) are compiled results of T1–T5, not separate features. NuGet/node_modules, SelfHost images, and BarcodeScanner Java are incidental.

This comparison does **not** include AWS IIS CI/CD or Azure AD SSO (those landed later on `SI-PDR-CICD-AWS`).

---

## File counts by category

| Category | Files | Notes |
| --- | --- | --- |
| core_source | 86 | First-party source, scripts, docs, configs |
| binaries_obj | 163 | Compiled DLL/PDB/obj/cache |
| publish_output | 272 | MSBuild publish-output and Visual Studio .vs |
| vendor_packages | 131 | NuGet packages / node_modules |
| bundled_vendor_assets | 116 | SelfHost deps, barcode plugin, WebBundleResources |
| **Total** | 768 | 553 added, 215 modified |

## File counts by theme

| Theme | Files |
| --- | --- |
| T0 Other product source | 29 |
| T1 Application footer / layout | 13 |
| T3 Password flow / lock / email / unlock | 74 |
| T4 Auth audit (TCS_LOG_ACESSO_AUTH) | 3 |
| T5 Portal login UI | 4 |
| T6 VS Code tasks / SI-PDR package | 11 |
| T8 Binary / publish artifacts | 386 |
| T9 Noise / generated | 248 |

---

## Core source files (86) — status and theme

| Status | File | Theme |
| --- | --- | --- |
| M | .vscode/TASKS.README.md | T6 VS Code tasks / SI-PDR package |
| M | .vscode/deploy-to-linx-framework.ps1 | T6 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr-INVENTORY.template.md | T6 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr-LEIA-ME.template.md | T6 VS Code tasks / SI-PDR package |
| A | .vscode/pack-si-pdr.ps1 | T6 VS Code tasks / SI-PDR package |
| A | .vscode/settings.json | T6 VS Code tasks / SI-PDR package |
| M | .vscode/tasks.json | T6 VS Code tasks / SI-PDR package |
| M | Main/Application/Linx.Internet.Application/Linx.Framework.BV.SPA/App_Start/ModuleConfig.cs | T3 Password flow / lock / email / unlock |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shared/modalChangePassword.js | T3 Password flow / lock / email / unlock |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/viewmodels/shell/_footer.js | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell.html | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/views/shell/_footer.html | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/App/widgets/datatoolbar/view.html | T3 Password flow / lock / email / unlock |
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
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/Web.config | T3 Password flow / lock / email / unlock |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-common.less | T1 Application footer / layout |
| M | Main/Application/Linx.Internet.Application/Linx.Internet.Application/lib/linx/css/linx-theme-default.less | T1 Application footer / layout |
| M | Main/Application/Linx.Portal/Linx.Portal/Controllers/AccountController.cs | T5 Portal login UI |
| M | Main/Application/Linx.Portal/Linx.Portal/Views/Account/Login.cshtml | T5 Portal login UI |
| M | Main/Application/Linx.Portal/Linx.Portal/assets/css/portal.css | T5 Portal login UI |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Autorizacao.bmd | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Migrations/Configuration.cs | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.cs | T0 Other product source |
| M | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Model/BusinessDataModel.tt | T0 Other product source |
| A | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Scripts/INDICA_USUARIO_SERVICO.sql | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| A | Main/BM/Linx.Framework.Autorizacao.BM/Linx.Framework.Autorizacao.BM/Scripts/TCS_LOG_ACESSO_AUTH.sql | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| M | Main/Binary/Application/Views/Shared/Authentication.cshtml | T0 Other product source |
| M | Main/Binary/Application/Views/Shared/Unauthorized.cshtml | T0 Other product source |
| M | Main/Binary/Application/Views/Shared/_Footer.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_FooterClean.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_Layout.cshtml | T1 Application footer / layout |
| M | Main/Binary/Application/Views/Shared/_LayoutClean.cshtml | T1 Application footer / layout |
| M | Main/Binary/Portal/Views/Account/Login.cshtml | T5 Portal login UI |
| A | Main/Binary/Service/SqlScripts/Disable_Update_aspnet_Membership_Trigger.sql | T0 Other product source |
| M | Main/Binary/Service/Web.config | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/AutorizacaoDomainService.UserExtension.cs | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.AuthenticateUserExtension/Linx.Framework.BV.AuthenticateUserExtension.csproj | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.Implementations/Linx.Framework.BV.Implementations.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.Reports/Linx.Framework.BV.Reports.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacao.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkAutorizacaoAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkEmpresaAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkModulo.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkObjeto.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkParametro.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkPerfilAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioAutorizacaoAutoGen.cs | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUsuarioFranquiaAutoGen.cs | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Controllers/LinxFrameworkUtilitariosAutoGen.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/Linx.Framework.BV.WebAPI.DS.csproj | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI/Linx.Framework.BV.WebAPI.csproj | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthAccessAudit.Operations.cs | T4 Auth audit (TCS_LOG_ACESSO_AUTH) |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Autorizacao.AuthorizationServices.Operations.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Linx.Framework.BV.csproj | T0 Other product source |
| A | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Modulo.TcsVersao.Operations.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/TcsAutorizacao.AuthorizationServices.Operations.cs | T0 Other product source |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.DomainService.cs | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioAutorizacao.UsuarioAutorizacaoDomainService.Operations.cs | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.DomainService.cs | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/UsuarioFranquia.TcsUsuarioAutenticacao.Events.cs | T3 Password flow / lock / email / unlock |
| M | Main/Business/Linx.Framework.BV/Linx.Framework.BV/Web.config | T3 Password flow / lock / email / unlock |
| A | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1 | T6 VS Code tasks / SI-PDR package |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxErrorConstants.cs | T3 Password flow / lock / email / unlock |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/LinxMail.cs | T3 Password flow / lock / email / unlock |
| M | Main/Common/Linx.Tools.Library/Desktop/Linx.Tools.Core/LinxErrorConstants.cs | T3 Password flow / lock / email / unlock |
| A | Main/NLOG.log | T9 Noise / generated |
| M | Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1 | T6 VS Code tasks / SI-PDR package |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioAutenticacao_pt-br.js | T3 Password flow / lock / email / unlock |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/resources/CadastroUsuarioLocal_pt-br.js | T3 Password flow / lock / email / unlock |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioAutorizacaoContext.js | T3 Password flow / lock / email / unlock |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/services/UsuarioFranquiaContext.js | T3 Password flow / lock / email / unlock |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioAutenticacao.js | T3 Password flow / lock / email / unlock |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/viewmodels/CadastroUsuarioLocal.js | T3 Password flow / lock / email / unlock |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioAutenticacao.html | T3 Password flow / lock / email / unlock |
| M | Main/User Interface/Linx.Framework.BV/Linx.Framework.BV.SPA/App/views/CadastroUsuarioLocal.html | T3 Password flow / lock / email / unlock |
| A | Main/User Interface/Linx.Framework.BV/docker-framework-bv/Dockerfile | T6 VS Code tasks / SI-PDR package |
| A | Main/User Interface/Linx.Framework.BV/docker-framework-bv/docker-compose.yml | T6 VS Code tasks / SI-PDR package |

`A` = present on footer, absent on `original`. `M` = both have the path, content differs.

---

## Related VS Code tasks

Run from Command Palette → **Tasks: Run Task**.

| Goal | Task |
|------|------|
| Compile Tools (mail/errors) | **Build Linx.Tools** (footer only) |
| Compile SPA / user unlock + GpeCon UI | **Build Linx.Framework.BV** |
| Compile Service API / auth | **Build Linx.Framework.BV.WebAPI.DS** |
| Compile Application (footer, modal password) | **Build Linx.Internet.Application** |
| Compile Portal login | **Build Linx.Portal** |
| Everything above (except Tools) | **Build All** |
| Stage IIS-shaped package | **Build Publish Package** |
| Push to local IIS | **Deploy to Linx Framework 6.0.0 (backup + …)** |
| Portal views only | **Deploy Portal (views + DLLs)** |
| Service DLLs | **Deploy Service DLLs (updated only)** / **Deploy Linx.Tools.dll** |
| Desktop security zip vs `original` | **Update SI-PDR package** (footer only) |

See `TASKS.md`.

## Suggested review order

1. `COMMITS.md` (footer’s 17 commits, then original’s 1).
2. `CORE_SOURCE_FILES.md` / `core-source.patch`.
3. Ignore T8/T9 unless you need a specific DLL.
4. For another environment: task **Update SI-PDR package** on the footer branch.

## Reproduce this comparison

```bash
git fetch origin original footer-presente-colocando-filtro-codigo-gpecon-na-exportacao
git log --oneline origin/original..origin/footer-presente-colocando-filtro-codigo-gpecon-na-exportacao
git log --oneline origin/footer-presente-colocando-filtro-codigo-gpecon-na-exportacao..origin/original
git diff --stat origin/original origin/footer-presente-colocando-filtro-codigo-gpecon-na-exportacao
```
