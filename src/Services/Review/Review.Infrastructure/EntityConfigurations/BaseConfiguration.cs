using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Interfaces;

namespace Review.Infrastructure.EntityConfigurations;

public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IBaseEntity 
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(b => b.DateCreated).HasDefaultValueSql("new Date()");
        builder.HasQueryFilter(e => e.DateDeleted == null);
    }
}