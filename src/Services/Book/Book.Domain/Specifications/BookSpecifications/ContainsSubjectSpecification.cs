using Book.Domain.Entities;

namespace Book.Domain.Specifications.BookSpecifications;

public class ContainsSubjectSpecification(string subjectName)
    : ExpressionSpecification<Entities.Book>(b =>
        b.Subjects.Any(s => s.Name.ToLower().Trim().Equals(subjectName.ToLower().Trim())));