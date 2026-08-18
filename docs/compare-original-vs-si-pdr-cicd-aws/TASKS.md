# VS Code / Cursor tasks vs file changes

Definitions: `.vscode/tasks.json`  
Guide on `SI-PDR-CICD-AWS`: `.vscode/TASKS.README.md`

## Label diff (`original` → `SI-PDR-CICD-AWS`)

| Change | Label |
|--------|--------|
| Added | **Build Linx.Tools** |
| Added | **Update SI-PDR package (inventory + LEIA-ME + package)** |
| Unchanged (29 labels) | All other tasks from `original` |

`original` had 29 tasks; `SI-PDR-CICD-AWS` has 31.

## Task → script → why it matters for this diff

| Task | Script / command | Related themes / files |
|------|------------------|------------------------|
| Build Linx.Tools | `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1` | T3 `LinxMail.cs`, `LinxErrorConstants.cs` |
| Build Linx.Framework.BV | `Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1` | T3/T5 SPA CadastroUsuario* Bloqueado/unlock |
| Build Linx.Framework.BV (with Podman sync) | same without `SKIP_PODMAN_SYNC` | same |
| Build Linx.Framework.BV.WebAPI.DS | `Main/Business/.../WebAPI.DS/.vscode/msbuild-build.ps1` | T3/T4 auth domain + WebAPI controllers |
| Build Linx.Internet.Application | `Main/Application/Linx.Internet.Application/.vscode/msbuild-build.ps1` | T1 footer, T3 modalChangePassword |
| Build Linx.Portal | `Main/Application/Linx.Portal/.vscode/msbuild-build.ps1` | T5 login, T6 MSAL |
| Build All | depends on the four builds (plus Tools is separate) | full product compile |
| Publish Linx.* / Publish All | each project's `deploy-to-podman-volume.ps1` | container pipeline (unchanged labels) |
| Copy All to Out | `.vscode/copy-to-out.ps1` | staging |
| Stack Changed to Deploy | `.vscode/stack-to-deploy.ps1` (**modified**) | delta package vs IIS |
| Stack Changed to Deploy (no build) | same `-SkipBuild` | |
| Stack Changed to Deploy (reset baseline) | same `-SkipBuild -ResetBaseline` | |
| Build Publish Package | `.vscode/stack-to-publish.ps1` (**modified**; AWS CI calls this) | T7 |
| Build Publish Package (no build) | same `-SkipBuild` | T7 skip_build |
| Deploy Portal (views + DLLs) | `deploy-portal-views.ps1` | T5/T6 Login.cshtml, portal.css |
| Deploy Portal Views (all cshtml) | alias | |
| Deploy Current Portal File | active editor | |
| Deploy Current Portal View (cshtml) | alias | |
| Deploy Portal DLLs (updated only) | `-DllsOnly` | Linx.Portal.dll |
| Deploy Linx.Portal.dll | `-Dll Linx.Portal.dll` | T6 |
| Deploy to Linx Framework 6.0.0 (backup + …) | `.vscode/deploy-to-linx-framework.ps1` (**modified**) | full IIS refresh |
| Deploy to Linx Framework 6.0.0 (no backup) | `-SkipBackup` | AWS deploy uses this style |
| Deploy Service DLLs (updated only) | `deploy-service-dlls.ps1` | T3/T4 Service bin |
| Deploy Current Service DLL | active file | |
| Deploy Linx.Tools.dll | `-Dll Linx.Tools.dll` | T3 mail |
| Deploy Linx.Framework.BV.dll | `-Dll Linx.Framework.BV.dll` | T3/T4 |
| Update SI-PDR package | `.vscode/pack-si-pdr.ps1` (**new**) | Desktop security package vs `original` |

## Files that implement or document tasks

| Status | File |
|--------|------|
| M | `.vscode/tasks.json` |
| M | `.vscode/TASKS.README.md` |
| M | `.vscode/stack-to-deploy.ps1` |
| M | `.vscode/stack-to-publish.ps1` |
| M | `.vscode/deploy-to-linx-framework.ps1` |
| A | `.vscode/pack-si-pdr.ps1` |
| A | `.vscode/pack-si-pdr-LEIA-ME.template.md` |
| A | `.vscode/pack-si-pdr-INVENTORY.template.md` |
| A | `.vscode/settings.json` |
| A | `AGENTS.md` |
| M | several `**/msbuild-build.ps1` under Application / Portal / BV / Tools |

## AWS pipeline vs local tasks

The GitHub workflow does **not** replace local tasks. It remote-invokes the same publish/deploy scripts:

1. `Ensure-BuildTools.ps1` / `Ensure-IisSiPdr.ps1` (host bootstrap — no local task equivalent)
2. `stack-to-publish.ps1` ≡ **Build Publish Package**
3. `deploy-to-linx-framework.ps1 -SkipBackup -Force` ≡ **Deploy to Linx Framework 6.0.0 (no backup)** with force
4. `Set-SiPdrSqlConnectionStrings.ps1` — optional SQL/URL/ShellMode overrides (no local task)
5. `Diagnose-SiPdrRuntime.ps1` / `Sync-SiPdrWorkspace.ps1` / `Clear-BuildWorkspace.ps1` — CI helpers
