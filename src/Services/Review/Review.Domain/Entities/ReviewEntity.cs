using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class ReviewEntity : IBaseEntity
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public string Text { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    public Comment[] Comments { get; set; }
    public Book Book { get; set; }
    public User User { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}