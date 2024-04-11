using Microsoft.Extensions.DependencyInjection;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;

namespace Review.Infrastructure.Repositories;

public class UnitOfWork(ReviewContext _context, IServiceProvider _serviceProvider) : IUnitOfWork
{
    private bool _disposed = false;

    public TRepository GetRepository<TRepository>() where TRepository : IBaseRepository
    {
        return _serviceProvider.GetRequiredService<TRepository>();
    }

    public void Save()
    {
        _context.SaveChanges();
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}