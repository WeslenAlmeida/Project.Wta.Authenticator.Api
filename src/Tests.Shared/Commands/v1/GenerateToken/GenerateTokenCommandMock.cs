using CrossCutting.Configuration;
using Domain.Commands.v1.GenerateToken;

namespace Tests.Shared.Commands.v1.GenerateToken
{
    public static class GenerateTokenCommandMock
    {
        public static GenerateTokenCommand GetDefault() =>
            new GenerateTokenCommand
            {
                Email = "teste@teste.com",
                AccessToken = AppSettings.AccessToken.Id
            };

        public static GenerateTokenCommand GetFailedEmail() =>
            new GenerateTokenCommand
            {
                Email = "teste1@teste.com",
                AccessToken = AppSettings.AccessToken.Id
            };

        public static GenerateTokenCommand GetFailedValidation() =>
            new GenerateTokenCommand
            {
                Email = "teste@teste.com",
                AccessToken ="123"
            };
    }
}