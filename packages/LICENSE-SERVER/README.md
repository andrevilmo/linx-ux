# License Server packs (`origin/ux-license-server`)

Português: `LEIA-ME.md`.

Two drop folders from branch **`ux-license-server`** @ `f0ead48a`. They do not change Portal or Application.

| Folder | Use |
|--------|-----|
| `1-apply-iis\` | Overlay a **running IIS** Service site (`Web.config` keys + `bin\Linx.Framework.BV.dll`) |
| `2-overwrite-main\` | Overlay **source** under `Main\` (same paths) so you can compile |

Shared: `Merge-AppSettings.ps1` (merges `LicenseServer.*` keys, does not replace `Web.config`), `Merge-Csproj.ps1` (inserts 9 `<Compile>` items).

Inventories:

| File | Covers |
|------|--------|
| `INVENTORY-IIS.md` / `INVENTARIO-IIS.md` | What to drop on IIS (binary + keys) |
| `INVENTORY-SOURCE.md` / `INVENTARIO-FONTE.md` | What to overwrite under `Main\` |
| `1-apply-iis\FILE-LIST.txt` | Size + SHA256 of pack 1 |
| `2-overwrite-main\FILE-LIST.txt` | Size + SHA256 of pack 2 |
| `FILE-LIST.txt` | Size + SHA256 of the whole kit |
| `VERSIONS.txt` / `VERSOES.txt` | Branch / HML keys / what is not shipped |

---

## 1. Apply on a current installation (IIS)

Need only **Service**. Typical root:

```text
C:\Linx Program Files\Linx Framework 6.0.0\Service\Web.config
```

1. Overlay source with pack 2 (or copy `2-overwrite-main\Main\` over a clone) and compile `Linx.Framework.BV.csproj`.
2. Copy `bin\Linx.Framework.BV.dll` into `1-apply-iis\Service\bin\`.
3. From this pack:

```powershell
pwsh -File packages\LICENSE-SERVER\1-apply-iis\Copy-ToIis.ps1 `
    -FrameworkRoot 'C:\Linx Program Files\Linx Framework 6.0.0'
```

That copies the DLL (if present) and **merges** HML keys into Service `Web.config` (`LicenseServer.Enabled=true`, BaseUrl `api-hml.linx.com.br/app-licensing/`, …). Recycle the Service pool.

`LocalServiceBus` still skips the gate. Fail-closed: missing keys or HTTP error → `LicenseException` on login.

Do **not** copy `Portal.dll` / Application DLLs. Login already calls `LicenseControl.Validate` inside BV.

Reuse from the existing Service `\bin` (already there): `RestSharp.dll`, `Newtonsoft.Json.dll`, `Linx.Tools.dll`.

---

## 2. Overwrite Main (compile)

From the repo root that contains `Main\`:

```powershell
pwsh -File packages\LICENSE-SERVER\2-overwrite-main\Copy-ToMain.ps1 -RepoRoot (Get-Location)
```

Copies `LicenseServer\*.cs`, `Utils\LicenseControl.cs`, and `Linx.License.Server.Access.Tests\`. Merges the **csproj fragment** (9 `<Compile>` lines) unless they are already there. Use `-ReplaceCsproj` only if that tree is `master` / `ux-license-server` (a full csproj replace would drop later MFA/SSO Compile items).

You can also copy the folder by hand: every file under `2-overwrite-main\Main\` uses the same path as the repo `Main\`. Helper files (`*.fragment`, `*.FROM-ux-license-server`, `*.keys.xml`) stay in the pack; do not copy those names into the live tree.

Then:

```text
MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release
```

Copy `...\Linx.Framework.BV\bin\Release\Linx.Framework.BV.dll` to IIS `Service\bin` (or into `1-apply-iis\Service\bin\` and re-run pack 1).

Tests: `Main\Business\Linx.License.Server.Access.Tests\run-mono-tests.sh`.

---

## 3. What login uses

`LicenseControl.Validate` on `authenticateUser` / `UpdateToken` → Omni `POST api/v1/Authentication` then `POST api/v1/Licensing/billing/Validate`. Allow only `licenca.lxStatusChave == 1`. Logout → `Revoke` (revoke failure does not fail logout).
