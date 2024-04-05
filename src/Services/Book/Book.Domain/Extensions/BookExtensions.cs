using Book.Domain.Specifications.BookSpecifications;

namespace Book.Domain.Extensions;

public static class BookExtensions
{
    public static bool ContainsAuthor(this Entities.Book book, int authorId)
    {
        var specification = new ContainsAuthorSpecification(authorId);
        
        return specification.IsSatisfied(book);
    }

    public static bool ContainsSubject(this Entities.Book book, string subjectName)
    {
        var specification = new ContainsSubjectSpecification(subjectName);
        
        return specification.IsSatisfied(book);
    }
    
    public static bool ContainsLanguage(this Entities.Book book, string languageName)
    {
        var specification = new ContainsLanguageSpecification(languageName);
        
        return specification.IsSatisfied(book);
    }
}