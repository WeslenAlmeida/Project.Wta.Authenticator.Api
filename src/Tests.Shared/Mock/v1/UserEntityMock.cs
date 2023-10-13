using Domain.Entities;

namespace Tests.Shared.Mock.v1
{
    public static class UserEntityMock
    {
        public static UserEntity GetDefault() =>
            new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "test@test.com"
            };

        public static UserEntity GetFailed() =>
            null!;
    }
}