using Domain.Entities;

namespace Domain.Interfaces.v1
{
    public interface IUserRepository
    {
        public Task<UserEntity> CheckUser (string email);
    }
}