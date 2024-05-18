using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class ReviewEntity : IBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = null!;
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Text { get; set; } = null!;
    public virtual List<int> LikeUserIds { get; set; } = [];
    public virtual List<int> DislikeUserIds { get; set; } = [];
    public virtual List<Comment> Comments { get; set; } = [];
    [BsonIgnore] 
    public virtual Book Book { get; set; } = null!;
    [BsonIgnore] 
    public virtual User User { get; set; } = null!;
}