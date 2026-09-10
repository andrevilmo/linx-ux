using System;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Linx.Ux.AuthDesktopPoc
{
    public static class LinxDesktopSso
    {
        public static async Task<string> ObterNomeAutenticacaoAsync(
            string clientId, string tenantId, string redirectUri)
        {
            IPublicClientApplication app = PublicClientApplicationBuilder
                .Create(clientId)
                .WithAuthority("https://login.microsoftonline.com/" + tenantId)
                .WithRedirectUri(redirectUri)
                .Build();

            AuthenticationResult result = await app
                .AcquireTokenInteractive(new[] { "User.Read" })
                .WithPrompt(Prompt.ForceLogin)
                .ExecuteAsync()
                .ConfigureAwait(false);

            string upn = result.Account != null ? result.Account.Username : null;
            if (string.IsNullOrWhiteSpace(upn))
                throw new InvalidOperationException("Azure não devolveu UPN.");

            int at = upn.IndexOf('@');
            return at > 0 ? upn.Substring(0, at) : upn;
        }
    }
}
