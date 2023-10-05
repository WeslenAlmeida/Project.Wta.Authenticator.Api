using CrossCutting.Configuration;
using Domain.Interfaces.v1;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Data.v1.Mongo
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserEntity> _userCollection;
        public UserRepository()
        {
            var mongoClient = new MongoClient(AppSettings.MongoSettings.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(AppSettings.MongoSettings.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<UserEntity>("user");
        }

        public async Task<UserEntity> CheckUser(string email)
        {
            var response = await _userCollection.FindAsync(x => x.Email == email);

            return response.FirstOrDefault();
        }
    }
}