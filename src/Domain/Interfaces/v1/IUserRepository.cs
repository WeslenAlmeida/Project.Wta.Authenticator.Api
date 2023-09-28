using Domain.Models.v1;

namespace Domain.Interfaces.v1
{
    public interface IUserRepository
    {
        public Task<UserEntity> CheckUser (string email);
    }
}