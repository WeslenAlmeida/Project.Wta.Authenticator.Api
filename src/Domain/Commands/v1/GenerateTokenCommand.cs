using MediatR;

namespace Domain.Commands.v1
{
    public class GenerateTokenCommand : IRequest<object>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}