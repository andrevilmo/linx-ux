# Comparação: `original` × `footer-presente-colocando-filtro-codigo-gpecon-na-exportacao`

As branches **divergiram** a partir de `master`. Não há ancestral linear.

- `original`: 1 commit exclusivo (*adicionando tasks para vscode*).
- Footer: **17 commits** exclusivos.
- Diff de ponta a ponta: **768 arquivos** (553 novos, 215 modificados).

Os arquivos em `changed-files/` estão no estado da ponta **footer**.

## Itens de trabalho (só no footer, em relação a `original`)

1. **Footer** da Application (layout fixo, LESS).
2. **GpeCon** na exportação e em outras telas (via merges; a branch `feature/colocando-filtro-codigo-gpecon-na-exportacao` não é ancestral direto).
3. **Fluxo de senha:** 5 tentativas, link por e-mail, SMTP Linx, desbloqueio (inclusive após o próprio usuário trocar a senha).
4. **Auditoria** `TCS_LOG_ACESSO_AUTH` + `INDICA_USUARIO_SERVICO`.
5. **Login Portal** (mensagens / layout).
6. **Tasks VS Code novas:** **Build Linx.Tools** e **Update SI-PDR package**.

Não inclui CI/CD AWS nem SSO Azure AD (isso está em `SI-PDR-CICD-AWS`).

## Tasks para aplicar

- Local: **Build All** (+ **Build Linx.Tools**) → **Deploy to Linx Framework 6.0.0**.
- Pacote de segurança: **Update SI-PDR package** (compara com `original` por padrão).

Detalhes: `README.md`, `FILE_INVENTORY.md`, `TASKS.md`.
