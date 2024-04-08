using Book.Domain.Entities;

namespace Book.Domain.Specifications.LanguageSpecifications;

public class IsNameSpecification(string name)
    : ExpressionSpecification<Language>(l => l.Name.Trim().ToLower().Equals(name.Trim().ToLower()));