using Book.Domain.Entities;

namespace Book.Domain.Interfaces.Repositories;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<bool> AddAuthorToBookAsync(int authorId, int bookId,  CancellationToken cancellationToken);
    Task<bool> DeleteAuthorFromBookAsync(int authorId, int bookId, CancellationToken cancellationToken);
}