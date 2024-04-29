using Book.Domain.Entities;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Cache;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class AuthorRepository(BookContext context, IDistributedCache _cache) 
    : BaseRepository<Author>(context), IAuthorRepository
{
    private const string BaseCacheKey = "Author";
    
    private IQueryable<BookEntity> GetBookSet()
    {
        return Context.Set<BookEntity>()
            .Where(b => b.DateDeleted == null);
    }

    public override async Task<Author?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + id;

        var authorCache = await _cache.GetAsync(cacheKey, cancellationToken);

        Author? author;
        
        if (authorCache is not null)
        {
            author = Cache<Author>.GetData(authorCache);
        }
        else
        {
            author = await base.GetByIdAsync(id, cancellationToken);

            authorCache = Cache<Author>.GetCache(author, out var options);

            await _cache.SetAsync(cacheKey, authorCache, options, cancellationToken);
        }

        return author;
    }

    public async Task<Author?> GetByOpenLibraryKeyAsync(string key, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + key;

        var authorCache = await _cache.GetAsync(cacheKey, cancellationToken);

        Author? author;
        
        if (authorCache is not null)
        {
            author = Cache<Author>.GetData(authorCache);
        }
        else
        {
            author = await GetEntitySet()
                .FirstOrDefaultAsync(a => a.OpenLibraryKey == key, cancellationToken);

            authorCache = Cache<Author>.GetCache(author, out var options);

            await _cache.SetAsync(cacheKey, authorCache, options, cancellationToken);
        }

        return author;
    }

    public override void Delete(Author entity)
    {
        base.Delete(entity);
        
        RemoveCache(entity.Id, entity.OpenLibraryKey);
    }

    public override void Update(Author entity)
    {
        base.Update(entity);
        
        RemoveCache(entity.Id, entity.OpenLibraryKey);
    }

    public async Task<bool> AddAuthorToBookAsync(int authorId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstAsync(b => b.Id == bookId, cancellationToken);
        
        var author = await GetEntitySet()
            .FirstAsync(a => a.Id == authorId, cancellationToken);
        
        book.Authors.Add(author);
        
        return true;
    }

    public async Task<bool> RemoveAuthorFromBookAsync(int authorId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstAsync(b => b.Id == bookId, cancellationToken);
        
        var author = await GetEntitySet()
            .FirstAsync(a => a.Id == authorId, cancellationToken);
        
        return book.Authors.Remove(author);
    }

    private async Task RemoveCacheAsync(int id, string? key, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(BaseCacheKey + id, cancellationToken);

        if (key is not null)
        {
            await _cache.RemoveAsync(BaseCacheKey + key, cancellationToken);
        }
    }
    
    private void RemoveCache(int id, string? key)
    {
        _cache.Remove(BaseCacheKey + id);

        if (key is not null)
        {
            _cache.Remove(BaseCacheKey + key);
        }
    }
}