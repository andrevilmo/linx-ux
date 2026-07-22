# VS Code / Cursor Tasks

How to run: **Command Palette** → `Tasks: Run Task` → pick a label.

Task definitions: [`.vscode/tasks.json`](tasks.json)

Source code lives under `Main\`. The deploy-to-IIS script resolves `Main` automatically. Several older build/publish task paths in `tasks.json` omit the `Main\` prefix — if a task fails with “path not found”, run the matching `.ps1` under `Main\...\.vscode\` or open/fix paths so they point at `Main`.

Default IIS install root:

`C:\Linx Program Files\Linx Framework 6.0.0`

Override with env var `LINX_IIS_ROOT` (or script parameters where listed).

---

## Quick map

| Goal | Task |
|------|------|
| Build everything (default build) | **Build All** |
| Build one project | **Build Linx.\*** |
| Publish to Podman volume / host expose | **Publish \*** / **Publish All** |
| Copy Podman outputs to `C:\Linx Workspace\out` | **Copy All to Out** |
| Stage only *changed* files for later deploy | **Stack Changed to Deploy** |
| Stage a full publish package | **Build Publish Package** |
| Deploy Portal to running IIS Portal | **Deploy Portal \*** |
| Deploy Service DLLs to running IIS Service | **Deploy Service \*** / **Deploy Linx.\*.dll** |
| Full update of running Application + Portal + Service | **Deploy to Linx Framework 6.0.0 (backup + bin + Views + AppLogin)** |

---

## Build

### Build Linx.Framework.BV
- **Objective:** MSBuild Release for `Linx.Framework.BV` (SPA / UI).
- **Script:** `Main/User Interface/Linx.Framework.BV/.vscode/msbuild-build.ps1`
- **Env:** `SKIP_PODMAN_SYNC=1` (no Podman volume sync after build).

### Build Linx.Framework.BV (with Podman sync)
- Same build, then syncs/exposes Podman volume (no `SKIP_PODMAN_SYNC`).

### Build Linx.Framework.BV.WebAPI.DS
- **Objective:** MSBuild Release for WebAPI.DS.
- **Script:** `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/.vscode/msbuild-build.ps1`
- **Env:** `SKIP_PODMAN_SYNC=1`

### Build Linx.Internet.Application
- **Objective:** MSBuild Release for Internet Application (LIA).
- **Script:** `Main/Application/Linx.Internet.Application/.vscode/msbuild-build.ps1`
- **Env:** `SKIP_PODMAN_SYNC=1`

### Build Linx.Portal
- **Objective:** MSBuild Release for Portal.
- **Script:** `Main/Application/Linx.Portal/.vscode/msbuild-build.ps1`
- **Env:** `SKIP_PODMAN_SYNC=1`

### Build All
- **Objective:** Sequential build of BV → WebAPI.DS → Internet.Application → Portal.
- **Default build task** (`Ctrl+Shift+B` / Run Build Task).
- Each child uses `SKIP_PODMAN_SYNC=1`.

**Shared build script env (when not skipped):**
| Variable | Effect |
|----------|--------|
| `SKIP_PODMAN_SYNC=1` | Skip post-build Podman sync |

---

## Publish (Podman volume + host expose)

These run each project’s `deploy-to-podman-volume.ps1` (often rebuild/publish into `podman-volume-output` / named volume).

### Publish Linx.Framework.BV  
### Publish Linx.Framework.BV.WebAPI.DS  
### Publish Linx.Internet.Application  
### Publish Linx.Portal  
### Publish All
- **Objective:** Publish each (or all, in sequence) to Podman host output.
- **Internet.Application note:** can run MSBuild publish unless `SKIP_MSBUILD_PUBLISH=1` (then uses existing `publish-output`).

| Variable | Effect |
|----------|--------|
| `SKIP_MSBUILD_PUBLISH=1` | Skip MSBuild publish; require existing `publish-output` (Internet.Application) |

---

## Staging / packaging (not live IIS)

### Copy All to Out
- **Objective:** Copy each project’s `podman-volume-output` into `C:\Linx Workspace\out\<ProjectName>`.
- **Script:** `.vscode/copy-to-out.ps1`
- **Parameters:** none.

### Stack Changed to Deploy
- **Objective:** Build workspace (optional), then copy **only changed** Portal/Service/Application DLLs and Views into `C:\Linx Workspace\out\toDeploy`, compared against a baseline / IIS install.
- **Script:** `.vscode/stack-to-deploy.ps1`
- **Does not** update live IIS; stages a delta package + `manifest.json`.

| Parameter | Default | Meaning |
|-----------|---------|---------|
| `-SkipBuild` | off | Do not rebuild before stacking |
| `-Force` | off | Treat all artifacts as changed |
| `-ResetBaseline` | off | Clear hash baseline under `C:\Linx Workspace\out\.stack-state` |
| `-BaselineRoot` | `LINX_IIS_ROOT` or `...\Linx Framework 6.0.0` | IIS reference for comparison |
| `-OutRoot` | `C:\Linx Workspace\out\toDeploy` | Output folder |

### Stack Changed to Deploy (no build)
- Same as above with `-SkipBuild`.

### Stack Changed to Deploy (reset baseline)
- Same with `-SkipBuild -ResetBaseline` (next stack sees everything as new relative to cleared baseline).

### Build Publish Package
- **Objective:** Build (optional), then copy deployable Portal/Service/Application artifacts into `C:\Linx Workspace\out\toPublish` (full package style, not change-delta).
- **Script:** `.vscode/stack-to-publish.ps1`
- **Env (task):** `SKIP_PODMAN_SYNC=1`

| Parameter | Default | Meaning |
|-----------|---------|---------|
| `-SkipBuild` | off | Skip workspace build |
| `-OutRoot` | `C:\Linx Workspace\out\toPublish` | Output folder |
| `-BaselineRoot` | `LINX_IIS_ROOT` or install path | IIS reference (Application Linx*.dll names) |

### Build Publish Package (no build)
- Same with `-SkipBuild`.

---

## Deploy Portal → running IIS Portal

Default target: `C:\Linx Program Files\Linx Framework 6.0.0\Portal`  
**Script:** `Main/Application/Linx.Portal/.vscode/deploy-portal-views.ps1`  
Also syncs `Main/Binary/Portal` unless skipped. Touches `Web.config` when something changed.

### Deploy Portal (views + DLLs)
- All Portal `*.cshtml` Views + DLLs: `Linx.Portal.dll`, `Linx.Tools.dll`, `Linx.Resources.Localization.dll` (updated only).

### Deploy Portal Views (all cshtml)
- Alias of the task above.

### Deploy Current Portal File
- Uses the **active editor file** (`${file}`). Maps to a view or DLL when possible.

### Deploy Current Portal View (cshtml)
- Alias of **Deploy Current Portal File**.

### Deploy Portal DLLs (updated only)
- DLLs only (`-DllsOnly`).

### Deploy Linx.Portal.dll
- Only `Linx.Portal.dll` (`-Dll Linx.Portal.dll -DllsOnly`).

| Parameter | Default | Meaning |
|-----------|---------|---------|
| `-File <path>` | | Deploy that view or inferred DLL |
| `-Dll <name>` | | Deploy one known DLL |
| `-TargetPath` | `PORTAL_DEPLOY_PATH` or install `\Portal` | Portal site root |
| `-Force` | off | Copy even if timestamps/size match |
| `-SkipBinarySync` | off | Do not update `Binary\Portal` |
| `-SkipDlls` | off | Views only |
| `-DllsOnly` | off | DLLs only |

| Env | Meaning |
|-----|---------|
| `PORTAL_DEPLOY_PATH` | Override Portal site root |

---

## Deploy Service → running IIS Service

Default target: `C:\Linx Program Files\Linx Framework 6.0.0\Service`  
**Script:** `Main/Business/Linx.Framework.BV/Linx.Framework.BV.WebAPI.DS/.vscode/deploy-service-dlls.ps1`  
Copies into `Service\bin` (and `Binary\Service\bin`). Touches Service `Web.config` when changed.

Known DLLs: `Linx.Tools.dll`, `Linx.Framework.BV.dll`, `Linx.Framework.BV.WebAPI.DS.dll`.

### Deploy Service DLLs (updated only)
- All three known DLLs (skip if already up to date).

### Deploy Current Service DLL
- Maps **active file** to one known DLL, or deploys all three if unmapped.

### Deploy Linx.Tools.dll  
### Deploy Linx.Framework.BV.dll
- Single-DLL variants.

| Parameter | Default | Meaning |
|-----------|---------|---------|
| `-Dll <name>` | | One of the known DLL names |
| `-File <path>` | | Infer DLL from edited file |
| `-TargetPath` | `SERVICE_DEPLOY_PATH` or install `\Service` | Service site root |
| `-Force` | off | Always copy |
| `-SkipBinarySync` | off | Do not update `Binary\Service` |

| Env | Meaning |
|-----|---------|
| `SERVICE_DEPLOY_PATH` | Override Service site root |

---

## Deploy to Linx Framework 6.0.0 (full live update)

Updates the **running** install: Application, Portal, and Service.

**Script:** `.vscode/deploy-to-linx-framework.ps1`  
Resolves workspace to `Main\` when present.

### Deploy to Linx Framework 6.0.0 (backup + bin + Views + AppLogin)
Full pipeline:

1. **Backup** entire install folder to  
   `C:\Linx Program Files\Linx Framework 6.0.0_BKP_{yyyyMMdd_HHmmss}.zip`
2. **bin**
   - Portal: `Linx.Portal.dll`, `Linx.Tools.dll`, `Linx.Resources.Localization.dll`
   - Service: `Linx.Tools.dll`, `Linx.Framework.BV.dll`, `Linx.Framework.BV.WebAPI.DS.dll`
   - Application: `Linx*.dll` that already exist under install `Application\bin`, when a newer build source exists
3. **Views**
   - Portal: all `*.cshtml`
   - Application: full `Views` tree from the LIA project
4. **AppLogin → Application\App**  
   Overlay files from  
   `Main\Application\Linx.Internet.Application\Linx.Internet.Application\AppLogin`  
   into install `Application\App` **only when needed** (missing, newer, or content differs). Does not delete extra App files.
5. Touch `Web.config` on sites that received changes (app domain recycle).
6. Write **`RELEASE_{yyyyMMdd_HHmmss}.txt`** next to the install folder (same parent as the backup zip), listing every **Binary**, **Views**, and **App** file applied, plus the **git branch** (and short commit) used for this deploy — use this list to update other environments.

Also syncs matching trees under `Main\Binary\...` unless `-SkipBinarySync`.

Example release path:

`C:\Linx Program Files\RELEASE_20260721_173045.txt`

Manifest sections:
- Header: date/time, `GitBranch`, `GitCommit`, workspace, target root, backup zip (if any)
- `--- Binary ---` — DLL/PDB paths relative to install root (e.g. `Service\bin\Linx.Framework.BV.dll`)
- `--- Views ---` — Portal/Application view paths
- `--- App ---` — files under `Application\App\...`
- `--- Detail ---` — each relative path with source file used
### Deploy to Linx Framework 6.0.0 (no backup)
- Same pipeline with `-SkipBackup` (faster; no zip).

| Parameter | Default | Meaning |
|-----------|---------|---------|
| `-TargetRoot` | `LINX_IIS_ROOT` or `C:\Linx Program Files\Linx Framework 6.0.0` | Install root (`Application`, `Portal`, `Service`) |
| `-SkipBackup` | off | Skip zip backup |
| `-Force` | off | Copy even if destination looks up to date |
| `-SkipBinarySync` | off | Do not update `Main\Binary\...` |
| `-SkipAppLogin` | off | Skip AppLogin → `Application\App` overlay |

| Env | Meaning |
|-----|---------|
| `LINX_IIS_ROOT` | Override install root |

**Manual example:**

```powershell
powershell.exe -NoProfile -ExecutionPolicy Bypass -File .\.vscode\deploy-to-linx-framework.ps1
powershell.exe -NoProfile -ExecutionPolicy Bypass -File .\.vscode\deploy-to-linx-framework.ps1 -SkipBackup -Force
```

**Notes:**
- Backup can take several minutes (install is large, ~1.5+ GB).
- Prefer building relevant projects (**Build All** or individual builds) before deploy so bin sources are current.
- Does not replace `Web.config` / site config files in the install (only timestamps them for recycle).

---

## Suggested workflows

1. **Daily UI/API fix on running machine**  
   Build affected project → **Deploy to Linx Framework 6.0.0 (no backup)**  
   or targeted **Deploy Portal** / **Deploy Service** tasks.

2. **Safer full refresh**  
   **Build All** → **Deploy to Linx Framework 6.0.0 (backup + bin + Views + AppLogin)**

3. **Package for another environment (no live IIS write)**  
   **Stack Changed to Deploy** or **Build Publish Package**, then copy `C:\Linx Workspace\out\toDeploy` / `toPublish` manually.

4. **Container / Podman pipeline**  
   **Publish All** → optional **Copy All to Out**
