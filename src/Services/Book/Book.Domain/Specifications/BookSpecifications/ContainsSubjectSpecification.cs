using Book.Domain.Entities;

namespace Book.Domain.Specifications.BookSpecifications;

public class ContainsSubjectSpecification(int subjectId)
    : ExpressionSpecification<Entities.Book>(b => b.Subjects.Any(s => s.Id == subjectId));