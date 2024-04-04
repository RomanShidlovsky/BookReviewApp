using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Book.Infrastructure.UnitOfWork;

public class UnitOfWork(BookContext _context, IServiceProvider _serviceProvider) 
    : IUnitOfWork
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