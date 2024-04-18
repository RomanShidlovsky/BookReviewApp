using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class ReviewEntity : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string? Text { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public virtual List<Comment> Comments { get; set; } = [];
    [BsonIgnore] 
    public virtual Book Book { get; set; } = null!;
    [BsonIgnore] 
    public virtual User User { get; set; } = null!;
}