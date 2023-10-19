using CrossCutting.Exception.CustomExceptions;
using Domain.Queries.v1.GetToken;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Tests.Shared.Infrastructure.v1.Redis;
using Tests.Shared.Queries.v1.GetTokenInfo;

namespace Tests.Unity.Domain.CommandsHandler.v1.GenerateToken
{
    [TestFixture]
    public class GetTokenInfoQueryHandlerTest 
    {
        private static GetTokenInfoQueryHandler GetContext()
        {
            return new GetTokenInfoQueryHandler(
                new RedisMock().SetUpSuccessTokenInfo(),
                new Mock<ILogger<GetTokenInfoQueryHandler>>().Object
            );
        }

        private static GetTokenInfoQueryHandler GetInvalidContext()
        {
            return new GetTokenInfoQueryHandler(
                new RedisMock().SetUpFailed(),
                new Mock<ILogger<GetTokenInfoQueryHandler>>().Object
            );
        }

        [Test]
        public void Get_info_token_when_Sucess()
        {
            Assert.DoesNotThrowAsync(async () => {await GetContext().Handle(GetTokenInfoQueryMock.GetDefault(), CancellationToken.None);});
        }

        [Test]
        public void Get_info_token_when_Failed()
        {
            Assert.ThrowsAsync<TokenNotFoundException>(async () => {await GetInvalidContext().Handle(GetTokenInfoQueryMock.GetDefault(), CancellationToken.None);});
        }
    }
}