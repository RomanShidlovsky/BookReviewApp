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
            .FirstOrDefaultAsync(b => b.OpenLibraryKey != null && b.OpenLibraryKey.Equals(key), 
                cancellationToken);
    }
}