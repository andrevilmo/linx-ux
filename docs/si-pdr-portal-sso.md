# Portal SSO (Azure AD / MSAL)

See [sso-azure-ad-msal-guide.md](sso-azure-ad-msal-guide.md) for the OmniPOS pattern and section **10** for the Portal web adaptation.

## Enable on an environment

1. Azure App Registration (Web platform):
   - Redirect URI = `{PortalUrl}/Account/SsoCallback`
   - Create a client secret → `SSO_CLIENT_SECRET`
   - API permission: Microsoft Graph `User.Read` (+ admin consent if required)
2. Set Portal `PortalSettings`:
   - `SSO_HABILITA_AUTENTICACAO` = `true`
   - `SSO_CLIENT_ID`, `SSO_TENANT_ID`, `SSO_CLIENT_SECRET`
   - `SSO_REDIRECT_URI` matching Azure
   - `SSO_PERMITE_OFFLINE` = `true` to keep password login / contingency
3. Ensure each user `NomeAutenticacao` equals the UPN prefix (before `@`).
4. Redeploy Portal + Service (Service exposes `AuthenticatePortalSso`).
