

namespace Book.Domain.Specifications.BookSpecifications;

public class IsOpenLibraryKeySpecification(string key)
    : ExpressionSpecification<Entities.Book>(b => 
        b.OpenLibraryKey != null && b.OpenLibraryKey.Trim().ToLower().Equals(key));