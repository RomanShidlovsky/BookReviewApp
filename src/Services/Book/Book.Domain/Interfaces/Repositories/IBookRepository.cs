using BookEntity = Book.Domain.Entities.Book;

namespace Book.Domain.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    Task<List<BookEntity>> GetBooksAsync(int pageNumber, int pageSize, string filterQueryString,
        string orderByQueryString, CancellationToken cancellationToken);
    Task<BookEntity?> GetByOpenLibraryKey(string key, CancellationToken cancellationToken);
}