# Inventário de deploy - Segurança / fluxo de senha

**Base de comparação:** branch `{{BASE_BRANCH}}`  
**Branch atual:** `{{GIT_BRANCH}}`  
**Commit de referência:** `{{GIT_COMMIT}}`  
**Data do inventário:** {{STAMP}} (gerado em {{STAMP_FULL}})  
**Pacote:** `Desktop\{{PACKAGE_NAME}}\`

Este documento lista os arquivos (DLLs, views, configs, SPA/App e scripts SQL) empacotados para atualizar outros ambientes com as alterações de segurança desde a branch `{{BASE_BRANCH}}`.

Destino típico IIS: `C:\Linx Program Files\Linx Framework 6.0.0\{Application|Service|Portal}\...`

> Regenerar este inventário, o `LEIA-ME.md` e o pacote com a task  
> **Update SI-PDR package (inventory + LEIA-ME + package)**  
> (script: `.vscode/pack-si-pdr.ps1`).

---

## Resumo funcional

- Bloqueio de conta após 5 tentativas inválidas (Membership ASP.NET)
- Fluxo esqueci/redefinir senha por link de e-mail
- Desbloqueio do usuário após alteração de senha no fluxo por e-mail
- Ajuste de envio de e-mail (SMTP Linx / `SendEmailSettings`)
- Mensagens de login (tentativas restantes) e tela de login do Portal
- Auditoria em `LX_TCS.TCS_LOG_ACESSO_AUTH` e flag `INDICA_USUARIO_SERVICO`
- Cadastros SPA: desbloqueio / usuário autenticação e local
- Footer / layout da Application e modal de troca de senha
- Após alterar senha expirada, redireciona de volta ao login (`modalChangePassword.js`)

---

## 1. Application

Arquivos no pacote:

{{APP_LIST}}

### Destinos IIS

- DLLs -> `Application\bin\`
- Views -> `Application\Views\`
- App/SPA soltos -> `Application\App\` (se o site usar arquivos soltos; SPA cadastros também via `Linx.Framework.BV.SPA.dll`)
- `modalChangePassword.js` é crítico quando houver pasta `App`

---

## 2. Service

Arquivos no pacote:

{{SERVICE_LIST}}

### Destinos IIS

- DLLs -> `Service\bin\`
- Extension -> `Service\bin\Extension\`
- Config -> mesclar `Service\Web.config` (Membership + SendEmailSettings **do ambiente**)

---

## 3. Portal

Arquivos no pacote:

{{PORTAL_LIST}}

### Destinos IIS

- DLLs -> `Portal\bin\`
- View -> `Portal\Views\Account\Login.cshtml`
- CSS -> `Portal\assets\css\portal.css`

---

## 4. DB (banco - não IIS)

{{DB_LIST}}

Ordem sugerida:

1. `TCS_LOG_ACESSO_AUTH.sql`
2. `INDICA_USUARIO_SERVICO.sql`
3. `Disable_Update_aspnet_Membership_Trigger.sql`

---

## 5. Checklist

### Application
- [ ] Copiar `SI-PDR\Application\bin`
- [ ] Copiar `SI-PDR\Application\Views`
- [ ] Copiar `SI-PDR\Application\App` (se aplicável)
- [ ] Reciclar Application

### Service
- [ ] Copiar `SI-PDR\Service\bin` (+ `Extension`)
- [ ] Mesclar `Web.config` (SMTP do ambiente)
- [ ] Executar scripts `DB\`
- [ ] Reciclar Service

### Portal
- [ ] Copiar `SI-PDR\Portal\bin`
- [ ] Copiar Login.cshtml e portal.css
- [ ] Reciclar Portal

---

## 6. Observações

1. Pacote em `Desktop\{{PACKAGE_NAME}}\` - estrutura espelha a raiz IIS + pasta `DB`.
2. Ver `{{PACKAGE_NAME}}\LEIA-ME.md` para ordem de aplicação.
3. **Não copiar cegamente senhas SMTP** do `Web.config`.
4. PDBs são opcionais.
5. Arquivos faltando na geração: **{{MISSING}}**.

---

## 7. Lista plana (relativo à raiz IIS / pacote)

```
{{FLAT_LIST}}
```