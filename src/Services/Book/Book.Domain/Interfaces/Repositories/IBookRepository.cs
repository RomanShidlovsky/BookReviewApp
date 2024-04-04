using BookEntity = Book.Domain.Entities.Book;

namespace Book.Domain.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    Task<BookEntity?> GetByOpenLibraryKey(string key, CancellationToken cancellationToken);
}