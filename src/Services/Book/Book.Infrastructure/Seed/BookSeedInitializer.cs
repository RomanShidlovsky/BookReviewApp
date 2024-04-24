using Book.Domain.Interfaces.Repositories;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Seed;

public class BookSeedInitializer(IBookRepository _bookRepository) : ISeedInitializer
{
    public static BookEntity[] Books =
    [
        new BookEntity
        {
            EditionCount = 257,
            Title = "Harry Potter and the Prisoner of Azkaban",
            Authors = [AuthorSeedInitializer.Authors[0]],
            Subjects = [SubjectSeedInitializer.Subjects[0], SubjectSeedInitializer.Subjects[1]],
            PublicationYear = 1999,
            Languages =
            [
                LanguageSeedInitializer.Languages[0],
                LanguageSeedInitializer.Languages[1],
                LanguageSeedInitializer.Languages[2]
            ]
        },
        new BookEntity
        {
            EditionCount = 66,
            Title = "Harry Potter and the Prisoner of Azkaban",
            Authors = [AuthorSeedInitializer.Authors[1]],
            Subjects = [SubjectSeedInitializer.Subjects[2]],
            PublicationYear = 1962,
            Languages =
            [
                LanguageSeedInitializer.Languages[0],
                LanguageSeedInitializer.Languages[1],
            ]
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