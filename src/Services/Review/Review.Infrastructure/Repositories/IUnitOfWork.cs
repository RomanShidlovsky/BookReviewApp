using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Repositories;

public interface IUnitOfWork : IDisposable
{
    TRepository GetRepository<TRepository>() where TRepository : IBaseRepository;
    void Save();
    Task SaveAsync(CancellationToken cancellationToken);
}