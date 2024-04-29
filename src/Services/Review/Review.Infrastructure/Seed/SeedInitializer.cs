using Review.Infrastructure.Repositories;

namespace Review.Infrastructure.Seed;

public class SeedInitializer(IUnitOfWork _unitOfWork) : ISeedInitializer
{
    public async void Init()
    {
        ISeedInitializer[] initializers =
        [
            new UserSeedInitializer(_unitOfWork.UserRepository),
            new BookSeedInitializer(_unitOfWork.BookRepository),
            new ReviewSeedInitializer(_unitOfWork.ReviewRepository)
        ];

        var userExists = await _unitOfWork.UserRepository.GetByIdAsync("1", new CancellationToken());

        if (userExists is not null)
        {
            return;
        }

        foreach (var initializer in initializers)
        {
            initializer.Init();
        }
    }
}