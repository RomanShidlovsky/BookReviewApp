using Book.Domain.Entities;
using Book.Domain.Interfaces.Repositories;

namespace Book.Infrastructure.Seed;

public class AuthorSeedInitializer(IAuthorRepository _authorRepository) : ISeedInitializer
{
    public static readonly Author[] Authors =
    [
        new Author
        {
            FirstName = "Joanne",
            LastName = "Rowling",
            FullName = "J. K. Rowling",
            BirthDate = new DateOnly(1965, 7, 31)
        },
        new Author
        {
            FirstName = "Stanisław",
            LastName = "Lem",
            FullName = "Stanisław Herman Lem",
            BirthDate = new DateOnly(1921, 9, 12),
            DeathDate = new DateOnly(2006, 3, 27)
        }
    ];

    public void Init()
    {
        foreach (var author in Authors)
        {
            _authorRepository.Create(author);
        }
    }
}