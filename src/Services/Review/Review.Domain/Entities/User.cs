using Review.Domain.Interfaces;

namespace Review.Domain.Entities;

public class User : IBaseEntity
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string? ImageUrl { get; set; }
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}