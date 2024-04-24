namespace Book.Domain.Specifications.BookSpecifications;

public class ContainsAuthorSpecification(int authorId)
    : ExpressionSpecification<Entities.Book>(b => b.Authors.Any(a => a.Id == authorId));