# Inventário — pacote IIS (`1-apply-iis`)

English: `INVENTORY-IIS.md`.

Branch: `origin/ux-license-server` @ f0ead48a.
Site em runtime: **somente Service**.

## Obrigatório implantar (binário + config)

| Caminho no pacote | Caminho no IIS | Função |
|-------------------|----------------|--------|
| `Service\bin\Linx.Framework.BV.dll` | `Service\bin\Linx.Framework.BV.dll` | Gate Omni app-licensing (`LicenseServer\*` + novo `LicenseControl`) |
| `Service\Web.config.keys.xml` | mesclar em `Service\Web.config` `<appSettings>` | Chaves `LicenseServer.*` (Enabled, BaseUrl, Email, Password, Cnpj, Key, LicenseId, ProductId, …) |

`Linx.Framework.BV.dll` **não** está no git neste pacote. Compile pelo `2-overwrite-main` e coloque em `Service\bin\` antes de rodar `Copy-ToIis.ps1`.

## Já existe numa instalação normal do Service (não substitua)

| Arquivo | Por que fica |
|---------|----------------|
| `RestSharp.dll` | HTTP do `LicenseServerApiClient` |
| `Newtonsoft.Json.dll` | JSON do body/resposta |
| `Linx.Tools.dll` | `LocalServiceBus`, helpers |
| `Linx.Framework.BV.WebAPI.DS.dll` | controllers inalterados; eles carregam o BV |
| `Portal\bin\*.dll` | esta branch não muda Portal |
| `Application\bin\*.dll` | esta branch não muda Application |

Lista de hashes deste pacote: `1-apply-iis\FILE-LIST.txt`.

## Scripts nesta pasta

| Arquivo | Função |
|---------|--------|
| `Copy-ToIis.ps1` | copia a DLL se existir + mescla as chaves |
| `COPY-MAP.txt` / `MAPA-COPIA.txt` | o mesmo mapa, forma curta |
| `README-DLL.txt` / `LEIA-ME-DLL.txt` | de onde vem a DLL (`Service\bin\`, ignorada pelo git até você colocar o arquivo) |

## Chaves mescladas (valores HML da branch)

`LicenseServer.Enabled`, `BaseUrl`, `ApiV1Root`, `Environment`, `ProductId`, `LicenseId`, `Email`, `Password`, `Cnpj`, `Key`, `User`, `Terminal`, `Version`.

**Não** copie o `Main/Binary/Service/Web.config` inteiro da branch para o IIS: isso sobrescreveria SQL e ServiceBus.
