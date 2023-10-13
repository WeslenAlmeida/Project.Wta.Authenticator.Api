using CrossCutting.Exception.CustomExceptions;
using Domain.Commands.v1.GenerateToken;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Tests.Shared.Commands.v1.GenerateToken;
using Tests.Shared.Infrastructure.v1.Redis;
using Tests.Shared.Infrastructure.v1.UserRepositoy;

namespace Tests.Unity.Domain.CommandsHandler.v1.GenerateToken
{
    [TestFixture]
    public class GenerateTokenCommandHandlerTest 
    {
        private static GenerateTokenCommandHandler GetContext()
        {
            return new GenerateTokenCommandHandler(
                new UserRepositoryMock().SetUpSuccess(),
                new RedisMock().SetUpSuccess(),
                new Mock<Logger<GenerateTokenCommandHandler>>().Object
            );
        }

        private static GenerateTokenCommandHandler GetUserInvalidContext()
        {
            return new GenerateTokenCommandHandler(
                new UserRepositoryMock().SetUpFailed(),
                new RedisMock().SetUpFailed(),
                new Mock<Logger<GenerateTokenCommandHandler>>().Object
            );
        }

        [Test]
        public void Can_generate_token_when_Sucess()
        {
            Assert.DoesNotThrowAsync(async () => {await GetContext().Handle(GenerateTokenCommandMock.GetDefault(), CancellationToken.None);});
        }

        [Test]
        public void Can_generate_token_when_Validate_Email_Failed()
        {
            Assert.ThrowsAsync<UserNotFoundException>(async () => {await GetUserInvalidContext().Handle(GenerateTokenCommandMock.GetFailedEmail(), CancellationToken.None);});
        }
    }
}