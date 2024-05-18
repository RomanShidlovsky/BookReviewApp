using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class Comment
{
    public int UserId { get; set; }
    public string Text { get; set; } = null!;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}