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
                ApiKey = AppSettings.ApiKey.Id
            };

        public static GenerateTokenCommand GetFailedEmail() =>
            new GenerateTokenCommand
            {
                Email = "teste1@teste.com",
                ApiKey = AppSettings.ApiKey.Id
            };

        public static GenerateTokenCommand GetFailedValidation() =>
            new GenerateTokenCommand
            {
                Email = "teste@teste.com",
                ApiKey ="123"
            };
    }
}