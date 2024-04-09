using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared;
using Shared.Interfaces;

namespace Book.Infrastructure.EntityConfigurations;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IBaseEntity 
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(b => b.DateCreated).HasDefaultValueSql("SYSUTCDATETIME()");
        builder.HasQueryFilter(e => e.DateDeleted == null);
    }
}