using System.Security.Principal;
using System.Security.Claims;
using Domain.Security;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Commands.v1
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, object>
    {
        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;

        public GenerateTokenCommandHandler(SigningConfiguration signingConfiguration,
                                           TokenConfiguration tokenConfiguration)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
        }

        public async Task<object> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            //verificar login e senha no banco
            var data = "dados do banco";

            if(data is null)
            {
                return new 
                {
                    authenticated = false,
                    message = "Error !!!"
                };
            }

            var identity = new ClaimsIdentity
            (
                new GenericIdentity(request.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                    new Claim(JwtRegisteredClaimNames.UniqueName, request.Email)
                }
            );

            var createDate = DateTime.Now;

            var expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

            var token = CreateToken(identity, createDate, expirationDate);

            

            
        }

        private string CreateToken(ClaimsIdentity claimsIdentity, DateTime createDate, DateTime expirationDate)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken
            (
                new SecurityTokenDescriptor()
                {
                    Issuer = _tokenConfiguration.Issuer,
                    Audience = _tokenConfiguration.Audience,
                    SigningCredentials = _signingConfiguration.SigningCredentials,
                    Subject = claimsIdentity,
                    NotBefore = createDate,
                    Expires = expirationDate,
                }
            );

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private object SuccessObject(string token)
        {
            return new 
                {
                    authenticated = true,
                    message = "Success !!!",
                    accessToken = token
                };
        }
    }
}