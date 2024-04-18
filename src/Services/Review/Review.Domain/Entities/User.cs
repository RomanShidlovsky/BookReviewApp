using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class User : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string? ImageUrl { get; set; }
}