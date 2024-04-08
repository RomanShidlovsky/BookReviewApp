using Book.Domain.Entities;

namespace Book.Domain.Specifications.AuthorSpecifications;

public class IsOpenLibraryKeySpecification(string key)
    : ExpressionSpecification<Author>(a => 
        a.OpenLibraryKey != null && a.OpenLibraryKey.Trim().ToLower().Equals(key.Trim().ToLower()));