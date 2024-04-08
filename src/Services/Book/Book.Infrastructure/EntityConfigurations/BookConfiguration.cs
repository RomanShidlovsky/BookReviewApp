using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BookEntity =  Book.Domain.Entities.Book;

namespace Book.Infrastructure.EntityConfigurations;

public class BookConfiguration : BaseConfiguration<BookEntity>
{
    public override void Configure(EntityTypeBuilder<BookEntity> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Title).HasMaxLength(100);
        builder.Property(b => b.OpenLibraryKey).HasMaxLength(50);
        builder.Property(b => b.ImageUrl).HasMaxLength(2000);
    }
}