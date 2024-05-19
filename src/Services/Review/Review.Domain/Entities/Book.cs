using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class Book : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }
    public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public double AverageRating { get; set; }
    public double AverageCriticRating { get; set; }
    public string? ImageUrl { get; set; }
    public virtual List<ReviewEntity> Reviews { get; set; } = [];
}