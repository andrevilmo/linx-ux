using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Linx.Portal.Authentication
{
    /// <summary>
    /// MSAL authentication service (OmniPOS pattern).
    /// Portal IIS uses confidential client + authorization-code redirect (ForceLogin).
    /// When ClientSecret is empty, falls back to public client interactive APIs (desktop-style).
    /// </summary>
    public class MsalAuthenticationService : IAuthenticationService
    {
        private readonly AzureAdOptions _options;
        private readonly ITokenCacheStore _cache;
        private readonly IConfidentialClientApplication _confidentialApp;
        private readonly IPublicClientApplication _publicApp;
        private readonly bool _useConfidential;

        public bool IsAuthenticated { get { return CurrentUser != null; } }
        public AuthenticatedUser CurrentUser { get; private set; }

        public MsalAuthenticationService(AzureAdOptions options, ITokenCacheStore cacheStore)
        {
            if (options == null)
                throw new ArgumentNullException("options");
            if (string.IsNullOrWhiteSpace(options.ClientId))
                throw new ArgumentException("SSO_CLIENT_ID is required.", "options");
            if (string.IsNullOrWhiteSpace(options.TenantId))
                throw new ArgumentException("SSO_TENANT_ID is required.", "options");
            if (options.Scopes == null || options.Scopes.Length == 0)
                options.Scopes = new[] { "User.Read" };
            if (string.IsNullOrWhiteSpace(options.RedirectUri))
                options.RedirectUri = "https://localhost";

            _options = options;
            _cache = cacheStore;
            _useConfidential = !string.IsNullOrWhiteSpace(options.ClientSecret);

            if (_useConfidential)
            {
                _confidentialApp = ConfidentialClientApplicationBuilder
                    .Create(_options.ClientId)
                    .WithClientSecret(_options.ClientSecret)
                    .WithAuthority(_options.Authority)
                    .WithRedirectUri(_options.RedirectUri)
                    .Build();

                if (_cache != null)
                    _cache.RegisterCache(_confidentialApp.UserTokenCache);
            }
            else
            {
                _publicApp = PublicClientApplicationBuilder
                    .Create(_options.ClientId)
                    .WithAuthority(_options.Authority)
                    .WithRedirectUri(_options.RedirectUri)
                    .Build();

                if (_cache != null)
                    _cache.RegisterCache(_publicApp.UserTokenCache);
            }
        }

        public async Task<AuthenticationResultModel> LoginAsync()
        {
            EnsurePublicClient("LoginAsync");
            var result = await _publicApp
                .AcquireTokenInteractive(_options.Scopes)
                .ExecuteAsync()
                .ConfigureAwait(false);

            return MapResult(result);
        }

        public async Task<AuthenticationResultModel> LoginSilentAsync()
        {
            if (_useConfidential)
            {
                var account = (await _confidentialApp.GetAccountsAsync().ConfigureAwait(false)).FirstOrDefault();
                if (account == null)
                    return null;

                var result = await _confidentialApp
                    .AcquireTokenSilent(_options.Scopes, account)
                    .ExecuteAsync()
                    .ConfigureAwait(false);

                return MapResult(result);
            }

            var pubAccount = (await _publicApp.GetAccountsAsync().ConfigureAwait(false)).FirstOrDefault();
            if (pubAccount == null)
                return null;

            var pubResult = await _publicApp
                .AcquireTokenSilent(_options.Scopes, pubAccount)
                .ExecuteAsync()
                .ConfigureAwait(false);

            return MapResult(pubResult);
        }

        /// <summary>
        /// OmniPOS desktop flow: always forces Azure login UI.
        /// On Portal (confidential/web) use GetAuthorizationUrlAsync + CompleteAuthorizationCodeAsync instead.
        /// </summary>
        public async Task<AuthenticationResultModel> LoginForceAsync()
        {
            EnsurePublicClient("LoginForceAsync");
            var result = await _publicApp
                .AcquireTokenInteractive(_options.Scopes)
                .WithPrompt(Prompt.ForceLogin)
                .ExecuteAsync()
                .ConfigureAwait(false);

            return MapResult(result);
        }

        public async Task<Uri> GetAuthorizationUrlAsync(string state, bool forceLogin = true)
        {
            // Portal IIS uses confidential client only. IPublicClientApplication in MSAL 4.54
            // does not expose GetAuthorizationRequestUrl (desktop interactive APIs do).
            if (!_useConfidential || _confidentialApp == null)
            {
                throw new InvalidOperationException(
                    "SSO_CLIENT_SECRET is required for Portal web SSO (authorization-code + ForceLogin). " +
                    "Register the Portal as a Web app in Azure AD and set the client secret.");
            }

            var builder = _confidentialApp
                .GetAuthorizationRequestUrl(_options.Scopes)
                .WithRedirectUri(_options.RedirectUri);

            if (!string.IsNullOrEmpty(state))
                builder = builder.WithExtraQueryParameters("state=" + Uri.EscapeDataString(state));

            if (forceLogin)
                builder = builder.WithPrompt(Prompt.ForceLogin);

            return await builder.ExecuteAsync().ConfigureAwait(false);
        }

        public async Task<AuthenticationResultModel> CompleteAuthorizationCodeAsync(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Authorization code is required.", "code");

            if (_useConfidential)
            {
                var result = await _confidentialApp
                    .AcquireTokenByAuthorizationCode(_options.Scopes, code)
                    .ExecuteAsync()
                    .ConfigureAwait(false);

                return MapResult(result);
            }

            // Public client authorization-code is not supported the same way — require secret for Portal web.
            throw new InvalidOperationException(
                "SSO_CLIENT_SECRET is required for Portal web authorization-code flow. " +
                "Register the Portal as a Web app in Azure AD and set the client secret.");
        }

        public async Task LogoutAsync()
        {
            if (_useConfidential)
            {
                foreach (var account in await _confidentialApp.GetAccountsAsync().ConfigureAwait(false))
                    await _confidentialApp.RemoveAsync(account).ConfigureAwait(false);
            }
            else if (_publicApp != null)
            {
                foreach (var account in await _publicApp.GetAccountsAsync().ConfigureAwait(false))
                    await _publicApp.RemoveAsync(account).ConfigureAwait(false);
            }

            if (_cache != null)
                _cache.Clear();

            CurrentUser = null;
        }

        private void EnsurePublicClient(string methodName)
        {
            if (_useConfidential || _publicApp == null)
            {
                throw new InvalidOperationException(
                    methodName + " requires a public client (desktop). " +
                    "For Portal web use GetAuthorizationUrlAsync / CompleteAuthorizationCodeAsync with SSO_CLIENT_SECRET.");
            }
        }

        private AuthenticationResultModel MapResult(AuthenticationResult result)
        {
            CurrentUser = new AuthenticatedUser
            {
                Username = result.Account != null ? result.Account.Username : null,
                Name = result.ClaimsPrincipal != null && result.ClaimsPrincipal.Identity != null
                    ? result.ClaimsPrincipal.Identity.Name
                    : null,
                TenantId = result.TenantId,
                ObjectId = result.UniqueId
            };

            return new AuthenticationResultModel
            {
                AccessToken = result.AccessToken,
                ExpiresOn = result.ExpiresOn,
                User = CurrentUser,
                IsAuthenticated = true
            };
        }
    }
}
