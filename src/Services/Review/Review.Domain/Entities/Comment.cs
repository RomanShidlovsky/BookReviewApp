using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class Comment : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}