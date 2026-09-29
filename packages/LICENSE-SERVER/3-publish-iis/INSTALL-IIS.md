# Publish License Server to IIS — full guide

Português: `INSTALAR-IIS.md`.

Pack **3** (`3-publish-iis\`). Target: an **existing** Linx Framework 6.0.0 IIS install. It does not install the Framework from scratch and it **does not** replace the whole `Web.config`.

Source branch: `origin/ux-license-server` @ `f0ead48a`.

---

## 1. What this pack does

On Portal/Application login the Service calls `LicenseControl.Validate`. After this pack, that call hits Omni **app-licensing** (HML by default):

1. `POST api/v1/Authentication` (email + password from `Web.config`)
2. `POST api/v1/Licensing/billing/Validate` (Bearer token)
3. Allow login **only** if `licenca.lxStatusChave == 1` (Active)
4. On logout: `POST api/v1/Licensing/Revoke` (revoke failure does **not** fail logout)

Fail-closed: missing keys, bad password, HTTP error, or `lxStatusChave` ≠ 1 → `LicenseException` and login is blocked.

`LocalServiceBus` enabled **skips** the gate (no License Server call).

Portal and Application **do not change**. **Service** site only.

---

## 2. Prerequisites

| Item | Typical value |
|------|----------------|
| Windows Server / Windows with IIS | IIS 8+ |
| ASP.NET 4.5 / 4.8 (integrated pipeline) | feature `IIS-ASPNET45` |
| .NET Framework 4.6.1+ in the pool | `managedRuntimeVersion = v4.0` |
| Linx Framework 6.0.0 already published | folder below |
| SQL / ServiceBus already in Service `Web.config` | **do not** replace that file |
| Outbound HTTPS 443 to HML | `api-hml.linx.com.br` |
| `Linx.Framework.BV.dll` built with the gate | pack 2 (`2-overwrite-main`) |

Typical IIS root:

```text
C:\Linx Program Files\Linx Framework 6.0.0\
  Service\Web.config
  Service\bin\Linx.Framework.BV.dll
  Service\bin\RestSharp.dll
  Service\bin\Newtonsoft.Json.dll
  Service\bin\Linx.Tools.dll
  Portal\
  Application\
```

### Sites and ports (SI-PDR / UX stack)

| IIS site | Primary port | CI alias | Application Pool | Folder |
|----------|--------------|----------|------------------|--------|
| **Service** | **1710** | 8082 | `SI-PDR-Service` | `...\Service` |
| Portal | 8172 | 8081 | `SI-PDR-Portal` | `...\Portal` |
| Application | 8174 | 8080 | `SI-PDR-Application` | `...\Application` |

The gate runs **inside** the Service w3wp. Recycle **SI-PDR-Service** after publish. Recycle Portal/Application only if an old session looks stuck.

---

## 3. Files this pack publishes

Inventory: `INVENTORY.md`.

| In the pack | On IIS | Action |
|-------------|--------|--------|
| `Service\bin\Linx.Framework.BV.dll` | `Service\bin\Linx.Framework.BV.dll` | **replace** (after compiling pack 2) |
| `Service\Web.config.keys.xml` | merge into `Service\Web.config` `<appSettings>` | **merge** 13 `LicenseServer.*` keys |

**Do not copy** (already on a normal install):

- `RestSharp.dll`, `Newtonsoft.Json.dll`, `Linx.Tools.dll`
- `Linx.Framework.BV.WebAPI.DS.dll`
- `Portal\bin\*`, `Application\bin\*`
- `Main\Binary\Library\*\Linx.LicenseServer.*.dll` (old module, unrelated)
- the whole `Web.config` (that would wipe SQL and ServiceBus)

The DLL is **not in git**. Build:

```text
MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release
```

after applying pack 2. Copy `bin\Release\Linx.Framework.BV.dll` into `3-publish-iis\Service\bin\` **before** `Publish-ToIis.ps1`.

---

## 4. Install (step by step)

Run PowerShell **as Administrator** on the IIS host (app-pool recycle needs it).

### 4.1 Backup

```powershell
$root = 'C:\Linx Program Files\Linx Framework 6.0.0'
$stamp = Get-Date -Format 'yyyyMMdd-HHmmss'
Copy-Item "$root\Service\Web.config" "$root\Service\Web.config.bak-$stamp"
Copy-Item "$root\Service\bin\Linx.Framework.BV.dll" "$root\Service\bin\Linx.Framework.BV.dll.bak-$stamp"
```

`Publish-ToIis.ps1` also writes timestamped backups in the same folders.

### 4.2 Drop the DLL into the pack

```text
3-publish-iis\Service\bin\Linx.Framework.BV.dll
```

### 4.3 Publish

From the kit root `packages\LICENSE-SERVER`:

```powershell
pwsh -File packages\LICENSE-SERVER\3-publish-iis\Publish-ToIis.ps1 `
    -FrameworkRoot 'C:\Linx Program Files\Linx Framework 6.0.0'
```

The script:

1. Checks `Service\Web.config` exists
2. Backs up current `Web.config` and BV.dll
3. Copies `Linx.Framework.BV.dll` if it is in the pack `Service\bin\`
4. Merges HML `LicenseServer.*` keys (does not wipe the rest of `Web.config`)
5. Recycles pool `SI-PDR-Service` if present; otherwise tries `SI-PDR-Application`

Dry run:

```powershell
pwsh -File packages\LICENSE-SERVER\3-publish-iis\Publish-ToIis.ps1 -WhatIf
```

### 4.4 Check the keys

In `Service\Web.config` `<appSettings>` you should see:

```xml
<add key="LicenseServer.Enabled" value="true" />
<add key="LicenseServer.BaseUrl" value="https://api-hml.linx.com.br/app-licensing/" />
<add key="LicenseServer.ApiV1Root" value="https://api-hml.linx.com.br/app-licensing/api/v1/" />
<add key="LicenseServer.Environment" value="0" />
<add key="LicenseServer.ProductId" value="LINX-POS" />
<add key="LicenseServer.LicenseId" value="4" />
<add key="LicenseServer.Email" value="linxpos@linx.com.br" />
<add key="LicenseServer.Password" value="Linx@022023" />
<add key="LicenseServer.Cnpj" value="45510647000100" />
<add key="LicenseServer.Key" value="MAQUINA-01" />
<add key="LicenseServer.User" value="" />
<add key="LicenseServer.Terminal" value="" />
<add key="LicenseServer.Version" value="1.0" />
```

For production, change Email, Password, Cnpj, Key, LicenseId, BaseUrl **before** letting users in.

Empty `LicenseServer.User` / `LicenseServer.Terminal`: terminal becomes the machine name; user comes from login.

### 4.5 Recycle by hand (if the script was not elevated)

```powershell
Import-Module WebAdministration
Restart-WebAppPool -Name 'SI-PDR-Service'
Start-WebAppPool -Name 'SI-PDR-Service'
```

Or IIS Manager → Application Pools → SI-PDR-Service → Recycle.

---

## 5. How to run / test

Checklist: `CHECKLIST.txt`.

```powershell
pwsh -File packages\LICENSE-SERVER\3-publish-iis\Test-Iis.ps1 `
    -FrameworkRoot 'C:\Linx Program Files\Linx Framework 6.0.0'
```

File/config checks:

- `Service\Web.config` has `LicenseServer.Enabled=true` and the HML BaseUrl
- `Service\bin\Linx.Framework.BV.dll` exists
- `RestSharp.dll`, `Newtonsoft.Json.dll`, `Linx.Tools.dll` still present
- pool `SI-PDR-Service` is Started
- `GET http://localhost:1710/` is reachable
- TCP 443 to `api-hml.linx.com.br`

### Login test (this is the real gate)

1. Open Portal: `http://localhost:8172/` (or the env URL).
2. Sign in with a user that is **not** on LocalServiceBus.
3. Service calls HML during `authenticateUser`.

| Outcome | Meaning |
|---------|---------|
| Login succeeds | `lxStatusChave == 1` (Active license) |
| Login refused with `LicenseException` / license message | fail-closed working (HML test license often returns `lxStatusChave=4` / discontinued) |
| Login succeeds **without** calling HML | `LicenseServer.Enabled=false` **or** `LocalServiceBus` on |

Outbound: the Service w3wp must reach `https://api-hml.linx.com.br/app-licensing/`. Corporate proxy: configure it for the pool identity.

---

## 6. Turn the gate off (without uninstalling)

In `Service\Web.config`:

```xml
<add key="LicenseServer.Enabled" value="false" />
```

Recycle `SI-PDR-Service`. `LicenseControl.Validate` becomes a no-op. Other keys can stay.

Full rollback: restore `Web.config.bak-*` and `Linx.Framework.BV.dll.bak-*`.

---

## 7. Common problems

| Symptom | What to do |
|---------|------------|
| Old login, no license message | stale DLL — publish again and recycle |
| `LicenseException` “Email e Password são obrigatórios” | keys not merged, or wrong `Web.config` |
| License Server HTTP 401 | wrong HML Email/Password |
| Timeout / connect failure | firewall, DNS, TLS, proxy; `Test-NetConnection api-hml.linx.com.br -Port 443` |
| Login succeeds with Enabled=true | LocalServiceBus skips the bootstrap |
| SQL / ServiceBus gone | whole `Web.config` was copied — restore backup |
| Portal 8172 up but gate never runs | Validate is in Service — check **Service** DLL and `Web.config` |
| Pool Stopped after Recycle | `Start-WebAppPool SI-PDR-Service` |
| Script warned DLL missing | compile pack 2 and copy the DLL into `3-publish-iis\Service\bin\` |

---

## 8. How the three packs fit together

| Pack | Use |
|------|-----|
| `1-apply-iis` | Minimal overlay (DLL + merge). No backup, recycle, or runbook |
| `2-overwrite-main` | Source under `Main\` so you can **compile** the DLL |
| **`3-publish-iis`** | **Publish and operate on IIS**: backup, merge, recycle, test, this guide |

Recommended QA order:

1. Pack 2 on a clone → MSBuild Release  
2. DLL into `3-publish-iis\Service\bin\`  
3. `Publish-ToIis.ps1`  
4. `Test-Iis.ps1`  
5. Portal login
