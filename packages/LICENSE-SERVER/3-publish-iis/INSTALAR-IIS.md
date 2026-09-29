# Publicar License Server no IIS — guia completo

English: `INSTALL-IIS.md`.

Pacote **3** (`3-publish-iis\`). Destino: uma instalação **já existente** do Linx Framework 6.0.0 no IIS. Não instala o Framework do zero e **não** substitui o `Web.config` inteiro.

Branch de origem: `origin/ux-license-server` @ `f0ead48a`.

---

## 1. O que este pacote faz

No login do Portal/Application o Service chama `LicenseControl.Validate`. Com este pacote, essa chamada passa a ir ao Omni **app-licensing** (HML por padrão):

1. `POST api/v1/Authentication` (email + senha do `Web.config`)
2. `POST api/v1/Licensing/billing/Validate` (Bearer token)
3. Libera o login **somente** se `licenca.lxStatusChave == 1` (Ativo)
4. No logout: `POST api/v1/Licensing/Revoke` (falha no revoke **não** derruba o logout)

Fail-closed: chave faltando, senha errada, HTTP de erro ou `lxStatusChave` ≠ 1 → `LicenseException` e o login não entra.

`LocalServiceBus` ligado **pula** o gate (não chama o License Server).

Portal e Application **não mudam**. Só o site **Service**.

---

## 2. Pré-requisitos

| Item | Valor típico |
|------|----------------|
| Windows Server / Windows com IIS | IIS 8+ |
| ASP.NET 4.5 / 4.8 (pipeline integrado) | feature `IIS-ASPNET45` |
| .NET Framework 4.6.1+ no pool | `managedRuntimeVersion = v4.0` |
| Linx Framework 6.0.0 já publicado | pasta abaixo |
| SQL / ServiceBus já no `Web.config` do Service | **não** mexer nisso |
| Saída HTTPS 443 para o HML | `api-hml.linx.com.br` |
| `Linx.Framework.BV.dll` compilado com o gate | pacote 2 (`2-overwrite-main`) |

Raiz IIS típica:

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

### Sites e portas (stack SI-PDR / UX)

| Site IIS | Porta principal | Alias CI | Application Pool | Pasta |
|----------|-----------------|----------|------------------|-------|
| **Service** | **1710** | 8082 | `SI-PDR-Service` | `...\Service` |
| Portal | 8172 | 8081 | `SI-PDR-Portal` | `...\Portal` |
| Application | 8174 | 8080 | `SI-PDR-Application` | `...\Application` |

O gate roda **dentro** do w3wp do Service. Recicle **SI-PDR-Service** depois de publicar. Reciclar Portal/Application é opcional (só se a sessão antiga ficar estranha).

---

## 3. Arquivos que este pacote publica

Inventário: `INVENTARIO.md`.

| No pacote | No IIS | Ação |
|-----------|--------|------|
| `Service\bin\Linx.Framework.BV.dll` | `Service\bin\Linx.Framework.BV.dll` | **substituir** (depois de compilar o pacote 2) |
| `Service\Web.config.keys.xml` | mesclar em `Service\Web.config` `<appSettings>` | **mesclar** 13 chaves `LicenseServer.*` |

**Não copie** (já estão na instalação):

- `RestSharp.dll`, `Newtonsoft.Json.dll`, `Linx.Tools.dll`
- `Linx.Framework.BV.WebAPI.DS.dll`
- `Portal\bin\*`, `Application\bin\*`
- `Main\Binary\Library\*\Linx.LicenseServer.*.dll` (módulo antigo, outra coisa)
- o `Web.config` inteiro (apagaria SQL e ServiceBus)

A DLL **não vem no git**. Compile:

```text
MSBuild Main\Business\Linx.Framework.BV\Linx.Framework.BV\Linx.Framework.BV.csproj /p:Configuration=Release
```

depois de aplicar o pacote 2. Copie `bin\Release\Linx.Framework.BV.dll` para `3-publish-iis\Service\bin\` **antes** de `Publish-ToIis.ps1`.

---

## 4. Instalação (passo a passo)

Rode o PowerShell **como Administrador** no servidor IIS (reciclar pool precisa disso).

### 4.1 Backup

```powershell
$root = 'C:\Linx Program Files\Linx Framework 6.0.0'
$stamp = Get-Date -Format 'yyyyMMdd-HHmmss'
Copy-Item "$root\Service\Web.config" "$root\Service\Web.config.bak-$stamp"
Copy-Item "$root\Service\bin\Linx.Framework.BV.dll" "$root\Service\bin\Linx.Framework.BV.dll.bak-$stamp"
```

`Publish-ToIis.ps1` também faz backup com timestamp na mesma pasta.

### 4.2 Colocar a DLL no pacote

```text
3-publish-iis\Service\bin\Linx.Framework.BV.dll
```

### 4.3 Publicar

Na pasta deste pacote (ou a partir da raiz do kit `packages\LICENSE-SERVER`):

```powershell
pwsh -File packages\LICENSE-SERVER\3-publish-iis\Publish-ToIis.ps1 `
    -FrameworkRoot 'C:\Linx Program Files\Linx Framework 6.0.0'
```

O script:

1. Confirma que existe `Service\Web.config`
2. Copia backup de `Web.config` e da DLL atual
3. Copia `Linx.Framework.BV.dll` se estiver em `Service\bin\` do pacote
4. Mescla as chaves HML `LicenseServer.*` (não apaga o resto do `Web.config`)
5. Recicla o pool `SI-PDR-Service` (se existir); senão tenta `SI-PDR-Application`

Somente conferir, sem gravar:

```powershell
pwsh -File packages\LICENSE-SERVER\3-publish-iis\Publish-ToIis.ps1 -WhatIf
```

### 4.4 Conferir as chaves

Abra `Service\Web.config` e confira em `<appSettings>`:

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

Em produção troque Email, Password, Cnpj, Key, LicenseId, BaseUrl (produção vs HML) **antes** de liberar usuários.

`LicenseServer.User` e `LicenseServer.Terminal` vazios: o terminal vira o nome da máquina; o usuário vem do login.

### 4.5 Reciclar na mão (se o script não rodou como admin)

```powershell
Import-Module WebAdministration
Restart-WebAppPool -Name 'SI-PDR-Service'
# se o pool estava Stopped:
Start-WebAppPool -Name 'SI-PDR-Service'
```

Ou IIS Manager → Application Pools → SI-PDR-Service → Recycle.

---

## 5. Como rodar / testar

Checklist: `CHECKLIST.txt`.

```powershell
pwsh -File packages\LICENSE-SERVER\3-publish-iis\Test-Iis.ps1 `
    -FrameworkRoot 'C:\Linx Program Files\Linx Framework 6.0.0'
```

O teste de arquivo/config:

- `Service\Web.config` tem `LicenseServer.Enabled=true` e BaseUrl HML
- `Service\bin\Linx.Framework.BV.dll` existe
- `RestSharp.dll`, `Newtonsoft.Json.dll`, `Linx.Tools.dll` existem (não foram apagados)
- pool `SI-PDR-Service` Started
- `GET http://localhost:1710/` responde (qualquer HTTP ≠ conexão recusada)
- TCP 443 até `api-hml.linx.com.br` (saída do servidor)

### Teste de login (obrigatório para o gate)

1. Abra o Portal: `http://localhost:8172/` (ou a URL do ambiente).
2. Entre com um usuário **não** de LocalServiceBus.
3. O Service chama o HML na hora do `authenticateUser`.

Resultados possíveis:

| Resultado | Significado |
|-----------|-------------|
| Login entra | `lxStatusChave == 1` (licença Ativa) |
| Login recusa com `LicenseException` / mensagem de licença | fail-closed correto (HML de testes costuma devolver `lxStatusChave=4` / contrato descontinuado) |
| Login entra **sem** chamar HML | `LicenseServer.Enabled=false` **ou** `LocalServiceBus` ligado |

Firewall de saída: o w3wp do Service precisa alcançar `https://api-hml.linx.com.br/app-licensing/`. Proxy corporativo: configure no machine.config / WinHTTP do pool identity.

---

## 6. Desligar o gate (sem desinstalar)

No `Service\Web.config`:

```xml
<add key="LicenseServer.Enabled" value="false" />
```

Recicle `SI-PDR-Service`. O `LicenseControl.Validate` vira no-op. As outras chaves podem ficar.

Para voltar atrás de verdade: restaure `Web.config.bak-*` e `Linx.Framework.BV.dll.bak-*`.

---

## 7. Problemas frequentes

| Sintoma | Causa / o que fazer |
|---------|---------------------|
| Login antigo, sem mensagem de licença | DLL antiga no `Service\bin` — publique de novo e recicle o pool |
| `LicenseException` “Email e Password são obrigatórios” | chaves não mescladas, ou `Web.config` errado |
| Autenticação HTTP 401 no License Server | Email/Password HML errados |
| Timeout / “Falha ao conectar” | firewall, DNS, TLS, proxy; teste `Test-NetConnection api-hml.linx.com.br -Port 443` |
| Login entra mesmo com Enabled=true | `LocalServiceBus` em modo que seta `LocalServiceBus.Enabled` — o bootstrap retorna cedo |
| SQL / ServiceBus sumiram | alguém copiou o `Web.config` inteiro da branch — restaure o backup |
| Portal 8172 ok mas o gate não dispara | o Validate está no Service, não no Portal — confira a DLL e o `Web.config` do **Service** |
| Pool Stopped depois do Recycle | `Start-WebAppPool SI-PDR-Service` |
| `Copy-ToIis` avisou DLL ausente | compile o pacote 2 e copie a DLL para `3-publish-iis\Service\bin\` |

---

## 8. Relação com os outros pacotes

| Pacote | Quando usar |
|--------|-------------|
| `1-apply-iis` | Overlay mínimo (DLL + merge). Sem backup, recycle nem guia de operação |
| `2-overwrite-main` | Código em `Main\` para **compilar** a DLL |
| **`3-publish-iis`** | **Publicar e operar no IIS**: backup, merge, recycle, teste, este guia |

Ordem recomendada num servidor de QA:

1. Pacote 2 no clone → MSBuild Release  
2. DLL em `3-publish-iis\Service\bin\`  
3. `Publish-ToIis.ps1`  
4. `Test-Iis.ps1`  
5. Login no Portal
