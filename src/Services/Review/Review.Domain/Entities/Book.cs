using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class Book : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    public int AverageRating { get; set; }
    public string? ImageUrl { get; set; }
    public virtual List<ReviewEntity> Reviews { get; set; } = [];
}