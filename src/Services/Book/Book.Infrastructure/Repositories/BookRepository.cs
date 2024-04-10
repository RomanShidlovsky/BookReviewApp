using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using BookEntity = Book.Domain.Entities.Book;
using Book.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

    public Task<BookEntity?> GetByOpenLibraryKey(string key, CancellationToken cancellationToken)
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