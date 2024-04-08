using Book.Domain.Specifications.BookSpecifications;

namespace Book.Domain.Extensions;

public static class BookExtensions
{
    public static bool ContainsAuthor(this Entities.Book book, int authorId)
    {
        var specification = new ContainsAuthorSpecification(authorId);
        
        return specification.IsSatisfied(book);
    }

    public static bool ContainsSubject(this Entities.Book book, int subjectId)
    {
        var specification = new ContainsSubjectSpecification(subjectId);
        
        return specification.IsSatisfied(book);
    }
    
    public static bool ContainsLanguage(this Entities.Book book, int languageId)
    {
        var specification = new ContainsLanguageSpecification(languageId);
        
        return specification.IsSatisfied(book);
    }
}