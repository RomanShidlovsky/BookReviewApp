using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Interfaces;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;

namespace Review.Infrastructure.Repositories;

public abstract class BaseRepository<T>(ReviewContext context) : IBaseRepository<T>
    where T : class, IBaseEntity
{
    protected readonly ReviewContext Context = context;
    
    protected virtual IQueryable<T> GetEntitySet()
    {
        return Context.Set<T>();
    }

    public virtual void Create(T entity)
    {
        Context.Add(entity);
    }

    public virtual void Update(T entity)
    {
        entity.DateUpdated = DateTimeOffset.UtcNow;
        Context.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        entity.DateDeleted = DateTimeOffset.UtcNow;
        Context.Update(entity);
    }

    public virtual Task<List<T>> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return GetEntitySet().Where(expression).ToListAsync(cancellationToken);
    }

    public virtual Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return GetEntitySet().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    
    public virtual Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return GetEntitySet().ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return GetEntitySet().AnyAsync(t => t.Id == id, cancellationToken);
    }
}