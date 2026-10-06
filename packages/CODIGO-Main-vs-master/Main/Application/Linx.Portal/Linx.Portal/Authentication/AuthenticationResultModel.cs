using System;

namespace Linx.Portal.Authentication
{
    public class AuthenticationResultModel
    {
        public string AccessToken { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public AuthenticatedUser User { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Message { get; set; }
    }
}
