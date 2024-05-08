using Book.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Infrastructure.EntityConfigurations;

public class LanguageConfiguration : BaseConfiguration<Language>
{
    public override void Configure(EntityTypeBuilder<Language> builder)
    {
        base.Configure(builder);
        builder.Property(l => l.Name).HasMaxLength(15);
    }
}