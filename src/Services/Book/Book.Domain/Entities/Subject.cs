using Shared.Interfaces;
using IBaseEntity = Book.Domain.Interfaces.IBaseEntity;

namespace Book.Domain.Entities;

public class Subject : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<Book> Books { get; set; } = [];
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}