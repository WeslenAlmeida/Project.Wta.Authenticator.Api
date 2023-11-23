using Domain.Entities.v1;

namespace Domain.Interfaces.v1
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUser (string email, string password);
    }
}