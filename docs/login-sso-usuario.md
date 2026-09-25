# SSO no Portal Linx UX — guia de alto nível

Versão em Word (telas, requisitos e Revoga SSO): [Linx-UX-SSO-Portal.docx](Linx-UX-SSO-Portal.docx).

Este texto é **só SSO** (entrar com Microsoft). Não descreve o MFA Linx. Se a empresa tiver MFA ligado, o código de 6 dígitos continua existindo **depois** do SSO; o login Microsoft não o substitui.

## O que a pessoa faz

1. Na primeira tela informa o **login Linx** e clica em **CONTINUAR**.
2. Se o cadastro tiver **Utiliza SSO** e o Portal estiver com SSO ligado, aparece **entrar com Microsoft**.
3. A Microsoft pede a conta da empresa (sempre de novo). O e-mail do cadastro, quando existe, aparece como dica.
4. Ao voltar, o Linx amarra aquela conta Microsoft ao login digitado no CONTINUAR.
5. O Portal segue para o ambiente (e, se a empresa exigir, para o MFA). Depois abre o Application.

A identidade no Linx é o **Nome de autenticação** do CONTINUAR, não o prefixo do e-mail Microsoft.

## Vínculo da conta

- **Primeira vez** — o Linx grava a conta Microsoft (OID + UPN) naquele usuário.
- **Mesma conta** — entra de novo; atualiza a data do último login.
- **Outra conta Microsoft no mesmo login Linx** — recusa, não abre sessão e volta ao CONTINUAR.
- **A mesma conta Microsoft já ligada a outro usuário Linx** — recusa.

## Revoga SSO

No cadastro de usuário (Application), **Revoga SSO** fica sempre ao lado de **Revoga MFA**. O ícone da barra também fica sempre visível nesses cadastros.

Remove só o vínculo da conta Microsoft. Senha Linx e a flag **Utiliza SSO** não mudam. No próximo CONTINUAR → Microsoft o Linx grava um vínculo novo.

Use quando a pessoa trocou de conta Microsoft, quando o vínculo ficou errado, ou depois do aviso “conta Microsoft diferente da vinculada”.

## Requisitos

### Empresa (Microsoft Entra ID)

| Requisito | Por quê |
|-----------|---------|
| Tenant Entra ID da empresa | Diretório onde as pessoas autenticam |
| App registration do tipo **Web** (não desktop) | O Portal é confidential client |
| Directory (tenant) ID | `SSO_TENANT_ID` |
| Application (client) ID | `SSO_CLIENT_ID` |
| Object ID do aplicativo | `SSO_OBJECT_ID` (referência) |
| Client secret válido | `SSO_CLIENT_SECRET` — sem isso o retorno da Microsoft não fecha |
| Redirect URI = URL pública do Portal + `/Account/SsoCallback` | Tem de ser idêntica (http/https, host, porta, caminho) |
| Permissão Microsoft Graph **User.Read** | Ler o perfil/UPN; consentimento do admin se a política exigir |
| Contas só deste diretório (tenant único), em geral | Evita login de conta pessoal / outro tenant |
| Pessoas existentes e ativas no Entra | Quem não entra na Microsoft não entra no SSO Linx |

A URI de redirecionamento é o item que mais quebra implantação. Exemplos: `https://portal.empresa.com.br/Account/SsoCallback` ou `https://qa-ux.linx.com.br/3.11-NT/Account/SsoCallback`. O secret expira: gere outro no Azure e atualize só `SSO_CLIENT_SECRET` — não republica DLL.

### Portal (Web.config / PortalSettings)

