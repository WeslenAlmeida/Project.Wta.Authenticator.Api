using Domain.Interfaces.v1;
using Domain.Queries.v1.GetToken;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Shared.Infrastructure.v1.Redis
{
    public class RedisMock
    {
       readonly  IRedisService mock = Substitute.For<IRedisService>();

        [SetUp]
        public IRedisService SetUpSuccess()
        {
            mock.GetAsync("teste").ReturnsForAnyArgs("teste");
            return mock;
        }

        [SetUp]
        public IRedisService SetUpSuccessTokenInfo()
        {
            mock.GetAsync("teste").ReturnsForAnyArgs(JsonConvert.SerializeObject(new {Email = "teste", ExpirationDate = DateTime.UtcNow}));
            return mock;
        }


        [SetUp]
         public IRedisService SetUpFailed()
        {
            mock.GetAsync("teste").ReturnsForAnyArgs("");
            return mock;
        }
    }
}