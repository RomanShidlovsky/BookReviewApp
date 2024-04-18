using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class User : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    public int Id { get; set; }

    public string UserName { get; set; } = null!;
    public string? ImageUrl { get; set; }
}