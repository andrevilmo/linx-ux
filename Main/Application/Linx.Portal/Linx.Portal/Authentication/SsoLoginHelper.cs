using System;
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
            var msal = ex as MsalClientException;
            if (msal == null)
                msal = ex.InnerException as MsalClientException;

            if (msal != null)
            {
                if (string.Equals(msal.ErrorCode, "authentication_canceled", StringComparison.OrdinalIgnoreCase))
                    return "O usuário abortou o processo de autenticação.";

                if (string.Equals(msal.ErrorCode, "authentication_ui_failed", StringComparison.OrdinalIgnoreCase))
                {
                    suggestContingency = true;
                    return "Não foi possível estabelecer conexão com o servidor.";
                }
            }

            var msalSvc = ex as MsalServiceException;
            if (msalSvc == null)
                msalSvc = ex.InnerException as MsalServiceException;
            if (msalSvc != null)
            {
                suggestContingency = true;
                return "Não foi possível estabelecer conexão com o servidor.";
            }

            return "Não foi possível realizar autenticação.";
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
