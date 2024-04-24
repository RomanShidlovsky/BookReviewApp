using Book.Domain.Entities;

namespace Book.Domain.Specifications.SubjectSpecifications;

public class IsNameSpecification(string name)
    : ExpressionSpecification<Subject>(s => s.Name.Trim().ToLower().Equals(name)); 
