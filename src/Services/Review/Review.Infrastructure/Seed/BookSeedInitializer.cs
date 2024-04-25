using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Seed;

public class BookSeedInitializer(IBookRepository _bookRepository) : ISeedInitializer
{
    public static Book[] Books =
    [
        new Book
        {
            Id = "1",
            Title = "Harry Potter and the Prisoner of Azkaban",
            AverageRating = 8
        },
        new Book
        {
            Id = "2",
            Title = "Solaris",
            AverageRating = 9
        }
    ];

    public void Init()
    {
        foreach (var book in Books)
        {
            _bookRepository.Create(book);
        }
    }
}