using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Review.Domain.Entities;

namespace Review.Infrastructure.EntityConfigurations;

public class BookConfiguration : BaseConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        base.Configure(builder);

        builder.ToCollection("books");
    }
}