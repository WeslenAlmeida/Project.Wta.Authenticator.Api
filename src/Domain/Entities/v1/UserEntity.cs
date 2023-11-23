using Domain.Fixed;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities.v1 
{
    public class UserEntity 
    {
        [BsonElement("id")]
        public Guid Id { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }

        [BsonElement("claim")]
        public string? Claim { get; set; }
    }
}