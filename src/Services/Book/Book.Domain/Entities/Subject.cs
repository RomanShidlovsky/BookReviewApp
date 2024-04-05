using Shared.Interfaces;

namespace Book.Domain.Entities;

public class Subject : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; } = [];
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}