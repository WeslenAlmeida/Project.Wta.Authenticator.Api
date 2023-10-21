using System.Security.Principal;
using System.Security.Claims;
using Domain.Security;
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

            var email = await _redis.GetAsync(request.Email!);

            if(string.IsNullOrEmpty(email))
            {
                if(await _user.CheckUser(request.Email!))
                {
                    await _redis.SetAsync(request.Email!, request.Email!);
                    email = request.Email;
                }    
                else
                    throw new UserNotFoundException();    
            }

            var identity = new ClaimsIdentity
            (
                new GenericIdentity(email!),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                    new Claim(JwtRegisteredClaimNames.UniqueName, email!)
                }
            );

            var createDate = DateTime.UtcNow;
            var key = Encoding.ASCII.GetBytes(_key);
            var expirationDate = createDate.AddSeconds(_expirationTime);
            var token = CreateToken(identity, createDate, expirationDate, key);

            await _redis.SetAsync(token, JsonConvert.SerializeObject(new TokenData(request.Email!, expirationDate)));

            _logger.LogInformation("End GenerateTokenCommandHandler");
            
            return SuccessObject(token);
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

        private static GenerateTokenCommandResponse SuccessObject(string token)
        {
            return new GenerateTokenCommandResponse()
                {
                    Authenticated = true,
                    Message = "Success !!!",
                    AccessToken = token
                };
        }
    }
}