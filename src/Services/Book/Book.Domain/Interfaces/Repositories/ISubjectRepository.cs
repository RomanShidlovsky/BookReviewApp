using Book.Domain.Entities;

namespace Book.Domain.Interfaces.Repositories;

public interface ISubjectRepository : IBaseRepository<Subject>
{
    Task<Subject?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> AddSubjectToBookAsync(int subjectId, int bookId, CancellationToken cancellationToken);
    Task<bool> RemoveSubjectFromBookAsync(int subjectId, int bookId, CancellationToken cancellationToken);
}