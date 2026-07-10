using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Linx.Security.Core.Authentication
{
    public class JwtConfig
    {
        public TokenConfigurations TokenConfigurations { get; set; }
        public SigningConfigurations SigningConfigurations { get; set; }
    }

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }

    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfigurations(string key)
        {
            Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256);
        }
    }
}
