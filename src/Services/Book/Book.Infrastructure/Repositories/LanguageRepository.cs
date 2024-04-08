using Book.Domain.Entities;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class LanguageRepository(BookContext context) : BaseRepository<Language>(context), ILanguageRepository
{
    private IQueryable<BookEntity> GetBookSet()
    {
        return Context.Set<BookEntity>()
            .Where(b => b.DateDeleted == null);
    }

    public Task<Language?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return GetEntitySet()
            .FirstOrDefaultAsync(l => l.IsName(name), cancellationToken);
    }

    public async Task<bool> AddLanguageToBookAsync(int languageId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);

        if (book == null)
            return false;

        var language = await GetEntitySet()
            .FirstOrDefaultAsync(l => l.Id == languageId, cancellationToken);

        if (language == null)
            return false;
        
        book.Languages.Add(language);
        
        return true;
    }

    public async Task<bool> RemoveLanguageFromBookAsync(int languageId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);

        if (book == null)
            return false;

        var language = await GetEntitySet()
            .FirstOrDefaultAsync(l => l.Id == languageId, cancellationToken);

        if (language == null)
            return false;

        return book.Languages.Remove(language);
    }
}