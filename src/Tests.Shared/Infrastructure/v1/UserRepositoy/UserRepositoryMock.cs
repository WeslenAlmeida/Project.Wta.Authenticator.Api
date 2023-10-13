using Domain.Interfaces.v1;
using NSubstitute;
using NUnit.Framework;
using Tests.Shared.Mock.v1;

namespace Tests.Shared.Infrastructure.v1.UserRepositoy
{
    public class UserRepositoryMock 
    {
        readonly IUserRepository mock = Substitute.For<IUserRepository>();  

        [SetUp]
        public IUserRepository SetUpSuccess()
        {
            mock.CheckUser("teste").ReturnsForAnyArgs(true);
            return mock;
        }

        [SetUp]
        public IUserRepository SetUpFailed()
        {
            mock.CheckUser("teste").ReturnsForAnyArgs(false);
            return mock;    
        }
    }
}