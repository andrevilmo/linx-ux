using System;

namespace Linx.Portal.Authentication
{
    /// <summary>
    /// Azure AD / Entra ID options (OmniPOS SSO guide).
    /// </summary>
    public class AzureAdOptions
    {
        public string ClientId { get; set; }
        public string TenantId { get; set; }
        public string RedirectUri { get; set; }
        public string[] Scopes { get; set; }
        /// <summary>
        /// Required for Portal web (confidential client / authorization code).
        /// OmniPOS desktop uses a public client and does not need a secret.
        /// </summary>
        public string ClientSecret { get; set; }

        public string Authority
        {
            get { return string.Format("https://login.microsoftonline.com/{0}", TenantId); }
        }
    }
}
