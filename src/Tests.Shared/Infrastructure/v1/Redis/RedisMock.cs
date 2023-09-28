using Domain.Interfaces.v1;
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
    }
}