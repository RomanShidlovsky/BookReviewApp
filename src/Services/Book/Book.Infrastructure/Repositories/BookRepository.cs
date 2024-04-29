using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Book.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Cache;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class BookRepository(BookContext context, IDistributedCache _cache)
    : BaseRepository<BookEntity>(context), IBookRepository
{
    public const string BaseCacheKey = "Book";
    public const string BooksCacheKey = "Books";

    public static List<string> CacheKeys = [];

    protected override IQueryable<BookEntity> GetEntitySet()
    {
        return base.GetEntitySet()
            .Include(b => b.Authors)
            .Include(b => b.Subjects)
            .Include(b => b.Languages);
    }

    public async Task<List<BookEntity>> GetBooksAsync(int pageNumber, int pageSize, string filterQueryString,
        string orderByQueryString, CancellationToken cancellationToken)
    {
        var cacheKey = BooksCacheKey + pageNumber + pageSize + filterQueryString + orderByQueryString;

        var booksCache = await _cache.GetAsync(cacheKey, cancellationToken);

        List<BookEntity>? books;

        if (booksCache is not null)
        {
            books = Cache<List<BookEntity>>.GetData(booksCache);
        }
        else
        {
            books = await GetEntitySet()
                .Filter(filterQueryString)
                .Sort(orderByQueryString)
                .Paginate(pageNumber, pageSize)
                .ToListAsync(cancellationToken);

            booksCache = Cache<List<BookEntity>>.GetCache(books, out var options);

            await _cache.SetAsync(cacheKey, booksCache, options, cancellationToken);

            CacheKeys.Add(cacheKey);
        }

        return books;
    }

    public override async Task<BookEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + id;

        var bookCache = await _cache.GetAsync(cacheKey, cancellationToken);

        BookEntity? book;

        if (bookCache is not null)
        {
            book = Cache<BookEntity>.GetData(bookCache);
        }
        else
        {
            book = await base.GetByIdAsync(id, cancellationToken);

            bookCache = Cache<BookEntity>.GetCache(book, out var options);

            await _cache.SetAsync(cacheKey, bookCache, options, cancellationToken);
        }

        return book;
    }

    public async Task<BookEntity?> GetByOpenLibraryKeyAsync(string key, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + key;

        var bookCache = await _cache.GetAsync(cacheKey, cancellationToken);

        BookEntity? book;

        if (bookCache is not null)
        {
            book = Cache<BookEntity>.GetData(bookCache);
        }
        else
        {
            book = await GetEntitySet()
                .FirstOrDefaultAsync(b => b.OpenLibraryKey != null && b.OpenLibraryKey == key,
                    cancellationToken);

            bookCache = Cache<BookEntity>.GetCache(book, out var options);

            await _cache.SetAsync(cacheKey, bookCache, options, cancellationToken);
        }

        return book;
    }

    public override void Create(BookEntity entity)
    {
        base.Create(entity);
        
        RemoveBooksCache();
    }

    public override void Update(BookEntity entity)
    {
        base.Update(entity);
        
        RemoveCache(entity.Id, entity.OpenLibraryKey);
    }

    public override void Delete(BookEntity entity)
    {
        base.Delete(entity);
        
        RemoveCache(entity.Id, entity.OpenLibraryKey);
    }

    private void RemoveCache(int id, string? key)
    {
        _cache.Remove(BaseCacheKey + id);

        if (key is not null)
        {
            _cache.Remove(BaseCacheKey + key);
        }
        
        RemoveBooksCache();
    }

    private async Task RemoveCacheAsync(int id, string? key, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(BaseCacheKey + id, cancellationToken);

        if (key is not null)
        {
            await _cache.RemoveAsync(BaseCacheKey + key, cancellationToken);
        }
        
        await RemoveBooksCacheAsync(cancellationToken);
    }

    private void RemoveBooksCache()
    {
        foreach (var key in CacheKeys)
        {
            _cache.Remove(key);
        }

        CacheKeys.Clear();
    }
    
    private async Task RemoveBooksCacheAsync(CancellationToken cancellationToken)
    {
        foreach (var key in CacheKeys)
        {
           await _cache.RemoveAsync(key, cancellationToken);
        }

        CacheKeys.Clear();
    }
}