using System.Linq.Expressions;

namespace Review.Domain.Interfaces.Repositories;

public interface IBaseRepository;
    
public interface IBaseRepository<T> : IBaseRepository
    where T : IBaseEntity
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
    Task<List<T>> GetAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
}