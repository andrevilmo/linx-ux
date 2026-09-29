# Pacotes License Server (`origin/ux-license-server`)

English: `README.md`.

Duas pastas de overlay da branch **`ux-license-server`** @ `f0ead48a`. Não alteram Portal nem Application.

| Pasta | Uso |
|-------|-----|
| `1-apply-iis\` | Aplicar sobre uma instalação **IIS** do site Service (`chaves do Web.config` + `bin\Linx.Framework.BV.dll`) |
| `2-overwrite-main\` | Sobrescrever **código-fonte** em `Main\` (mesmos caminhos) para compilar |

Scripts compartilhados: `Merge-AppSettings.ps1` (mescla chaves `LicenseServer.*`, **não** substitui o `Web.config`), `Merge-Csproj.ps1` (insere 9 itens `<Compile>`).

Inventários:

| Arquivo | Conteúdo |
|---------|----------|
| `INVENTORY-IIS.md` / `INVENTARIO-IIS.md` | O que copiar no IIS (binário + chaves) |
| `INVENTORY-SOURCE.md` / `INVENTARIO-FONTE.md` | O que sobrescrever em `Main\` |
| `1-apply-iis\FILE-LIST.txt` | Tamanho + SHA256 do pacote 1 |
| `2-overwrite-main\FILE-LIST.txt` | Tamanho + SHA256 do pacote 2 |
| `FILE-LIST.txt` | Tamanho + SHA256 do kit inteiro |
| `VERSIONS.txt` / `VERSOES.txt` | Branch / chaves HML / o que não vai no pacote |

---

## 1. Aplicar sobre uma instalação atual (IIS)

Precisa só do **Service**. Raiz típica:

```text
C:\Linx Program Files\Linx Framework 6.0.0\Service\Web.config
```

1. Sobrescreva o código com o pacote 2 (ou copie `2-overwrite-main\Main\` em cima de um clone) e compile `Linx.Framework.BV.csproj`.
2. Copie `bin\Linx.Framework.BV.dll` para `1-apply-iis\Service\bin\`.
3. Neste pacote:

```powershell
pwsh -File packages\LICENSE-SERVER\1-apply-iis\Copy-ToIis.ps1 `
    -FrameworkRoot 'C:\Linx Program Files\Linx Framework 6.0.0'
```

Isso copia a DLL (se existir) e **mescla** as chaves HML no `Web.config` do Service (`LicenseServer.Enabled=true`, BaseUrl `api-hml.linx.com.br/app-licensing/`, …). Recicle o Application Pool do Service.

`LocalServiceBus` continua pulando o gate. Fail-closed: chave faltando ou erro HTTP → `LicenseException` no login.

**Não** copie `Portal.dll` nem DLLs da Application. O login já chama `LicenseControl.Validate` dentro do BV.

Reaproveite do `\bin` do Service (já está lá): `RestSharp.dll`, `Newtonsoft.Json.dll`, `Linx.Tools.dll`.

---

## 2. Sobrescrever Main (compilar)

Na raiz do repositório que contém `Main\`:

```powershell
pwsh -File packages\LICENSE-SERVER\2-overwrite-main\Copy-ToMain.ps1 -RepoRoot (Get-Location)
```

Copia `LicenseServer\*.cs`, `Utils\LicenseControl.cs` e `Linx.License.Server.Access.Tests\`. Mescla o **fragmento do csproj** (9 linhas `<Compile>`) se ainda não existirem. Use `-ReplaceCsproj` só se a árvore for `master` / `ux-license-server` (substituir o csproj inteiro apagaria itens posteriores de MFA/SSO).

Também dá para copiar à mão: cada arquivo em `2-overwrite-main\Main\` usa o mesmo caminho do `Main\` do repositório. Arquivos auxiliares (`*.fragment`, `*.FROM-ux-license-server`, `*.keys.xml`) ficam no pacote; não copie esses nomes para a árvore viva.

Depois:

```text
MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release
```

Copie `...\Linx.Framework.BV\bin\Release\Linx.Framework.BV.dll` para o IIS `Service\bin` (ou para `1-apply-iis\Service\bin\` e rode de novo o pacote 1).

Testes: `Main\Business\Linx.License.Server.Access.Tests\run-mono-tests.sh`.

---

## 3. O que o login usa

`LicenseControl.Validate` em `authenticateUser` / `UpdateToken` → Omni `POST api/v1/Authentication` e depois `POST api/v1/Licensing/billing/Validate`. Libera só se `licenca.lxStatusChave == 1`. Logout → `Revoke` (falha no revoke **não** falha o logout).
