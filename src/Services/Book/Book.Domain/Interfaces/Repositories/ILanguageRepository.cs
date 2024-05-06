using Book.Domain.Entities;

namespace Book.Domain.Interfaces.Repositories;

public interface ILanguageRepository : IBaseRepository<Language>
{
    Task<Language?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> AddLanguageToBookAsync(int languageId, int bookId, CancellationToken cancellationToken);
    Task<bool> RemoveLanguageFromBookAsync(int languageId, int bookId, CancellationToken cancellationToken);
}