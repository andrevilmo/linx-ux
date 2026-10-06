using System;
using System.Threading.Tasks;

namespace Linx.Portal.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResultModel> LoginAsync();
        Task<AuthenticationResultModel> LoginSilentAsync();
        Task<AuthenticationResultModel> LoginForceAsync();
        Task LogoutAsync();
        bool IsAuthenticated { get; }
        AuthenticatedUser CurrentUser { get; }

        /// <summary>
        /// Web adaptation: build Azure authorize URL (prompt=login when forceLogin).
        /// </summary>
        Task<Uri> GetAuthorizationUrlAsync(string state, bool forceLogin = true);

        /// <summary>
        /// Web adaptation: exchange authorization code for tokens.
        /// </summary>
        Task<AuthenticationResultModel> CompleteAuthorizationCodeAsync(string code);
    }
}
