namespace Book.Domain.Specifications.BookSpecifications;

public class ContainsLanguageSpecification(int languageId)
    : ExpressionSpecification<Entities.Book>(b => b.Languages.Any(l => l.Id == languageId));