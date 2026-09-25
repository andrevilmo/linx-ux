# Login Linx UX — descrição para usuário

Versão em Word (frontend, prints e parâmetros SSO da empresa): [Linx-UX-MFA-SSO-Portal.docx](Linx-UX-MFA-SSO-Portal.docx).

O acesso ao Linx UX tem **duas etapas**. Elas não acontecem na mesma tela.

## 1. Entrar (usuário e senha ou Microsoft)

Na tela de login você entra com **usuário e senha** ou com **entrar com Microsoft**.

O login Microsoft (SSO) só prova **quem você é**. Ele **não** substitui o código do aplicativo de autenticação.

## 2. Ambiente

Depois do login, o sistema usa o **GPECON já cadastrado** na sua ficha.

Se você tem um ambiente padrão (ou um único ambiente), o Portal segue em frente sem pedir escolha. Se houver vários ambientes sem padrão, aparece a lista para você escolher.

## 3. Verificação em duas etapas (MFA)

Em seguida o Portal pede o **código de 6 dígitos** gerado pelo Google Authenticator ou Microsoft Authenticator.

- Na **primeira vez**, aparece um **QR Code**. Você escaneia no aplicativo e confirma com o primeiro código.
- Nas vezes seguintes, só informa o código de 6 dígitos.

Essa verificação é da **empresa (GPECON)** no Linx. Mesmo quem entrou com Microsoft precisa dela, se a empresa estiver com MFA ligado.

Usuário de serviço **não entra pelo Portal**. Nas APIs ele entra e **não** pede o código. Também não pedem o código: autenticação Windows, empresa com MFA desligado, ou usuário com “Utiliza MFA” desmarcado.

## O que isso não é

- Não é a mesma coisa que o MFA da Microsoft no Azure.
- Não escolhe sozinho o ambiente só porque o MFA foi cadastrado. GPECON e ambiente padrão são cadastros diferentes.

## Depois que o código é aceito

O Portal abre o Application. Sem o código (quando ele é exigido), o sistema não entra no produto.

## Processo completo de SSO (o que a pessoa percorre)

O login Microsoft não é um clique único. Ele amarra o usuário digitado no CONTINUAR à conta Azure e só então segue para ambiente e MFA.

1. **CONTINUAR** — a pessoa informa o login Linx. O Portal consulta o cadastro (`Utiliza SSO`, e-mail, usuário de serviço). Usuário de serviço é recusado. Se `Utiliza SSO` estiver ligado, o Portal guarda esse login e o e-mail para a Microsoft.
2. **Entrar com Microsoft** — abre a página da Microsoft. O e-mail do cadastro, quando existe, aparece como dica de conta. A Microsoft sempre pede a conta de novo (`prompt=login`).
3. **Volta ao Portal** — a Microsoft devolve o código. O Portal troca o código pelo perfil (OID + UPN) e **não** envia o token Azure ao Service.
4. **Vínculo** — o Service grava ou confirma `TCS_USUARIO_SSO_VINCULO`:
   - primeira vez: insere OID + UPN daquela conta Microsoft no login do CONTINUAR;
   - mesma conta: atualiza a data do último login;
   - outra conta Microsoft no mesmo login Linx: recusa, não abre sessão e volta ao CONTINUAR;
   - o mesmo OID já ligado a outro usuário Linx: recusa.
5. **Sessão Portal** — só depois do vínculo aceito o Service autentica sem senha (`AuthenticatePortalSso`) e o Portal grava o cookie.
6. **Ambiente e MFA** — iguais ao caminho de senha. SSO **não** dispensa o código de 6 dígitos.

A identidade no Linx continua sendo o **Nome de autenticação** digitado no CONTINUAR, não o prefixo do e-mail Microsoft.

## Processo de Revogar SSO

**Revoga SSO** fica no cadastro de usuário (Application), **sempre ao lado** de **Revoga MFA**. O ícone da barra de ferramentas também fica **sempre** visível nesses cadastros.

1. Abra o cadastro do usuário (local ou de autenticação).
2. Os dois botões aparecem na mesma linha. Se não houver vínculo, o clique avisa; o botão não some.
3. Clique em **Revoga SSO**.
4. Confirme: *“Revogar o SSO deste usuário? Remove só o vínculo da conta Microsoft. O próximo login SSO gravará um novo OID/UPN.”*
5. O Service apaga só a linha em `TCS_USUARIO_SSO_VINCULO`. Senha Linx, secret MFA e flags `Utiliza SSO` / `Utiliza MFA` **não** mudam.
6. Na próxima vez que a pessoa fizer CONTINUAR → Microsoft, o Linx grava um vínculo novo com a conta Azure usada naquele momento.

Use Revogar SSO quando a pessoa trocou de conta Microsoft, quando o vínculo ficou com a conta errada, ou depois de um acesso recusado (“conta Microsoft diferente da vinculada”).
