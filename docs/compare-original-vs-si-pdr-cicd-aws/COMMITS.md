# Commits on `SI-PDR-CICD-AWS` not in `original`

Newest first. Count: **68**.

| SHA | Date | Author | Subject |
| --- | --- | --- | --- |
| 64739300 | 2026-08-11 | Cursor Agent | fix(si-pdr): apply QA 3-12 Service connectionStrings for AWS IIS |
| ca8192bb | 2026-08-10 | Cursor Agent | ci(si-pdr): touch Portal SSO file to retrigger AWS IIS path filters |
| 35be3b44 | 2026-08-10 | Cursor Agent | ci(si-pdr): retrigger AWS IIS deploy after SSO MSAL compile fix |
| b0e913cf | 2026-08-10 | Cursor Agent | fix(sso): require confidential client for Portal authorize URL |
| 74a5488d | 2026-08-10 | Cursor Agent | merge: publish new-features (SSO, password 5min, Bloqueado) to AWS CI |
| 58aa47a0 | 2026-08-10 | Cursor Agent | feat: Azure AD SSO (MSAL) for Portal per OmniPOS guide |
| d036b366 | 2026-08-10 | Cursor Agent | feat: password link 5min, auth audit ID_LINX, login forget-password bottom, Bloqueado filter |
| 1a02800c | 2026-08-10 | Cursor Agent | merge: bring password-flow, auth audit, and Bloqueado UI into new-features branch |
| e2a65145 | 2026-08-10 | Cursor Agent | test(si-pdr): smoke Portal login after Service warm-up |
| ab1a0640 | 2026-08-10 | Cursor Agent | fix(si-pdr): diagnose Service hang and isolate IIS app pools |
| 55470c35 | 2026-08-07 | Cursor Agent | fix(si-pdr): remove ambiguous Service connectionString comment block |
| 3bfe7a51 | 2026-08-07 | Cursor Agent | fix(si-pdr): clean Service connectionString comments and timeouts |
| 96ae8b23 | 2026-08-07 | Cursor Agent | fix(si-pdr): Office365 email settings and Service login 500 diagnostics |
| 018a40c3 | 2026-08-07 | Cursor Agent | docs(si-pdr): note persistent C:\lx\si-pdr workspace for incremental builds |
| 2935c352 | 2026-08-07 | Cursor Agent | fix(ci): UTF-8 BOM + ASCII-only in Ensure-IisSiPdr for PS 5.1 |
| bf8d10d6 | 2026-08-07 | Cursor Agent | perf(si-pdr): speed up publish with incremental workspace and skip_build |
| 58b700f9 | 2026-08-07 | Cursor Agent | ci(si-pdr): deploy QA web.configs now that host is free |
| 9b404b10 | 2026-08-07 | Cursor Agent | ci(si-pdr): retrigger deploy with QA web.configs |
| 3cb9559c | 2026-08-07 | Cursor Agent | ci(si-pdr): redeploy QA web.configs after cancelled run |
| 818c83dc | 2026-08-07 | Cursor Agent | fix(si-pdr): apply QA Binary web.configs and IIS ports 8174/8172/1710 |
| e5780050 | 2026-08-07 | Cursor Agent | fix(si-pdr): apply QA Service Web.config with PROD ShellMode |
| 3ca681ce | 2026-08-07 | Cursor Agent | ci(si-pdr): pass SI_PDR_LOCAL_SERVICEBUS_MODE to SSM |
| 8c7acc72 | 2026-08-07 | Cursor Agent | fix(si-pdr): disable LocalServiceBus DEV so modules use Portal user |
| 4e1db925 | 2026-08-07 | Cursor Agent | ci(si-pdr): pass optional SI_PDR_SHELL_MODE secret to SSM |
| 7999f50b | 2026-08-07 | Cursor Agent | fix(si-pdr): use ShellMode PROD so module cards show DB names |
| e0a25d9e | 2026-08-07 | Cursor Agent | fix(si-pdr): Portal login SQL overrides and Service :8082 |
| 76c883d4 | 2026-08-07 | Cursor Agent | chore(si-pdr): mention 8172/8174 aliases in pipeline success banner |
| dd5e92c2 | 2026-08-07 | Cursor Agent | fix(si-pdr): bind IIS Express ports 8172/8174 for Portal redirects |
| ef10c5a7 | 2026-08-06 | Cursor Agent | fix(ci): build Application as Release\|Any CPU to skip WinHost |
| b783df9f | 2026-08-06 | Cursor Agent | fix(ci): quote paths with spaces for Start-Process script args |
| 9ceb8d77 | 2026-08-06 | Cursor Agent | fix(ci): restore SI-PDR failure logs and harden pipeline exits |
| 73872b52 | 2026-08-06 | Cursor Agent | fix(si-pdr): bind Service on :1710 for Application ServiceBus |
| 13fe5dc8 | 2026-08-06 | Cursor Agent | fix(deploy): sync Binary web.config to IIS sites on each deploy |
| 47edb0bd | 2026-08-06 | Cursor Agent | fix(si-pdr): seed IIS site web.config with MVC assemblyBinding redirects |
| 814cf4e9 | 2026-08-05 | Cursor Agent | fix(ci): install WebBuildTools via Chocolatey workload package |
| 9e5b01ff | 2026-08-05 | Cursor Agent | chore(ci): drop duplicate binaryRoot assignment |
| dadcc299 | 2026-08-05 | Cursor Agent | fix(ci): install VS WebBuildTools for WebApplication.targets |
| 293c3b96 | 2026-08-05 | Cursor Agent | perf(ci): use tar.gz + tar.exe instead of Expand-Archive |
| a85d36f5 | 2026-08-05 | Cursor Agent | fix(ci): satisfy BV PostBuildEvent Help xcopy on clean checkouts |
| 7b3c7110 | 2026-08-05 | Cursor Agent | fix(ci): seed Framework Library from Main/Binary for MSBuild |
| 191c78d8 | 2026-08-05 | Cursor Agent | fix(ci): seed AssemblyInfoShared.cs into Linx Program Files |
| bdb28b1b | 2026-08-05 | Cursor Agent | fix(ci): download SI-PDR zip with curl.exe fallback |
| f088f593 | 2026-08-05 | Cursor Agent | fix(ci): shrink SI-PDR source zip excludes |
| ab2cb893 | 2026-08-05 | Cursor Agent | fix(ci): install AWS CLI on Windows build host |
| 7ae5583f | 2026-08-05 | Cursor Agent | fix(ci): faster SI-PDR package transfer and smaller zip |
| a9993ebd | 2026-08-05 | Cursor Agent | fix(ci): detect incomplete net461 pack and install developer pack |
| f9648ca8 | 2026-08-05 | Cursor Agent | fix(ci): ensure .NET Framework 4.6.1 targeting pack on Build Tools |
| a6a7e692 | 2026-08-05 | Cursor Agent | fix(ci): make vswhere find VS 2022 Build Tools MSBuild |
| a24389fe | 2026-08-05 | Cursor Agent | docs: link AWS IIS CI in TASKS.README |
| 14da089e | 2026-08-05 | Cursor Agent | feat(ci): AWS IIS CI/CD for Application, Service, and Portal |
| a802e617 | 2026-08-04 | andre vilmo | adicionando evento de desbloqueio na troca de senha pelo proprio usuario |
| 106ea26b | 2026-08-03 | andre vilmo | Criando task para criar pacote de deploy |
| 87557883 | 2026-08-03 | andre vilmo | Flow de auditoria de desbloqueio |
| 328112f0 | 2026-07-31 | andre vilmo | Correção depois que a senha expirada é alterada pra voltar pro login |
| e6087027 | 2026-07-31 | andre vilmo | resolvendo problema do envio de e-mail com smtp linx e desbloqueio do usuário após alteração de senha no flow de envio de e-mail |
| 73ba4c4f | 2026-07-28 | andre vilmo | Implementação validada faltando apenas adiciona indoca suaurio serviço na auditoria e mensagem de erros com quantidade restante de tentativas no login |
| e6e4d0cd | 2026-07-27 | andre vilmo | Integração com flow de senha via link por e-mail ok e tela de debloqueio funcionando |
| 6a6648a9 | 2026-07-27 | andre vilmo | merge GpeCon em outras telas |
| 7ec12310 | 2026-07-25 | andre vilmo | Merge branch 'adding-password-flow' into footer-presente-colocando-filtro-codigo-gpecon-na-exportacao |
| 707d0560 | 2026-07-25 | andre vilmo | log de auditoria na tabela  "LX_TCS"."TCS_LOG_ACESSO_AUTH" funcionando |
| 3c665ab4 | 2026-07-24 | andre vilmo | Merge branchs footer-presente and  'feature/colocando-filtro-codigo-gpecon-na-exportacao' into original |
| 398f5903 | 2026-07-24 | andre vilmo | Arrumando a exportação para incluir código gpecon |
| 0c03f834 | 2026-07-23 | andre vilmo | bloqueio de senha em 5 tentativas - cadastro usuario server ja tem botao de desbloqueio |
| eab1cb71 | 2026-07-22 | andre vilmo | Alterações para flow da senha |
| c73a3da4 | 2026-07-17 | andre vilmo | Problema footer resolvido |
| e716d672 | 2026-07-15 | andre vilmo | resolvendo ultimo problema do footer, falta o sombreamento que ficou no mainFooter e adicionar para o class="footer" |
| cfa32ee5 | 2026-07-15 | andre vilmo | footer funcionando, mas com detalhe de estar sobrepondo o menu lateral quando modifica zoom ou redefinição de resolução ou modifica a janela |
| 533dde7e | 2026-07-15 | andre vilmo | colocando footer |
