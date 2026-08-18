# Comparação: `original` × `SI-PDR-CICD-AWS`

A branch `original` é ancestral de `SI-PDR-CICD-AWS`. Há **68 commits** e **813 arquivos** alterados (+344.406 / −6.304 linhas).

## Pacotes

- Este diretório: README (inglês), este LEIA-ME, inventários, patch dos 121 arquivos-fonte, e pasta `changed-files/` com **todos** os 813 arquivos no estado da ponta `SI-PDR-CICD-AWS`.
- A task **Update SI-PDR package** gera outro pacote (`Desktop/SI-PDR`) só do fluxo de senha/segurança, não do CI/CD AWS nem do SSO completo.

## Itens de trabalho

1. **Footer** da Application (layout fixo, LESS).
2. **Filtro/código GpeCon** na exportação.
3. **Fluxo de senha:** 5 tentativas, link por e-mail (5 min), SMTP, desbloqueio, modal de senha expirada.
4. **Auditoria** `TCS_LOG_ACESSO_AUTH` + `INDICA_USUARIO_SERVICO`.
5. **Login Portal:** forget-password no rodapé, filtro Bloqueado.
6. **SSO Azure AD (MSAL)** no Portal (cliente confidencial).
7. **CI/CD AWS IIS:** GitHub Actions → EC2 Windows, sites 8174/8172/1710, configs QA 3-12.
8. **Tasks VS Code:** **Build Linx.Tools** e **Update SI-PDR package**.
9. **Web.config** QA, portas IIS Express, ShellMode PROD, LocalServiceBus PROD.

## Tasks para aplicar

- Desenvolvimento local: **Build All** → **Deploy to Linx Framework 6.0.0**.
- Pacote de segurança para outro ambiente: **Update SI-PDR package**.
- AWS: push na branch `SI-PDR-CICD-AWS` (workflow `si-pdr-aws-iis.yml`).

Detalhes e lista completa de arquivos: `README.md` e `FILE_INVENTORY.md`.
