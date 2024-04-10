using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;

namespace Book.Infrastructure.Seed;

public class SeedInitializer(IUnitOfWork _unitOfWork) : ISeedInitializer
{
    public async void Init()
    {
        ISeedInitializer[] initializers =
        [
            new LanguageSeedInitializer(_unitOfWork.GetRepository<ILanguageRepository>()),
            new SubjectSeedInitializer(_unitOfWork.GetRepository<ISubjectRepository>()),
            new AuthorSeedInitializer(_unitOfWork.GetRepository<IAuthorRepository>()),
            new BookSeedInitializer(_unitOfWork.GetRepository<IBookRepository>())
        ];

        var bookExists = await _unitOfWork.GetRepository<IBookRepository>().ExistsAsync(1, new CancellationToken());
        
        if (bookExists)
            return;
        
        foreach (var initializer in initializers)
        {
            initializer.Init();
            _unitOfWork.Save();
        }
    }
}