using Newtonsoft.Json;
using Shared.Interfaces;
using IBaseEntity = Book.Domain.Interfaces.IBaseEntity;

namespace Book.Domain.Entities;

public class Language : IBaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual List<Book> Books { get; set; } = [];
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}