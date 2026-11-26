# Pacote do administrador do mesmo grupo

Este pacote tem dois programas e este texto. Os programas falam com o Service pela rede. Um cria um login de administrador ao lado de uma pessoa que já existe. O outro apaga só esse administrador.

Leia até o fim uma vez. Depois use a folha de cola.

## Folha de cola

Troque `teste.franqueado` pelo login da pessoa que já existe. O endereço abaixo é o Service do QA `3.11-NT`. Em outro ambiente, troque só esse endereço.

Abra o PowerShell dentro desta pasta e cole, uma linha de cada vez.

Primeiro, só olhar. Nada é gravado:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
.\criar-admin-mesmo-gpecon.ps1 -LoginAlvo teste.franqueado -ServiceUrl https://qa-ux-svc.linx.com.br/3.11-NT
```

A última linha precisa ser `SIMULACAO: nada foi gravado.` Se for isso, grave de verdade:

```powershell
.\criar-admin-mesmo-gpecon.ps1 -LoginAlvo teste.franqueado -ServiceUrl https://qa-ux-svc.linx.com.br/3.11-NT -Confirmar
```

A última linha precisa começar com `Gravado. Entre com`.

Para apagar esse administrador, primeiro só olhar:

```powershell
.\excluir-admin-mesmo-gpecon.ps1 -LoginAdmin admin.teste.franqueado -ServiceUrl https://qa-ux-svc.linx.com.br/3.11-NT
```

A última linha precisa ser `SIMULACAO: nada foi excluido.` Para apagar de verdade:

```powershell
.\excluir-admin-mesmo-gpecon.ps1 -LoginAdmin admin.teste.franqueado -ServiceUrl https://qa-ux-svc.linx.com.br/3.11-NT -Confirmar
```

A última linha precisa começar com `Excluido:`.

## O que cada palavra quer dizer

- **Login alvo.** A pessoa que já entra no sistema. Exemplo: `teste.franqueado`. O programa não muda a senha dela e não apaga ela.
- **Login admin.** O login novo. O nome é sempre `admin.` mais o login alvo. Exemplo: `admin.teste.franqueado`.
- **Service.** O endereço da API. A tela do navegador é outra. No QA, a tela é `https://qa-ux-app.linx.com.br/3.11-NT/` e o Service é `https://qa-ux-svc.linx.com.br/3.11-NT`.
- **Simulação.** O programa lê e escreve na tela o que faria. Não grava.
- **-Confirmar.** A palavra que autoriza gravar ou apagar. Sem ela, o programa para na simulação.
- **Grupo econômico.** A empresa do login alvo. O admin nasce dentro desse mesmo grupo.

## O que tem nesta pasta

- `LEIA-ME.md`: este texto.
- `criar-admin-mesmo-gpecon.ps1`: cria o admin, os acessos e as regras de módulo.
- `excluir-admin-mesmo-gpecon.ps1`: apaga só o admin criado por aquele programa.

Não mude o miolo dos arquivos `.ps1`. Não junte os dois comandos num só. Não rode os dois ao mesmo tempo.

## O que o programa de criar faz

1. Procura o login alvo.
2. Se `admin.` mais esse login ainda não existe, cria esse login no mesmo grupo.
3. Marca administrador nos mesmos ambientes em que o alvo já entra.
4. Liga os módulos da empresa e grava acesso total aos módulos que a tela de cada ambiente lista.
5. Limpa a memória de módulos do Service. Sem esse passo a tela abre cinza, sem cartões.

Se o login admin já existe, o programa não cria outro. Ele só completa as regras de módulo que faltam.

## O que o programa de apagar faz

Apaga regras, a cópia do usuário em cada aplicação, os acessos, os registros de log desse login e o próprio login. Não apaga módulos da empresa. Não apaga o login alvo.

O nome precisa começar com `admin.`. Se alguém escrever `teste.franqueado`, o programa recusa e não apaga nada.

## Antes de começar

1. Use um Windows com PowerShell. O PowerShell que já vem no Windows serve. Não precisa instalar outro.
2. Descompacte o zip. A pasta precisa mostrar os dois arquivos `.ps1` e este texto, soltos, não dentro de outra pasta com o mesmo nome duas vezes.
3. Conecte a VPN se o seu acesso ao QA exige VPN.
4. Abra no navegador o endereço do Service, `https://qa-ux-svc.linx.com.br/3.11-NT`. Qualquer página do próprio servidor basta, mesmo uma página de erro do site. Se o navegador disser que não conseguiu acessar o site, pare. O programa também não vai conseguir.

## Passo 1. Abrir o PowerShell na pasta

1. Abra a pasta descompactada no Explorador de Arquivos.
2. Clique uma vez na barra de endereço, onde aparece o caminho da pasta.
3. Apague o caminho, digite `powershell` e pressione Enter.

Uma janela azul ou preta abre já dentro da pasta. O texto antes do cursor mostra esse caminho.

Se essa janela não abrir, abra o PowerShell pelo menu Iniciar, digite `cd` e um espaço, arraste a pasta para dentro da janela e pressione Enter.

## Passo 2. Liberar o programa nesta janela

