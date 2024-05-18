using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Review.Domain.Entities;

public class Dislike
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    [BsonRepresentation(BsonType.ObjectId)]
    public string ReviewId { get; set; } = null!;
    public int UserId { get; set; }
}