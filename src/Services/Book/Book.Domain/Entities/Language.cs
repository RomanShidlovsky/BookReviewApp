using Shared;

namespace Book.Domain.Entities;

public class Language : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Book> Books { get; set; } = [];
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}