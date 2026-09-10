# Login Linx UX — descrição para usuário

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

Alguns casos **não** pedem o código: usuário de serviço, autenticação Windows, empresa com MFA desligado, ou usuário com “Utiliza MFA” desmarcado.

## O que isso não é

- Não é a mesma coisa que o MFA da Microsoft no Azure.
- Não escolhe sozinho o ambiente só porque o MFA foi cadastrado. GPECON e ambiente padrão são cadastros diferentes.

## Depois que o código é aceito

O Portal abre o Application. Sem o código (quando ele é exigido), o sistema não entra no produto.
