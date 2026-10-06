PACOTE INSTALL_MFA_SSO
======================
Atualizacao de instalacao existente — somente MFA e SSO (Azure AD / Microsoft Entra).

Pasta base tipica da instalacao IIS (ajuste se a sua for outra):

    C:\Linx Program Files\Linx Framework 6.0.0\

Nessa pasta existem os tres sites IIS:

    Portal\          (Web.config do Portal)
    Service\         (Web.config do Service / ServiceBus)
    Application\     (Web.config da Application)

Os caminhos IIS deste pacote sao relativos a essa BASE. Exemplo:

    Pacote:   Portal\bin\Linx.Portal.dll
    Destino:  C:\Linx Program Files\Linx Framework 6.0.0\Portal\bin\Linx.Portal.dll

Inventario e funcao de cada DLL: veja FILES_TO_INSTALL.txt.
Script de banco: DB\APPLY_SSO_MFA.sql (nao copiar para o IIS).


1) BANCO DE DADOS (obrigatorio)
-------------------------------
Execute no SSMS, conectado ao catalogo Portal / FrameworkAutorizacao
(o mesmo que tem LX_TCS.TCS_USUARIO_AUTENTICACAO). NAO rode no catalogo da Application.

    DB\APPLY_SSO_MFA.sql

Idempotente: pode executar de novo. Cria o que faltar:

Colunas em tabelas existentes
    TCS_USUARIO_AUTENTICACAO.INDICA_UTILIZA_SSO
    TCS_USUARIO_AUTENTICACAO.INDICA_UTILIZA_MFA
    TCS_USUARIO_AUTENTICACAO.INDICA_USUARIO_SERVICO
    TCS_LOG_ACESSO_AUTH.INDICA_USUARIO_SERVICO   (se a tabela ja existir)
    TCS_LOG_ACESSO_AUTH.ID_LINX                  (se a tabela ja existir)

Tabelas novas
    TCS_LOG_ACESSO_AUTH            auditoria de login / MFA / SSO
    TCS_GPECON_MFA                 politica MFA da empresa (sem linha = MFA ligado)
    TCS_USUARIO_MFA                segredo TOTP, tentativas, bloqueio
    TCS_USUARIO_MFA_DISPOSITIVO    dispositivo confiavel
    TCS_USUARIO_SSO_VINCULO        vinculo da conta Microsoft (OID + UPN); Revogar SSO

Login do SQL precisa poder CREATE SCHEMA / CREATE TABLE / ALTER TABLE.


2) ANTES DE COPIAR OS ARQUIVOS IIS
----------------------------------
- Faca backup de cada arquivo que sera substituido (pelo menos as DLLs e as views).
- NAO copie Web.config (nao ha Web.config neste pacote de proposito).
  Parametros SSO sao editados no Web.config que ja esta no servidor (secao 5).


3) APLICAR OS ARQUIVOS IIS
--------------------------
Opcao A — lote (recomendado no servidor Windows):

    Aplicar-IIS.bat
    Aplicar-IIS.bat "D:\Linx Program Files\Linx Framework 6.0.0"

Opcao B — copia manual, cada pasta do pacote em cima da pasta do site:

    Portal\*         ->  {BASE}\Portal\
    Service\*        ->  {BASE}\Service\
    Application\*    ->  {BASE}\Application\

Nao copie a pasta DB\ para o IIS.

Ordem sugerida:

    1. DB\APPLY_SSO_MFA.sql no catalogo Portal
    2. Service\bin\*.dll
    3. Portal\bin\*.dll  e  Portal\Views\...
    4. Application\bin\*.dll  e  Application\App\...

Observacao Application: as telas pkg_linx-framework-bv-spa (Cadastro e Manutencao de Usuarios)
leem HTML/JS EMBUTIDOS em Linx.Framework.BV.SPA.dll. Copiar so a pasta App\ solta NAO atualiza
essas rotas. A DLL SPA e obrigatoria.


4) RECICLAR OS APPLICATION POOLS
--------------------------------
No IIS Manager (ou PowerShell):

    Restart-WebAppPool SI-PDR-Service
    Restart-WebAppPool SI-PDR-Portal
    Restart-WebAppPool SI-PDR-Application

Se os pools tiverem outro nome, recicle os pools dos sites Service, Portal e Application.
Como alternativa, "toque" cada Web.config (nao precisa alterar o conteudo).