Cole isto e pressione Enter:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
```

Isso vale só para esta janela. Fechar a janela desfaz. Se o Windows perguntar, responda `S` e Enter.

## Passo 3. Simular a criação

Cole o comando da folha de cola, ainda sem `-Confirmar`. Troque o login alvo se não for `teste.franqueado`.

O programa escreve várias linhas. Leia estas:

- `Service:` precisa ser o endereço que você passou, não `http://127.0.0.1:1710`. Se aparecer `127.0.0.1`, você esqueceu o `-ServiceUrl`. Nada foi gravado no QA. Rode de novo com o endereço.
- `Alvo:` é o login que você pediu.
- `Empresa:` é o grupo dessa pessoa.
- `Novo admin:` é o login que será criado.
- Uma linha por ambiente, com o número, o nome e a empresa.
- A última linha é `SIMULACAO: nada foi gravado. Com -Confirmar as regras saem na empresa de cada ambiente e o cache de modulos e limpo.`

Texto em vermelho é erro. Não rode com `-Confirmar`. Copie o texto vermelho inteiro e pare.

`Usuario alvo nao encontrado` quer dizer que o login foi digitado diferente do que existe. Confira pontos e maiúsculas.

## Passo 4. Criar de verdade

Só depois da simulação, cole o mesmo comando com `-Confirmar` no final.

Espere. Pode levar alguns minutos. Não feche a janela.

No fim precisa aparecer `Gravado. Entre com admin....`.

Linhas no meio dizem o que cada ambiente fez:

- `gravando acesso total em N modulos` : esse ambiente recebeu acesso.
- `acesso total ja coberto` : esse ambiente já tinha o acesso. Está certo.
- `usuario nao replicado` ou um texto de falha : esse ambiente ficou sem regra. Anote o número do ambiente. Os outros continuam.

Se o login já existia, aparece `Login admin.... ja existe` e o programa só completa regras. Isso também está certo.

## Passo 5. Achar a senha

A senha já está dentro de `criar-admin-mesmo-gpecon.ps1`. Não está neste texto de propósito.

1. Clique com o botão direito no arquivo `.ps1` de criar.
2. Abra com o Bloco de Notas.
3. Procure a linha que contém `$Senha`.
4. Copie só o que está entre aspas simples.
5. Feche o Bloco de Notas sem salvar.

Use essa senha na tela de login. Não cole a senha no comando do PowerShell: o Windows guarda os comandos digitados. Não envie a senha por chat.

Para escolher outra senha, acrescente `-Senha` no comando de criar. Ela precisa ter 12 caracteres ou mais, com letra maiúscula, letra minúscula, número e um símbolo. Exemplo de formato, não use este valor: `-Senha 'TroqueEstaSenha1!'`.

O e-mail gravado, se você não passar outro, é o login admin seguido de `@example.com`. Para outro e-mail, acrescente `-Email nome@empresa.com`.

## Passo 6. Entrar na tela

1. Abra `https://qa-ux-app.linx.com.br/3.11-NT/`.
2. Entre com o login admin, por exemplo `admin.teste.franqueado`, e com a senha do passo 5.
3. A primeira aba é o ambiente padrão da pessoa alvo. No QA do `teste.franqueado` essa aba é a de Configuração.
4. O cartão **Utilitário (Server)** precisa aparecer. Dentro dele, o menu Sistema tem **Usuário (Server)**.

Se a aba abre e o miolo fica cinza, o navegador ainda está com a lista vazia de um login anterior. Entrar de novo não pede a lista nova. Limpe os dados deste site:

1. Na barra de endereço, clique no cadeado à esquerda de `qa-ux-app.linx.com.br`.
2. Abra os cookies e os dados do site.
3. Remova os dados de `qa-ux-app.linx.com.br`.
4. Feche todas as abas desse endereço.
5. Abra o endereço outra vez e entre de novo com o login admin.

Se o menu do usuário da própria tela abrir, o item **Cache** e o link **Limpar** fazem a mesma limpeza e recarregam a página.

O login alvo, sem o `admin.`, pode continuar com a aba cinza. Isso não indica que o admin falhou. Confira sempre com o login que começa com `admin.`.

## Passo 7. Apagar, se precisar refazer

Rode primeiro sem `-Confirmar`. A última linha precisa ser `SIMULACAO: nada foi excluido.`

Confira a linha `Login:` antes de confirmar. Ela precisa ser o admin, nunca o alvo.

Depois rode com `-Confirmar`. A última linha precisa ser `Excluido: admin.... O script de inclusao pode gravar este login de novo.`

`Login admin.... nao existe. Nada a excluir.` quer dizer que já não havia esse login. Não é erro.

Depois de excluir, o passo 4 pode criar o mesmo nome de novo.

## Quando alguma coisa sai errado

- A janela diz que a execução de scripts está desabilitada. Rode de novo o comando do passo 2, nesta mesma janela.
- O endereço na linha `Service:` é `127.0.0.1`. Faltou `-ServiceUrl`.
- O texto fala em tempo esgotado ou em não resolver o nome. A VPN caiu ou o Service está fora. Pare e teste o endereço no navegador.
- A simulação está certa e o `-Confirmar` para no meio com texto vermelho. Não rode o de apagar ainda. Copie da linha `Service:` até o fim e encaminhe esse texto. Rodar de novo o de criar, com `-Confirmar`, completa o que faltou quando o login já ficou gravado.
- A tela continua cinza depois de limpar os dados do site e entrar de novo. Confira se o login usado é o que começa com `admin.`.

## O que não fazer

- Não rode com `-Confirmar` na primeira vez em um ambiente novo.
- Não apague um login que não começa com `admin.`. O programa recusa, e a recusa é de propósito.
- Não use estes programas para mudar a senha de uma pessoa que já trabalha no sistema.
- Não publique a senha que está dentro do `.ps1`.
