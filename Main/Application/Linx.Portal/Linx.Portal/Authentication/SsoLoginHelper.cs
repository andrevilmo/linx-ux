using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Identity.Client;

namespace Linx.Portal.Authentication
{
    /// <summary>
    /// OmniPOS-style orchestration: MSAL ForceLogin → UPN prefix → local user session.
    /// Azure token is identity proof only; Portal Forms cookie uses local NomeAutenticacao.
    /// </summary>
    public static class SsoLoginHelper
    {
        public const string ContingencySessionKey = "EnableContingencySSO";
        public const string OAuthStateSessionKey = "SsoOAuthState";

        public static string ExtractLocalLogin(string upn)
        {
            if (string.IsNullOrWhiteSpace(upn))
                return null;

            var at = upn.IndexOf('@');
            var local = (at > 0 ? upn.Substring(0, at) : upn).ToLowerInvariant();
            return local;
        }

        public static MsalAuthenticationService CreateService()
        {
            var options = Utils.GetAzureAdOptions();
            return new MsalAuthenticationService(options, new FileTokenCacheStore("msal.cache", "LinxPortal"));
        }

        public static async Task<Uri> BeginForceLoginAsync(HttpSessionStateBase session)
        {
            var state = Guid.NewGuid().ToString("N");
            if (session != null)
                session[OAuthStateSessionKey] = state;

            var service = CreateService();
            return await service.GetAuthorizationUrlAsync(state, forceLogin: true).ConfigureAwait(false);
        }

        public static async Task<AuthenticationResultModel> CompleteForceLoginAsync(
            string code,
            string state,
            HttpSessionStateBase session)
        {
            if (session != null)
            {
                var expected = session[OAuthStateSessionKey] as string;
                session.Remove(OAuthStateSessionKey);
                if (!string.IsNullOrEmpty(expected) &&
                    !string.Equals(expected, state, StringComparison.Ordinal))
                {
                    return new AuthenticationResultModel
                    {
                        IsAuthenticated = false,
                        Message = "Estado de autenticação inválido. Tente novamente."
                    };
                }
            }

            var service = CreateService();
            return await service.CompleteAuthorizationCodeAsync(code).ConfigureAwait(false);
        }

        public static string MapMsalException(Exception ex, out bool suggestContingency)
        {
            suggestContingency = false;
            if (ex == null)
                return "Não foi possível realizar autenticação.";

            for (Exception walk = ex; walk != null; walk = walk.InnerException)
            {
                var canceled = walk as MsalClientException;
                if (canceled != null &&
                    string.Equals(canceled.ErrorCode, "authentication_canceled", StringComparison.OrdinalIgnoreCase))
                    return "O usuário abortou o processo de autenticação.";

                var uiFailed = walk as MsalClientException;
                if (uiFailed != null &&
                    string.Equals(uiFailed.ErrorCode, "authentication_ui_failed", StringComparison.OrdinalIgnoreCase))
                    suggestContingency = true;

                if (walk is MsalServiceException)
                    suggestContingency = true;
            }

            string detail = FormatExceptionChain(ex);
            string hint = DescribeSsoConfig();
            if (!string.IsNullOrEmpty(hint))
                return hint + " | " + detail;
            return detail;
        }

        public static string DescribeSsoConfig()
        {
            try
            {
                AzureAdOptions options = Utils.GetAzureAdOptions();
                StringBuilder sb = new StringBuilder();
                sb.Append("SSO_CLIENT_ID=").Append(string.IsNullOrWhiteSpace(options.ClientId) ? "vazio" : "ok");
                sb.Append(" SSO_TENANT_ID=").Append(string.IsNullOrWhiteSpace(options.TenantId) ? "vazio" : "ok");
                sb.Append(" SSO_REDIRECT_URI=").Append(string.IsNullOrWhiteSpace(options.RedirectUri) ? "vazio" : options.RedirectUri);
                if (string.IsNullOrWhiteSpace(options.ClientSecret))
                    sb.Append(" SSO_CLIENT_SECRET=vazio no PortalSettings");
                else
                    sb.Append(" SSO_CLIENT_SECRET=ok(len=").Append(options.ClientSecret.Length).Append(")");
                return sb.ToString();
            }
            catch (Exception cfgEx)
            {
                return "PortalSettings: " + cfgEx.Message;
            }
        }

        private static string FormatExceptionChain(Exception ex)
        {
            var parts = new List<string>();
            for (Exception e = ex; e != null && parts.Count < 4; e = e.InnerException)
            {
                var svc = e as MsalServiceException;
                var cli = e as MsalClientException;
                string piece;
                if (svc != null)
                    piece = string.IsNullOrEmpty(svc.ErrorCode) ? svc.Message : (svc.ErrorCode + ": " + svc.Message);
                else if (cli != null)
                    piece = string.IsNullOrEmpty(cli.ErrorCode) ? cli.Message : (cli.ErrorCode + ": " + cli.Message);
                else
                    piece = e.GetType().Name + ": " + e.Message;
                piece = RedactSecrets(piece);
                if (!string.IsNullOrWhiteSpace(piece) && !parts.Contains(piece))
                    parts.Add(piece);
            }
            if (parts.Count == 0)
                return "Não foi possível realizar autenticação.";
            return string.Join(" | ", parts.ToArray());
        }

        private static string RedactSecrets(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;
            try
            {
                AzureAdOptions options = Utils.GetAzureAdOptions();
                if (!string.IsNullOrWhiteSpace(options.ClientSecret) && text.IndexOf(options.ClientSecret, StringComparison.Ordinal) >= 0)
                    text = text.Replace(options.ClientSecret, "***");
            }
            catch
            {
            }
            return text;
        }

        public static bool IsContingencyEnabled(HttpSessionStateBase session)
        {
            if (session == null)
                return false;
            var value = session[ContingencySessionKey];
            return value != null && Convert.ToBoolean(value);
        }

        public static void EnableContingency(HttpSessionStateBase session)
        {
            if (session != null)
                session[ContingencySessionKey] = true;
        }

        public static void ClearContingency(HttpSessionStateBase session)
        {
            if (session != null)
                session.Remove(ContingencySessionKey);
        }
    }
}
