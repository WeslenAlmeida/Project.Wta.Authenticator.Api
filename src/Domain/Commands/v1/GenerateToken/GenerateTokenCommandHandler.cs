using System.Security.Principal;
using System.Security.Claims;
using MediatR;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Domain.Interfaces.v1;
using CrossCutting.Exception.CustomExceptions;
using Newtonsoft.Json;
using Domain.Models.v1;
using Microsoft.Extensions.Logging;
using System.Text;
using CrossCutting.Configuration;
using Domain.Security;
using Domain.Entities.v1;
using Domain.Fixed;

namespace Domain.Commands.v1.GenerateToken
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, GenerateTokenCommandResponse>
    {
        private readonly IUserRepository _user; 
        private readonly IRedisService _redis;
        private readonly string _key;
        private readonly string _audience;
        private readonly string _issuer;
        private readonly int _expirationTime;
        private readonly ILogger<GenerateTokenCommandHandler> _logger;

        public GenerateTokenCommandHandler(IUserRepository userRepository, IRedisService redis, ILogger<GenerateTokenCommandHandler> logger)
        {
            _logger = logger;
            _user = userRepository;
            _redis = redis;
            _key = AppSettings.TokenConfiguration.Key!;
            _audience = AppSettings.TokenConfiguration.Audience!;
            _issuer = AppSettings.TokenConfiguration.Issuer!;
            _expirationTime = int.Parse(AppSettings.TokenConfiguration.Seconds!);
        }

        public async Task<GenerateTokenCommandResponse> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start GenerateTokenCommandHandler");

            var user = await _user.GetUser(request.Email!, Cryptography.HashMd5(request.Password!)) ?? throw new UserNotFoundException();

            var createDate = DateTime.UtcNow;
            var expirationDate = createDate.AddSeconds(_expirationTime);
            
            var token = CreateToken(
                GetClaimsIdentity(user),
                createDate,
                expirationDate,
                Encoding.ASCII.GetBytes(_key));

            await _redis.SetAsync(token, JsonConvert.SerializeObject(new TokenData(request.Email!, expirationDate)));

            _logger.LogInformation("End GenerateTokenCommandHandler");
            
            return new GenerateTokenCommandResponse()
                {
                    Authenticated = true,
                    Message = "Success !!!",
                    AccessToken = token
                };
        }

        private string CreateToken(ClaimsIdentity claimsIdentity, DateTime createDate, DateTime expirationDate, byte[] key)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken
            (
                new SecurityTokenDescriptor()
                {
                    Issuer = _issuer,
                    Audience = _audience,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                    Subject = claimsIdentity,
                    NotBefore = createDate,
                    Expires = expirationDate,
                }
            );

            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }

        private ClaimsIdentity GetClaimsIdentity(UserEntity user)
        {
            return new ClaimsIdentity
            (
                new GenericIdentity(user.Email!),
                new[]
                {
                    new Claim(ClaimTypes.Name, user.Email!),  
                    new Claim(ClaimTypes.Role, user.Claim!)
                }
            );
        }
    }
}