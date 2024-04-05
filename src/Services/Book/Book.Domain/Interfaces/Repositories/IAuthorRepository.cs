using Book.Domain.Entities;

namespace Book.Domain.Interfaces.Repositories;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<Author?> GetByOpenLibraryKey(string key, CancellationToken cancellationToken);
    Task<bool> AddAuthorToBookAsync(int authorId, int bookId,  CancellationToken cancellationToken);
    Task<bool> RemoveAuthorFromBookAsync(int authorId, int bookId, CancellationToken cancellationToken);
}