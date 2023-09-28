using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Security
{
    public class SigningConfiguration
    {
        public SecurityKey? SecurityKey { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
        public SigningConfiguration()
        {
            using (var provider = new RSACryptoServiceProvider(2048)) 
            {
                SecurityKey = new RsaSecurityKey(provider.ExportParameters(true));
            } 

            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}