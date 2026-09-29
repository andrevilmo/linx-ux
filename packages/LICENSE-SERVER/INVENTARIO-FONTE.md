# Inventário — pacote de código (`2-overwrite-main`)

English: `INVENTORY-SOURCE.md`.

A mesma árvore de `Main\`. Overlay num clone e compile. Arquivos de `origin/ux-license-server`.

## Sobrescrever (obrigatório para compilar o gate)

| Caminho em `2-overwrite-main\` | Ação |
|-------------------------------|------|
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessDecision.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessGuard.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessResult.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseAccessSnapshot.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseException.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerApiClient.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerBootstrap.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerModels.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\LicenseServer\LicenseServerSettings.cs` | incluir |
| `Main\Business\Linx.Framework.BV\Linx.Framework.BV\Utils\LicenseControl.cs` | substituir |

Os pontos de chamada já no produto (`AuthenticateUser` / `UpdateToken` / logout `Remove`) **não mudam** nesta branch. Não overlay esses arquivos.

## csproj

| Arquivo | Ação |
|---------|------|
| `...\Linx.Framework.BV.csproj.LicenseServer.fragment` | **mesclar** 9 linhas `<Compile>` (`Copy-ToMain.ps1` / `Merge-Csproj.ps1`; seguro em árvores com MFA/SSO) |
| `...\Linx.Framework.BV.csproj.FROM-ux-license-server` | csproj inteiro da branch; só `-ReplaceCsproj` em árvores tipo master |

## Testes (opcional; compilar/rodar, não vai para o IIS)

| Caminho | Ação |
|---------|------|
| `Main\Business\Linx.License.Server.Access.Tests\LicenseAccessDecisionTests.cs` | incluir |
| `Main\Business\Linx.License.Server.Access.Tests\LicenseServerLiveTests.cs` | incluir |
| `Main\Business\Linx.License.Server.Access.Tests\Linx.License.Server.Access.Tests.csproj` | incluir |
| `Main\Business\Linx.License.Server.Access.Tests\Program.cs` | incluir |
| `Main\Business\Linx.License.Server.Access.Tests\run-mono-tests.sh` | incluir |

## Config (mesclar, não substituir)

| Caminho | Ação |
|---------|------|
| `Main\Binary\Service\Web.config.LicenseServer.keys.xml` | mesclar em `Main\Binary\Service\Web.config` |

## Fora deste pacote (de propósito)

- `Main\Binary\Service\Web.config` inteiro (SQL / ServiceBus seriam apagados)
- Fontes de Portal / Application / SPA
- Pacotes `RestSharp` / `Newtonsoft` (já referenciados em `Linx.Framework.BV.csproj`)

Lista de hashes deste pacote: `2-overwrite-main\FILE-LIST.txt`.

## Depois do overlay

```text
MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release
copy bin\Linx.Framework.BV.dll → IIS Service\bin
```
