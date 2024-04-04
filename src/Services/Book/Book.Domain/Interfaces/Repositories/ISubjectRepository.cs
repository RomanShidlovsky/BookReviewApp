using Book.Domain.Entities;

namespace Book.Domain.Interfaces.Repositories;

public interface ISubjectRepository : IBaseRepository<Subject>
{
    Task<bool> AddSubjectToBookAsync(int subjectId, int bookId, CancellationToken cancellationToken);
    Task<bool> RemoveSubjectFromBookAsync(int subjectId, int bookId, CancellationToken cancellationToken);
}