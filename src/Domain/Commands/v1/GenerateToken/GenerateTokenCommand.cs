using Domain.Interfaces.v1;
namespace Domain.Commands.v1.GenerateToken
{
    public class GenerateTokenCommand : ITokenRequest<GenerateTokenCommandResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; } 
    }
}