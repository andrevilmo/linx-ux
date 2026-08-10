# Pacote SI-PDR - Segurança / fluxo de senha

Atualização para qualquer ambiente Linx Framework 6.0.0 (Application, Service, Portal + DB).

**Commit:** `{{GIT_COMMIT}}`  
**Branch:** `{{GIT_BRANCH}}`  
**Base:** desde `{{BASE_BRANCH}}`  
**Gerado em:** {{STAMP_FULL}}

**Estrutura = caminhos relativos à raiz da instalação IIS**, por exemplo:  
`C:\Linx Program Files\Linx Framework 6.0.0\`

## Pastas

| Pasta | Destino no servidor |
|-------|---------------------|
| `Application\` | `{InstallRoot}\Application\` |
| `Service\` | `{InstallRoot}\Service\` |
| `Portal\` | `{InstallRoot}\Portal\` |
| `DB\` | Executar no banco (não copiar para IIS) |

## Ordem recomendada

1. **DB** - executar os scripts SQL (autorização / TCS) no banco do ambiente  
2. **Service** - copiar `bin\`, `bin\Extension\` e mesclar `Web.config`  
3. **Application** - copiar `bin\`, `Views\` e `App\` (especialmente `modalChangePassword.js`)  
4. **Portal** - copiar `bin\`, `Views\` e `assets\`  
5. Reciclar Application pools (ou tocar cada `Web.config`)

## Atenção - Service\Web.config

- Inclui Membership (`maxInvalidPasswordAttempts=5`, `passwordAttemptWindow=10`).
- Inclui `SendEmailSettings` (SMTP). **Ajuste usuário/senha/SMTP do ambiente de destino** antes de sobrescrever o Web.config de produção. Preferível mesclar só a seção `membership` e as chaves de e-mail necessárias.

## Scripts DB

1. `TCS_LOG_ACESSO_AUTH.sql`  
2. `INDICA_USUARIO_SERVICO.sql`  
3. `Disable_Update_aspnet_Membership_Trigger.sql`

## Conteúdo

- **Application:** DLLs (incl. BV.SPA), views Forgot/Reset Password, footer/layout, App SPA cadastros + `modalChangePassword.js`
- **Service:** DLLs BV/Tools/BM/WebAPI + Extension AuthenticateUser + Web.config  
- **Portal:** Linx.Portal + Linx.Tools, Login.cshtml, portal.css  
- **DB:** 3 scripts SQL  

Inventário detalhado: `Desktop\DEPLOY_ITEMS_SEGURANCA.MD`

## Arquivos neste pacote ({{FILE_COUNT}})

{{FILE_LIST}}