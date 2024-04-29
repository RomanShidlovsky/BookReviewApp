using Book.Domain.Entities;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Shared.Cache;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class LanguageRepository(BookContext context, IDistributedCache _cache)
    : BaseRepository<Language>(context), ILanguageRepository
{
    private const string BaseCacheKey = "Language";
    private const string AllLanguagesKey = "AllLanguages";

    private IQueryable<BookEntity> GetBookSet()
    {
        return Context.Set<BookEntity>()
            .Where(b => b.DateDeleted == null);
    }

    public override async Task<List<Language>> GetAllAsync(CancellationToken cancellationToken)
    {
        var languagesCache = await _cache.GetAsync(AllLanguagesKey, cancellationToken);

        List<Language>? languages;

        if (languagesCache is not null)
        {
            languages = Cache<List<Language>>.GetData(languagesCache);
        }
        else
        {
            languages = await base.GetAllAsync(cancellationToken);

            languagesCache = Cache<List<Language>>.GetCache(languages, out var options);

            await _cache.SetAsync(AllLanguagesKey, languagesCache, options, cancellationToken);
        }

        return languages;
    }

    public override async Task<Language?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + id;

        var languageCache = await _cache.GetAsync(cacheKey, cancellationToken);

        Language? language;

        if (languageCache is not null)
        {
            language = Cache<Language>.GetData(languageCache);
        }
        else
        {
            language = await base.GetByIdAsync(id, cancellationToken);

            languageCache = Cache<Language>.GetCache(language, out var options);

            await _cache.SetAsync(cacheKey, languageCache, options, cancellationToken);
        }

        return language;
    }

    public async Task<Language?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + name;

        var languageCache = await _cache.GetAsync(cacheKey, cancellationToken);

        Language? language;

        if (languageCache is not null)
        {
            language = Cache<Language>.GetData(languageCache);
        }
        else
        {
            language = await GetEntitySet()
                .FirstOrDefaultAsync(l => l.Name == name, cancellationToken);

            languageCache = Cache<Language>.GetCache(language, out var options);

            await _cache.SetAsync(cacheKey, languageCache, options, cancellationToken);
        }

        return language;
    }

    public override void Create(Language entity)
    {
        base.Create(entity);
        
        _cache.Remove(AllLanguagesKey);
    }

    public override void Update(Language entity)
    {
        base.Update(entity);
        
        RemoveLanguageCache(entity.Id, entity.Name);
    }

    public override void Delete(Language entity)
    {
        base.Delete(entity);
        
        RemoveLanguageCache(entity.Id, entity.Name);
    }

    public async Task<bool> AddLanguageToBookAsync(int languageId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstAsync(b => b.Id == bookId, cancellationToken);

        var language = await GetEntitySet()
            .FirstAsync(l => l.Id == languageId, cancellationToken);

        book.Languages.Add(language);

        await RemoveBookCacheAsync(book.Id, book.OpenLibraryKey, cancellationToken);

        return true;
    }

    public async Task<bool> RemoveLanguageFromBookAsync(int languageId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstAsync(b => b.Id == bookId, cancellationToken);

        var language = await GetEntitySet()
            .FirstAsync(l => l.Id == languageId, cancellationToken);

        await RemoveBookCacheAsync(book.Id, book.OpenLibraryKey, cancellationToken);
        
        return book.Languages.Remove(language);
    }

    private async Task RemoveBookCacheAsync(int id, string? key, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(BookRepository.BaseCacheKey + id, cancellationToken);

        if (key is not null)
        {
            await _cache.RemoveAsync(BookRepository.BaseCacheKey + key, cancellationToken);
        }
    }
    
    private void RemoveLanguageCache(int id, string name)
    {
        _cache.RemoveAsync(BaseCacheKey + id);
        _cache.RemoveAsync(BaseCacheKey + name);
        _cache.Remove(AllLanguagesKey);
    }
}