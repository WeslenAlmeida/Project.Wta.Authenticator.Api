using System.Security.Principal;
using System.Security.Claims;
using Domain.Security;
using MedeiatR;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Domain.Interfaces.v1;
using CrossCutting.Exception.CustomExceptions;
using CrossCutting.Configuration;

namespace Domain.Commands.v1.GenerateToken
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, object>
    {
        private readonly SigningConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfiguration;
        private readonly IUserRepository _user; 
        private readonly IRedisService _redis;
        private readonly string? _apikey;

        public GenerateTokenCommandHandler(IUserRepository userRepository, IRedisService redis)
        {
            _signingConfiguration = new SigningConfiguration();
            _tokenConfiguration = new TokenConfiguration();
            _user = userRepository;
            _redis = redis;
            _apikey = AppSettings.ApiKey.Id;
        }

        public async Task<object> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _user.CheckUser(request.Email!)?? throw new UserNotFoundException();

            if(request.ApiKey !=_apikey) throw new UnauthorizedException();

            var identity = new ClaimsIdentity
            (
                new GenericIdentity(user.Email!),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email!)
                }
            );

            var createDate = DateTime.Now;
            var expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);
            var token = CreateToken(identity, createDate, expirationDate);

            await _redis.SetAsync(user.Id, token);
            
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

        private static object SuccessObject(string token)
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