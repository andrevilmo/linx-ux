# Inventory — IIS apply pack (`1-apply-iis`)

Português: `INVENTARIO-IIS.md`.

Branch: `origin/ux-license-server` @ f0ead48a.
Runtime site: **Service** only.

## Must deploy (binary + config)

| Pack path | IIS path | Role |
|-----------|----------|------|
| `Service\bin\Linx.Framework.BV.dll` | `Service\bin\Linx.Framework.BV.dll` | Omni app-licensing gate (`LicenseServer\*` + new `LicenseControl`) |
| `Service\Web.config.keys.xml` | merge into `Service\Web.config` `<appSettings>` | `LicenseServer.*` keys (Enabled, BaseUrl, Email, Password, Cnpj, Key, LicenseId, ProductId, …) |

`Linx.Framework.BV.dll` is **not** stored in git in this pack. Build it from `2-overwrite-main` and drop it in `Service\bin\` before `Copy-ToIis.ps1`.

## Already on a normal Service install (do not replace)

| File | Why it stays |
|------|----------------|
| `RestSharp.dll` | `LicenseServerApiClient` HTTP |
| `Newtonsoft.Json.dll` | JSON body/response |
| `Linx.Tools.dll` | `LocalServiceBus`, helpers |
| `Linx.Framework.BV.WebAPI.DS.dll` | controllers unchanged; they load BV |
| `Portal\bin\*.dll` | no Portal code on this branch |
| `Application\bin\*.dll` | no Application code on this branch |

Hash list of this pack: `1-apply-iis\FILE-LIST.txt`.

## Scripts in this folder

| File | Role |
|------|------|
| `Copy-ToIis.ps1` | copy DLL if present + merge keys |
| `COPY-MAP.txt` / `MAPA-COPIA.txt` | same map in short form |
| `README-DLL.txt` / `LEIA-ME-DLL.txt` | where the DLL comes from (`Service\bin\`, gitignored until you drop it) |

## Keys merged (HML values from the branch)

`LicenseServer.Enabled`, `BaseUrl`, `ApiV1Root`, `Environment`, `ProductId`, `LicenseId`, `Email`, `Password`, `Cnpj`, `Key`, `User`, `Terminal`, `Version`.

Do **not** copy the full `Main/Binary/Service/Web.config` from the branch onto IIS: it would overwrite SQL and ServiceBus.
