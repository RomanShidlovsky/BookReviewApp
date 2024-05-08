using Book.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book.Infrastructure.EntityConfigurations;

public class SubjectConfiguration : BaseConfiguration<Subject>
{
    public override void Configure(EntityTypeBuilder<Subject> builder)
    {
        base.Configure(builder);
        builder.Property(s => s.Name).HasMaxLength(255);
    }
}