5) PARAMETROS NO Web.config — SOMENTE MFA / SSO
------------------------------------------------
MFA nao usa chave propria no Web.config. A politica vem do banco
(TCS_GPECON_MFA / INDICA_UTILIZA_MFA). O ticket MFA e gerado pelo Service e
enviado na URL da Application (mfaTicket). Nada para gravar no Web.config da Application
nem do Service para MFA.

SSO e configurado APENAS no Web.config do PORTAL, na secao <PortalSettings>
(nao e <appSettings>).

Arquivo:

    {BASE}\Portal\Web.config

Inclua / ajuste (valores de exemplo — use os da aplicacao Azure AD / Entra do ambiente):

    <configSections>
      <section name="PortalSettings" type="System.Configuration.DictionarySectionHandler" requirePermission="false" />
    </configSections>

    <PortalSettings>
      <add key="authorizationServiceAddress" value="http://localhost:1710/" />
      <add key="PortalUrl" value="http://localhost:8172/" />

      <add key="SSO_HABILITA_AUTENTICACAO" value="true" />
      <add key="SSO_CLIENT_ID" value="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" />
      <add key="SSO_OBJECT_ID" value="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" />
      <add key="SSO_TENANT_ID" value="xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" />
      <add key="SSO_CLIENT_SECRET" value="(segredo da aplicacao Entra — nao versionar)" />
      <add key="SSO_REDIRECT_URI" value="http://localhost:8172/Account/SsoCallback" />
      <add key="SSO_SCOPES" value="User.Read" />
      <add key="SSO_PERMITE_OFFLINE" value="true" />
      <add key="SSO_TIMEOUT_RESPOSTA" value="120" />
    </PortalSettings>

Significado:

    SSO_HABILITA_AUTENTICACAO   true = Portal oferece login Microsoft.
    SSO_CLIENT_ID               Application (client) ID do app Entra.
    SSO_OBJECT_ID               Object ID do app Entra (referencia / diagnostico).
    SSO_TENANT_ID               Directory (tenant) ID.
    SSO_CLIENT_SECRET           Segredo do app (confidential client / MSAL).
                                Deixe vazio no Git; preencha so no servidor.
    SSO_REDIRECT_URI            Tem de ser IDENTICO ao Redirect URI cadastrado no Entra
                                (padrao: {PortalUrl}/Account/SsoCallback).
    SSO_SCOPES                  Escopos OIDC; use User.Read.
    SSO_PERMITE_OFFLINE         true = se o Azure falhar de forma recuperavel, o Portal
                                pode sugerir senha local (contingencia). Com Utiliza SSO,
                                a 2a tela deste pacote mostra so o botao Microsoft.
    SSO_TIMEOUT_RESPOSTA        Timeout em segundos da ida ao Azure.

    authorizationServiceAddress URL do Service (AuthenticatePortalSso, MFA, vinculo SSO).
    PortalUrl                   URL publica deste Portal (usada no callback padrao).

Azure Portal (Entra): cadastre o mesmo Redirect URI e o mesmo Client secret.
Nao coloque o segredo neste pacote.


6) DEPOIS DE APLICAR — CHECAGEM RAPIDA
--------------------------------------
Portal:       http://localhost:8172/Account/Login
              1) Informe o usuario e Continuar.
              2) Se o cadastro tem Utiliza SSO: so o botao "entrar com Microsoft"
                 (sem senha). Se nao tem SSO: tela de senha.
              3) Erro de SSO volta para a 1a tela, limpa cookies/sessao SSO e
                 mostra so a mensagem — tente outro usuario.
              4) MFA TOTP aparece depois do login, se a empresa/usuario exigir.

Application:  o login so entra com mfaTicket valido (ou skip quando MFA nao e exigido).
Cadastro:     Utiliza MFA / Utiliza SSO; Revoga MFA so habilita com Utiliza MFA;
              Revoga SSO so habilita com Utiliza SSO.


7) ATUALIZAR ESTE PACOTE (depois de um novo publish no IIS)
-----------------------------------------------------------
No servidor, na pasta do pacote:

    Atualizar-Pacote.bat
    Atualizar-Pacote.bat "C:\Linx Program Files\Linx Framework 6.0.0"

Copia as DLLs/views do IIS de volta para esta pasta e gera de novo
INSTALL_MFA_SSO.zip ao lado da pasta. Nao altera DB\APPLY_SSO_MFA.sql.


8) O QUE ESTE PACOTE NAO FAZ
----------------------------
- Nao sobrescreve Web.config (SQL, SMTP, secret SSO permanecem no ambiente).
- Nao copia a pasta DB\ para o IIS.
- Nao copia outras DLLs da Application alem das duas listadas em FILES_TO_INSTALL.txt.
