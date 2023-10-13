using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities 
{
    public class UserEntity 
    {
         [BsonElement("id")]
        public Guid Id { get; set; }

         [BsonElement("email")]
        public string? Email { get; set; }
    }
}