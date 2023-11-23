using FluentValidation;

namespace Domain.Commands.v1.GenerateToken
{
    public class GenerateTokenCommandValidator : AbstractValidator<GenerateTokenCommand>
    {
        public GenerateTokenCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithMessage("Type email is invalid!");
            RuleFor(x => x.Password).NotEmpty().NotNull().Length(6,20)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z])")
                .WithMessage("Type password is invalid!");
        }
    }
}