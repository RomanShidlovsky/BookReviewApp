using Shared.Interfaces;
using IBaseEntity = Book.Domain.Interfaces.IBaseEntity;

namespace Book.Domain.Entities;

public class Book : IBaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int EditionCount { get; set; }
    public int PublicationYear { get; set; }
    public double AverageRating { get; set; }
    public double AverageCriticRating { get; set; }
    public string? ImageUrl { get; set; }
    public string? OpenLibraryKey { get; set; }
    public virtual List<Subject> Subjects { get; set; } = [];
    public virtual List<Author> Authors { get; set; } = [];
    public virtual List<Language> Languages { get; set; } = [];
    public DateTimeOffset DateCreated { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
    public DateTimeOffset? DateDeleted { get; set; }
}