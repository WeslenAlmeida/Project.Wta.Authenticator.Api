using System.Security.Principal;
using System.Security.Claims;
using Domain.Security;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Domain.Interfaces.v1;

namespace Domain.Commands.v1
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, object>
    {
        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IUserRepository _user; 

        public GenerateTokenCommandHandler(SigningConfiguration signingConfiguration,
                                           TokenConfiguration tokenConfiguration,
                                           IUserRepository userRepository)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfiguration = tokenConfiguration;
            _user = userRepository;
        }

        public async Task<object> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            //var data = _user.CheckUser(request.Email!);

            var data = "teste";

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
                new GenericIdentity(request.Email!),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                    new Claim(JwtRegisteredClaimNames.UniqueName, request.Email!)
                }
            );

            var createDate = DateTime.Now;
            var expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
            var token = CreateToken(identity, createDate, expirationDate);
            
            return SuccessObject(token);
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