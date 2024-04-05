using Book.Domain.Entities;
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
    
    public async Task<bool> AddAuthorToBookAsync(int authorId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);

        if (book == null)
            return false;

        var author = await GetEntitySet()
            .FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);

        if (author == null)
            return false;
        
        book.Authors.Add(author);
        
        return true;
    }

    public async Task<bool> DeleteAuthorFromBookAsync(int authorId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);

        if (book == null)
            return false;

        var author = await GetEntitySet()
            .FirstOrDefaultAsync(a => a.Id == authorId, cancellationToken);

        if (author == null)
            return false;

        return book.Authors.Remove(author);
    }
}