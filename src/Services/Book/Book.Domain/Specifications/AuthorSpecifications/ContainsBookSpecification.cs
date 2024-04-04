using Book.Domain.Entities;

namespace Book.Domain.Specifications.AuthorSpecifications;

public class ContainsBookSpecification(int bookId)
    : ExpressionSpecification<Author>(a => a.Books.Any(b => b.Id == bookId));