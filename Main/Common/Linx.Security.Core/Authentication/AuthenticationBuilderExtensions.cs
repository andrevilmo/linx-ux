using Microsoft.AspNetCore.Authentication;
using System;

namespace Linx.Security.Core.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        // Custom authentication extension method
        public static AuthenticationBuilder AddCustomAuth(this AuthenticationBuilder builder, Action<CustomAuthenticationOptions> configureOptions)
        {
            // Add custom authentication scheme with custom options and custom handler
            return builder.AddScheme<CustomAuthenticationOptions, CustomAuthenticationHandler>(CustomAuthenticationOptions.DefaultScheme, configureOptions);
        }
    }
}
