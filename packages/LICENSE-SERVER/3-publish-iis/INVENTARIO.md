# Inventário — pacote IIS completo (`3-publish-iis`)

English: `INVENTORY.md`.
Guia: `INSTALAR-IIS.md`.

Branch: `origin/ux-license-server` @ f0ead48a.
Site em runtime: **somente Service** (portas 1710 / 8082, pool `SI-PDR-Service`).

## Publicar no IIS

| Caminho no pacote | Caminho no IIS | Ação |
|-------------------|----------------|------|
| `Service\bin\Linx.Framework.BV.dll` | `Service\bin\Linx.Framework.BV.dll` | substituir (compile o pacote 2) |
| `Service\Web.config.keys.xml` | mesclar em `Service\Web.config` | 13 chaves `LicenseServer.*` HML |

`Linx.Framework.BV.dll` **não** está no git. Coloque em `Service\bin\` deste pacote antes de `Publish-ToIis.ps1`.

## Já na instalação (não substituir)

| Arquivo | Motivo |
|---------|--------|
| `RestSharp.dll` | HTTP do cliente Omni |
| `Newtonsoft.Json.dll` | JSON |
| `Linx.Tools.dll` | `LocalServiceBus` |
| `Linx.Framework.BV.WebAPI.DS.dll` | controllers; carregam o BV |
| `Portal\*`, `Application\*` | esta branch não muda esses sites |
| `Web.config` inteiro | SQL / ServiceBus |

## Scripts

| Arquivo | Função |
|---------|--------|
| `Publish-ToIis.ps1` | backup + DLL + merge + recycle |
| `Test-Iis.ps1` | checa arquivos, chaves, pool, GET :1710, HTTPS HML |
| `INSTALAR-IIS.md` | guia completo de instalação e operação |
| `CHECKLIST.txt` | ordem de execução |
| `COPY-MAP.txt` / `MAPA-COPIA.txt` | mapa curto |

Lista de hashes: `FILE-LIST.txt`.
