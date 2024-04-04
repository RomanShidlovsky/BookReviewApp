namespace Book.Domain.Specifications.BookSpecifications;

public class ContainsLanguageSpecification(string languageName)
    : ExpressionSpecification<Entities.Book>(b =>
        b.Languages.Any(l => l.Name.ToLower().Trim().Equals(languageName.ToLower().Trim())));