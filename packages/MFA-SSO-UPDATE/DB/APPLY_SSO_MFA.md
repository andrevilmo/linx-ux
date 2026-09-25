# Apply SSO + MFA database changes

Run these on the **Portal / FrameworkAutorizacao** SQL catalog (the database that holds `LX_TCS.TCS_USUARIO_AUTENTICACAO`). On the AWS SI-PDR host that is typically `QA-UX-Portal-3-12`.

Do **not** run them on the Application catalog.

## 1. Schema (required)

In SSMS, connected to the Portal catalog, execute:

`APPLY_SSO_MFA.sql`

Creates / adds, if missing:

| Object | Purpose |
|--------|---------|
| `TCS_USUARIO_AUTENTICACAO.INDICA_UTILIZA_SSO` | Utiliza SSO (Microsoft button) |
| `TCS_USUARIO_AUTENTICACAO.INDICA_UTILIZA_MFA` | Utiliza MFA (`NULL` or `1` = on, `0` = skip TOTP) |
| `TCS_USUARIO_AUTENTICACAO.INDICA_USUARIO_SERVICO` | Service user skip MFA |
| `TCS_LOG_ACESSO_AUTH` | Login + MFA + SSO process audit / password lockout (`I` = SSO step) |
| `TCS_GPECON_MFA` | Company MFA policy (no row = MFA on) |
| `TCS_USUARIO_MFA` | TOTP secret, attempts, lockout |
| `TCS_USUARIO_MFA_DISPOSITIVO` | Remember-device tokens |
| `TCS_USUARIO_SSO_VINCULO` | Last Azure OID+UPN bound to a Linx user (Revogar SSO) |

The Service also runs a subset of this on first MFA API call if the SQL login has `ALTER`. Use this script when that login cannot alter, or to apply ahead of time.

## 2. Optional data

`APPLY_SSO_MFA_OPTIONAL_DATA.sql`

- Batch A (active): set `INDICA_UTILIZA_SSO = 1` for `andrevilmo`. Change the name first.
- Other batches are commented: user MFA off, company MFA policy, TOTP unlock.

SSO mapping: Entra UPN prefix before `@` = `NOME_AUTENTICACAO`.

## 3. Existing split scripts (same schema)

If you prefer one object per file:

- `INDICA_USUARIO_SERVICO.sql`
- `TCS_LOG_ACESSO_AUTH.sql`
- `TCS_MFA.sql`
- `TCS_USUARIO_SSO_VINCULO.sql`
