using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Linx.Portal.Authentication;
using Linx.Tools;

namespace Linx.Portal
{
    public class Utils
    {
        private static Hashtable GetPortalSettings()
        {
            Hashtable config = System.Configuration.ConfigurationManager.GetSection("PortalSettings") as Hashtable;

            if (config.IsNullOrEmpty())
                throw new Exception("Configurações do Portal não foram encontradas.".Translate());

            return config;
        }

        private static string GetSetting(string key, string defaultValue = null)
        {
            Hashtable config = GetPortalSettings();
            var value = config[key];
            if (value.IsNullOrEmpty())
                return defaultValue;
            return value.ToString();
        }

        private static bool GetBoolSetting(string key, bool defaultValue)
        {
            var raw = GetSetting(key, null);
            if (raw.IsNullOrEmpty())
                return defaultValue;

            bool parsed;
            if (bool.TryParse(raw, out parsed))
                return parsed;

            if (string.Equals(raw, "1", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(raw, "yes", StringComparison.OrdinalIgnoreCase))
                return true;

            if (string.Equals(raw, "0", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(raw, "no", StringComparison.OrdinalIgnoreCase))
                return false;

            return defaultValue;
        }

        public static string GetServiceUrl()
        {
            string url = GetSetting("authorizationServiceAddress");
            if (url.IsNullOrEmpty())
                throw new Exception("Configurações do Portal não foram encontradas.".Translate());
            return url + (url.EndsWith("/") ? string.Empty : "/");
        }

        public static string GetPortalUrl()
        {
            string url = GetSetting("PortalUrl");
            if (url.IsNullOrEmpty())
                throw new Exception("Configurações do Portal não foram encontradas.".Translate());
            return url + (url.EndsWith("/") ? string.Empty : "/");
        }

        public static bool GetRecoverPasswordOption()
        {
            return GetBoolSetting("ShowRecoverPasswordOption", true);
        }

        public static bool GetListEnvironmentOptionOnLogin()
        {
            return GetBoolSetting("ShowListEnvironmentOptionOnLogin", true);
        }

        /// <summary>OmniPOS: SSO_HABILITA_AUTENTICACAO (default false).</summary>
        public static bool IsSsoEnabled()
        {
            return GetBoolSetting("SSO_HABILITA_AUTENTICACAO", false);
        }

        /// <summary>OmniPOS: SSO_PERMITE_OFFLINE — allows local user/password fallback.</summary>
        public static bool IsSsoOfflineFallbackAllowed()
        {
            return GetBoolSetting("SSO_PERMITE_OFFLINE", false);
        }

        public static int GetSsoTimeoutSeconds()
        {
            var raw = GetSetting("SSO_TIMEOUT_RESPOSTA", "120");
            int seconds;
            return int.TryParse(raw, out seconds) ? seconds : 120;
        }

        public static AzureAdOptions GetAzureAdOptions()
        {
            string redirect = GetSetting("SSO_REDIRECT_URI", null);
            if (redirect.IsNullOrEmpty())
            {
                // Default callback on this Portal instance.
                try
                {
                    redirect = GetPortalUrl().TrimEnd('/') + "/Account/SsoCallback";
                }
                catch
                {
                    redirect = "https://localhost";
                }
            }

            string scopesRaw = GetSetting("SSO_SCOPES", "User.Read");
            string[] scopes = (scopesRaw ?? "User.Read")
                .Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return new AzureAdOptions
            {
                ClientId = GetSetting("SSO_CLIENT_ID", null),
                TenantId = GetSetting("SSO_TENANT_ID", null),
                ClientSecret = GetSetting("SSO_CLIENT_SECRET", null),
                RedirectUri = redirect,
                Scopes = scopes.Length > 0 ? scopes : new[] { "User.Read" }
            };
        }
    }

}