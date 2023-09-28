using MediatR;

namespace Domain.Commands.v1.GenerateToken
{
    public class GenerateTokenCommand : IRequest<object>
    {
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
    }
}