| Chave | Obrigatória | Significado |
|-------|-------------|-------------|
| `SSO_HABILITA_AUTENTICACAO` | sim | `true` liga o SSO. `false` esconde o Microsoft |
| `SSO_CLIENT_ID` | se SSO on | Client ID do app |
| `SSO_TENANT_ID` | se SSO on | Tenant ID |
| `SSO_CLIENT_SECRET` | se SSO on | Valor do secret (ou variável `SI_PDR_SSO_CLIENT_SECRET`) |
| `SSO_OBJECT_ID` | recomendada | Object ID do app |
| `SSO_REDIRECT_URI` | sim na prática | URL + `/Account/SsoCallback` |
| `SSO_SCOPES` | não | Padrão `User.Read` |
| `SSO_PERMITE_OFFLINE` | recomendada `true` | Se a Microsoft falhar, permite senha Linx |
| `SSO_TIMEOUT_RESPOSTA` | não | Segundos (padrão 120) |
| `PortalUrl` | sim | URL pública deste Portal |
| `authorizationServiceAddress` | sim | URL do Service (cadastro e vínculo) |

Não copie um `Web.config` de outro ambiente por cima. Recicle o pool do Portal depois de alterar.

### Cadastro Linx (por pessoa)

| Campo | Efeito |
|-------|--------|
| Nome de autenticação | O que a pessoa digita no CONTINUAR. O SSO continua com esse login |
| E-mail | Dica na Microsoft (`login_hint`). Se vazio e o login já tiver `@`, usa o login |
| Utiliza SSO | Ligado: mostra **entrar com Microsoft**. Desligado: só senha |
| Usuário de serviço | Portal recusa. Esse perfil é para API |
| Ambiente padrão / GPECON | Define se a lista de ambientes aparece |

Sem ficha local o retorno da Microsoft volta “sem cadastro local”. Sem **Utiliza SSO** o botão Microsoft não aparece.

### Service e banco (catálogo do Portal)

- Service atualizado com vínculo SSO (`BindPortalSsoVinculo` / `RevokePortalSsoVinculo`). Sem isso o callback pode mostrar “Falha ao gravar vínculo SSO: Not Found”.
- Script `APPLY_SSO_MFA.sql` no catálogo Portal: tabela `TCS_USUARIO_SSO_VINCULO` e log `TCS_LOG_ACESSO_AUTH`.
- Não rode o script no catálogo da Application.

### Quem usa o Portal

- Browser (não é o caminho do usuário de serviço).
- Saber o **login Linx** para o CONTINUAR.
- Conta Microsoft corporativa ativa no tenant da empresa.

## Benefícios

- A pessoa entra com a conta que a empresa já gerencia no Entra ID (bloqueio, desligamento, políticas).
- Menos senha Linx no dia a dia.
- A cada SSO o Portal pede a conta Microsoft de novo (browser compartilhado).
- Depois do primeiro SSO, outra conta Microsoft não entra no mesmo usuário Linx.
- **Revoga SSO** troca o vínculo sem apagar senha.
- Se a Microsoft estiver fora e `SSO_PERMITE_OFFLINE` estiver ligado, a senha Linx continua valendo.

## O que isto não é

- Não é o MFA da Microsoft no lugar de outro fator Linx.
- Não escolhe ambiente sozinho — ambiente padrão é cadastro à parte.
- O token Azure **não** vai ao Service. O Linx só usa OID + UPN para amarrar a conta.
- Usuário de serviço não entra pelo Portal.

## Checklist

1. App registration Web no tenant, secret válido, `User.Read` (admin consent se preciso).
2. Redirect URI no Azure = URL real do Portal + `/Account/SsoCallback`.
3. `PortalSettings` preenchidas; reciclar o pool do Portal.
4. Service com as APIs de vínculo; SQL `APPLY_SSO_MFA.sql` no catálogo Portal.
5. Usuário de teste: cadastro local, **Utiliza SSO**, e-mail corporativo, não é usuário de serviço.
6. Prova: CONTINUAR → Microsoft → Application (ou ambiente).
7. Prova negativa: outra conta Microsoft no mesmo login Linx deve voltar ao CONTINUAR.
8. Prova Revoga SSO: o botão aparece ao lado de Revoga MFA; depois do revoke, o próximo Microsoft grava vínculo novo.
