using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;

namespace Linx.Security.Core.Authentication
{
    public class CustomAuthenticationOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "custom auth";
        public string Scheme => DefaultScheme;
        public StringValues AuthKey { get; set; }
    }
}
