using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class Comment : IBaseEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}