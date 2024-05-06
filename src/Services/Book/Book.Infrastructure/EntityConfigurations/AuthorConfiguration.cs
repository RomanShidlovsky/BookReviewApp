using Book.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Infrastructure.EntityConfigurations;

public class AuthorConfiguration : BaseConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        base.Configure(builder);
        builder.Property(a => a.FirstName).HasMaxLength(25);
        builder.Property(a => a.LastName).HasMaxLength(25);
        builder.Property(a => a.FullName).HasMaxLength(100);
        builder.Property(a => a.ImageUrl).HasMaxLength(2000);
        builder.Property(b => b.OpenLibraryKey).HasMaxLength(50);
    }
}