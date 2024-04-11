using Book.Domain.Entities;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class AuthorRepository(BookContext context) : BaseRepository<Author>(context), IAuthorRepository
{
    private IQueryable<BookEntity> GetBookSet()
    {
        return Context.Set<BookEntity>()
            .Where(b => b.DateDeleted == null);
    }

    public Task<Author?> GetByOpenLibraryKeyAsync(string key, CancellationToken cancellationToken)
    {
        return GetEntitySet()
            .FirstOrDefaultAsync(a => a.OpenLibraryKey == key, cancellationToken);
    }

    public async Task<bool> AddAuthorToBookAsync(int authorId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
        
        var author = await GetEntitySet()
            .FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);
        
        book?.Authors.Add(author);

        return true;
    }

    public async Task<bool> RemoveAuthorFromBookAsync(int authorId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
        
        var author = await GetEntitySet()
            .FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);
        
        return book.Authors.Remove(author);
    }
}