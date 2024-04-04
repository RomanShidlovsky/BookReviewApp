using Book.Domain.Interfaces.Repositories;

namespace Book.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    TRepository GetRepository<TRepository>() where TRepository : IBaseRepository;
    void Save();
    Task SaveAsync(CancellationToken cancellationToken);
}