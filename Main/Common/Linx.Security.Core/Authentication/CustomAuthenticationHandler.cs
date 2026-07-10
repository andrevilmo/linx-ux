using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Linx.Security.Core.Authentication
{
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {

                // Get Authorization header value
                if (Request.Headers.TryGetValue(HeaderNames.Authorization, out var authorization))
                {
                    if (authorization.ToString().StartsWith("Bearer"))
                    {
                        IEnumerable<Claim> claims = JwtToken.ValidateToken(authorization.ToString().Replace("Bearer ", ""));
                    }
                    else if (authorization.ToString().StartsWith("Basic"))
                    {
                        var param = authorization.ToString().Replace("Basic ", "");
                        var tokens = Encoding.Default.GetString(Convert.FromBase64String(param)).Split(':');
                    }
                    else
                    {
                        throw new Exception("Chave de autenticação inválida.");
                    }
                }
                else if (!Request.Headers.TryGetValue("CurrentCompany", out var authorizationC))
                {
                    throw new Exception("Headers de autenticação inválidos.");
                }

                // Create authenticated user
                var identities = new List<ClaimsIdentity> { new ClaimsIdentity("custom auth type") };
                var ticket = new AuthenticationTicket(principal: new ClaimsPrincipal(identities), authenticationScheme: Options.Scheme);


                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
            catch (Exception oException)
            {
                return Task.FromResult(AuthenticateResult.Fail(oException.Message));
            }
        }
    }
}
