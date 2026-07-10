using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Linx.Security.Core.Authentication
{
    public static class JwtToken
    {
        private static JwtConfig _jwtConfig { get; set; }

        static JwtToken()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();

            var section = Configuration.GetSection("TokenConfigurations");

            _jwtConfig = new JwtConfig
            {
                TokenConfigurations = new TokenConfigurations() { Audience = section["Audience"], Issuer = section["Issuer"], Seconds = Convert.ToInt32(section["Seconds"]) },
                SigningConfigurations = new SigningConfigurations(section["Hash"])
            };

        }

        public static IEnumerable<Claim> ValidateToken(string token)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _jwtConfig.SigningConfigurations.Key,
                ValidateIssuerSigningKey = true,
                ValidAudience = _jwtConfig.TokenConfigurations.Audience,
                ValidateAudience = true,
                ValidIssuer = _jwtConfig.TokenConfigurations.Issuer,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            handler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
            return (handler.ReadToken(token) as JwtSecurityToken).Claims;
        }

        public static string GenerateToken()
        {
            ClaimsIdentity identity = new ClaimsIdentity(
            new GenericIdentity("sergio.oliveira", "Login"), new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString())
                        });

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao + TimeSpan.FromSeconds(_jwtConfig.TokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _jwtConfig.TokenConfigurations.Issuer,
                Audience = _jwtConfig.TokenConfigurations.Audience,
                SigningCredentials = _jwtConfig.SigningConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });

            return handler.WriteToken(securityToken);
        }
    }
}
