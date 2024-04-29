using Book.Domain.Entities;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Shared.Cache;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class SubjectRepository(BookContext context, IDistributedCache _cache) 
    : BaseRepository<Subject>(context), ISubjectRepository
{
    private const string BaseCacheKey = "Subject";
    private const string AllSubjectsKey = "AllSubjects";
    
    private IQueryable<BookEntity> GetBookSet()
    {
        return Context.Set<BookEntity>()
            .Where(b => b.DateDeleted == null);
    }

    public override async Task<List<Subject>> GetAllAsync(CancellationToken cancellationToken)
    {
        var subjectsCache = await _cache.GetAsync(AllSubjectsKey, cancellationToken);

        List<Subject>? subjects;

        if (subjectsCache is not null)
        {
            subjects = Cache<List<Subject>>.GetData(subjectsCache);
        }
        else
        {
            subjects = await base.GetAllAsync(cancellationToken);

            subjectsCache = Cache<List<Subject>>.GetCache(subjects, out var options);

            await _cache.SetAsync(AllSubjectsKey, subjectsCache, options, cancellationToken);
        }

        return subjects;
    }

    public override async Task<Subject?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + id;

        var subjectCache = await _cache.GetAsync(cacheKey, cancellationToken);

        Subject? subject;

        if (subjectCache is not null)
        {
            subject = Cache<Subject>.GetData(subjectCache);
        }
        else
        {
            subject = await base.GetByIdAsync(id, cancellationToken);

            subjectCache = Cache<Subject>.GetCache(subject, out var options);

            await _cache.SetAsync(cacheKey, subjectCache, options, cancellationToken);
        }

        return subject;
    }

    public async Task<Subject?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + name;

        var subjectCache = await _cache.GetAsync(cacheKey, cancellationToken);

        Subject? subject;

        if (subjectCache is not null)
        {
            subject = Cache<Subject>.GetData(subjectCache);
        }
        else
        {
            subject = await GetEntitySet()
                .FirstOrDefaultAsync(s => s.Name == name, cancellationToken);

            subjectCache = Cache<Subject>.GetCache(subject, out var options);

            await _cache.SetAsync(cacheKey, subjectCache, options, cancellationToken);
        }

        return subject;
    }

    public override void Create(Subject entity)
    {
        base.Create(entity);
        
        _cache.Remove(AllSubjectsKey);
    }

    public override void Update(Subject entity)
    {
        base.Update(entity);
        
        RemoveSubjectCache(entity.Id, entity.Name);
    }

    public override void Delete(Subject entity)
    {
        base.Delete(entity);
        
        RemoveSubjectCache(entity.Id, entity.Name);
    }

    public async Task<bool> AddSubjectToBookAsync(int subjectId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstAsync(b => b.Id == bookId, cancellationToken);
        
        var subject = await GetEntitySet()
            .FirstAsync(s => s.Id == subjectId, cancellationToken);
        
        book.Subjects.Add(subject);

        await RemoveBookCacheAsync(book.Id, book.OpenLibraryKey, cancellationToken);
        
        return true;
    }

    public async Task<bool> RemoveSubjectFromBookAsync(int subjectId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstAsync(b => b.Id == bookId, cancellationToken);
        
        var subject = await GetEntitySet()
            .FirstAsync(s => s.Id == subjectId, cancellationToken);
        
        await RemoveBookCacheAsync(book.Id, book.OpenLibraryKey, cancellationToken);
        
        return book.Subjects.Remove(subject);
    }
    
    private async Task RemoveBookCacheAsync(int id, string? key, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(BookRepository.BaseCacheKey + id, cancellationToken);

        if (key is not null)
        {
            await _cache.RemoveAsync(BookRepository.BaseCacheKey + key, cancellationToken);
        }
    }

    private void RemoveSubjectCache(int id, string name)
    {
        _cache.RemoveAsync(BaseCacheKey + id);
        _cache.RemoveAsync(BaseCacheKey + name);
        _cache.Remove(AllSubjectsKey);
    }
}