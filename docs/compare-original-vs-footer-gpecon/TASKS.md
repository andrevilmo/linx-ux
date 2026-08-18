# VS Code / Cursor tasks vs file changes

Definitions on footer: `.vscode/tasks.json`  
Guide: `.vscode/TASKS.README.md`

## Label diff (`original` → footer)

| Change | Label |
|--------|--------|
| Added | **Build Linx.Tools** |
| Added | **Update SI-PDR package (inventory + LEIA-ME + package)** |
| Unchanged (29 labels) | All other tasks from `original` |

`original` has 29 tasks; footer has 31.

## Task → script → why it matters

| Task | Script | Related themes |
|------|--------|----------------|
| Build Linx.Tools | `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1` | T3 `LinxMail.cs`, `LinxErrorConstants.cs` |
| Build Linx.Framework.BV | `Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1` | T2/T3 SPA CadastroUsuario*, GpeCon UI |
| Build Linx.Framework.BV (with Podman sync) | same without `SKIP_PODMAN_SYNC` | same |
| Build Linx.Framework.BV.WebAPI.DS | `Main/Business/.../WebAPI.DS/.vscode/msbuild-build.ps1` | T3/T4 auth domain |
| Build Linx.Internet.Application | `Main/Application/Linx.Internet.Application/.vscode/msbuild-build.ps1` | T1 footer, T3 modalChangePassword |
| Build Linx.Portal | `Main/Application/Linx.Portal/.vscode/msbuild-build.ps1` | T5 login |
| Build All | BV → WebAPI.DS → Application → Portal | does not include Tools |
| Publish Linx.* / Publish All | each `deploy-to-podman-volume.ps1` | container pipeline |
| Copy All to Out | `.vscode/copy-to-out.ps1` (identical on both tips) | staging |
| Stack Changed to Deploy | `.vscode/stack-to-deploy.ps1` (identical) | delta vs IIS |
| Build Publish Package | `.vscode/stack-to-publish.ps1` (identical) | full package |
| Deploy Portal (views + DLLs) | `deploy-portal-views.ps1` | T5 Login.cshtml |
| Deploy to Linx Framework 6.0.0 | `.vscode/deploy-to-linx-framework.ps1` (**modified** on footer) | full IIS refresh |
| Deploy Service DLLs | `deploy-service-dlls.ps1` | T3/T4 |
| Deploy Linx.Tools.dll | `-Dll Linx.Tools.dll` | T3 mail |
| Deploy Linx.Framework.BV.dll | `-Dll Linx.Framework.BV.dll` | T2/T3/T4 |
| Update SI-PDR package | `.vscode/pack-si-pdr.ps1` (**new** on footer) | Desktop security package; `-BaseBranch original` |

## Files that implement or document tasks

| Status | File |
|--------|------|
| M | `.vscode/tasks.json` |
| M | `.vscode/TASKS.README.md` |
| M | `.vscode/deploy-to-linx-framework.ps1` |
| A | `.vscode/pack-si-pdr.ps1` |
| A | `.vscode/pack-si-pdr-LEIA-ME.template.md` |
| A | `.vscode/pack-si-pdr-INVENTORY.template.md` |
| A | `.vscode/settings.json` |
| A | `Main/Common/Linx.Tools.Library/Desktop/Linx.Desktop.Tools/.vscode/msbuild-build.ps1` |
| M | `Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1` |

Unchanged between tips: `.vscode/copy-to-out.ps1`, `.vscode/stack-to-deploy.ps1`, `.vscode/stack-to-publish.ps1`.
