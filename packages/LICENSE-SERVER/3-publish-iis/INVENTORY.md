# Inventory — full IIS publish pack (`3-publish-iis`)

Português: `INVENTARIO.md`.
Guide: `INSTALL-IIS.md`.

Branch: `origin/ux-license-server` @ f0ead48a.
Runtime site: **Service only** (ports 1710 / 8082, pool `SI-PDR-Service`).

## Publish to IIS

| Pack path | IIS path | Action |
|-----------|----------|--------|
| `Service\bin\Linx.Framework.BV.dll` | `Service\bin\Linx.Framework.BV.dll` | replace (compile pack 2) |
| `Service\Web.config.keys.xml` | merge into `Service\Web.config` | 13 HML `LicenseServer.*` keys |

`Linx.Framework.BV.dll` is **not** in git. Drop it in this pack’s `Service\bin\` before `Publish-ToIis.ps1`.

## Already on a normal install (do not replace)

| File | Why it stays |
|------|----------------|
| `RestSharp.dll` | Omni HTTP client |
| `Newtonsoft.Json.dll` | JSON |
| `Linx.Tools.dll` | `LocalServiceBus` |
| `Linx.Framework.BV.WebAPI.DS.dll` | controllers; they load BV |
| `Portal\*`, `Application\*` | this branch does not change those sites |
| whole `Web.config` | SQL / ServiceBus |

## Scripts

| File | Role |
|------|------|
| `Publish-ToIis.ps1` | backup + DLL + merge + recycle |
| `Test-Iis.ps1` | files, keys, pool, GET :1710, HTTPS HML |
| `INSTALL-IIS.md` | full install and run guide |
| `CHECKLIST.txt` | execution order |
| `COPY-MAP.txt` / `MAPA-COPIA.txt` | short map |

Hash list: `FILE-LIST.txt`.
