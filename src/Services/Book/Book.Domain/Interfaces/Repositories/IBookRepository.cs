using BookEntity = Book.Domain.Entities.Book;

namespace Book.Domain.Interfaces.Repositories;

public interface IBookRepository : IBaseRepository<BookEntity>
{
    Task<List<BookEntity>> GetBooksAsync(int pageNumber, int pageSize, string filterQueryString,
        string orderByQueryString, int[]? selectedSubjects, int[]? selectedLanguages, int[]? selectedAuthors,
        CancellationToken cancellationToken);

    Task<List<BookEntity>> GetBooksBySubjectMatchesAsync(int bookId, int count, CancellationToken cancellationToken);
    Task<BookEntity?> GetByOpenLibraryKeyAsync(string key, CancellationToken cancellationToken);
    Task UpdateRatingAsync(int bookId, double rating, CancellationToken cancellationToken);
    Task UpdateCriticRatingAsync(int bookId, double rating, CancellationToken cancellationToken);
}