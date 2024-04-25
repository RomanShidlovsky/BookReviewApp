using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Seed;

public class UserSeedInitializer(IUserRepository _userRepository) : ISeedInitializer
{
    public static User[] Users =
    [
        new User
        {
            Id = "1",
            UserName = "Client"
        },
        new User
        {
            Id = "2",
            UserName = "Admin"
        },
        new User
        {
            Id = "3",
            UserName = "Reviewer"
        }
    ];

    public void Init()
    {
        foreach (var user in Users)
        {
            _userRepository.Create(user);
        }
    }
}