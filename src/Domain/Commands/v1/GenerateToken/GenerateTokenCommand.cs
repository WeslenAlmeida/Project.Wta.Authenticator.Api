using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain.Commands.v1.GenerateToken
{
    public class GenerateTokenCommand : IRequest<GenerateTokenCommandResponse>
    {
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Input invalid email!")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}