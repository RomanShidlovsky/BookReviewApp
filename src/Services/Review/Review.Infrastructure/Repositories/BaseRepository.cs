using System.Linq.Expressions;
using MongoDB.Driver;
using Review.Domain.Interfaces;
using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Repositories;

public abstract class BaseRepository<T>(IMongoCollection<T> collection) : IBaseRepository<T>
    where T : class, IBaseEntity
{
    public virtual async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await collection.ReplaceOneAsync(e => e.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        await collection.FindOneAndDeleteAsync(e => e.Id == entity.Id, cancellationToken: cancellationToken);
    }
    
    public virtual async Task<List<T>> GetAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken)
    {
        return await collection.Find(condition)
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await collection.Find(entity => entity.Equals(id))
            .FirstOrDefaultAsync(cancellationToken);
    }
    
    public virtual async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await collection.Find(t => true)
            .ToListAsync(cancellationToken);
    }
}