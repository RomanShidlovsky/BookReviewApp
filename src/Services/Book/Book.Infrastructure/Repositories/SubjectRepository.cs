using Book.Domain.Entities;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Repositories;

public class SubjectRepository(BookContext context) : BaseRepository<Subject>(context), ISubjectRepository
{
    private IQueryable<BookEntity> GetBookSet()
    {
        return Context.Set<BookEntity>()
            .Where(b => b.DateDeleted == null);
    }

    public Task<Subject?> GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return GetEntitySet()
            .FirstOrDefaultAsync(s => s.Name == name, cancellationToken);
    }

    public async Task<bool> AddSubjectToBookAsync(int subjectId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
        
        var subject = await GetEntitySet()
            .FirstOrDefaultAsync(s => s.Id == subjectId, cancellationToken);
        
        book.Subjects.Add(subject);
        
        return true;
    }

    public async Task<bool> RemoveSubjectFromBookAsync(int subjectId, int bookId, CancellationToken cancellationToken)
    {
        var book = await GetBookSet()
            .FirstOrDefaultAsync(b => b.Id == bookId, cancellationToken);
        
        var subject = await GetEntitySet()
            .FirstOrDefaultAsync(s => s.Id == subjectId, cancellationToken);
        
        return book.Subjects.Remove(subject);
    }
}