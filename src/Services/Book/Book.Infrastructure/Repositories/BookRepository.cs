using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Book.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class BookRepository(BookContext context) : BaseRepository<BookEntity>(context), IBookRepository
{
    protected override IQueryable<BookEntity> GetEntitySet()
    {
        return base.GetEntitySet()
            .Include(b => b.Authors)
            .Include(b => b.Subjects)
            .Include(b => b.Languages);
    }

    public Task<BookEntity?> GetByOpenLibraryKeyAsync(string key, CancellationToken cancellationToken)
    {
        return GetEntitySet()
            .FirstOrDefaultAsync(b => b.OpenLibraryKey != null && b.OpenLibraryKey == key, 
                cancellationToken);
    }

    public Task<List<BookEntity>> GetBooksAsync(int pageNumber, int pageSize, string filterQueryString,
        string orderByQueryString, CancellationToken cancellationToken)
    {
        return GetEntitySet()
            .Filter(filterQueryString)
            .Sort(orderByQueryString)
            .Paginate(pageNumber, pageSize)
            .ToListAsync(cancellationToken);
    }
}