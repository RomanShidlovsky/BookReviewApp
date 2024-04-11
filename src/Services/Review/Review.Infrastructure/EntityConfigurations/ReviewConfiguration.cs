using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using Review.Domain.Entities;

namespace Review.Infrastructure.EntityConfigurations;

public class ReviewConfiguration : BaseConfiguration<ReviewEntity>
{
    public override void Configure(EntityTypeBuilder<ReviewEntity> builder)
    {
        base.Configure(builder);

        builder.ToCollection("reviews");
    }
}