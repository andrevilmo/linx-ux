# SQL MFA + SSO

Catálogo: **Portal / FrameworkAutorizacao** (`TCS_USUARIO_AUTENTICACAO`).  
Não executar no banco da Application.

## Um arquivo (recomendado)

`APPLY_SSO_MFA.sql`

## Avulsos (mesmo schema)

| Arquivo | Objeto |
|---------|--------|
| `INDICA_USUARIO_SERVICO.sql` | coluna `INDICA_USUARIO_SERVICO` |
| `TCS_LOG_ACESSO_AUTH.sql` | tabela de auditoria / lockout |
| `TCS_MFA.sql` | flags SSO/MFA + `TCS_GPECON_MFA` / `TCS_USUARIO_MFA` / dispositivo |
| `TCS_USUARIO_SSO_VINCULO.sql` | vínculo Azure OID+UPN |

## Dados opcionais

`APPLY_SSO_MFA_OPTIONAL_DATA.sql` — ligar Utiliza SSO num login (edite o nome).
