using Book.Domain.Entities;
using Book.Domain.Specifications.SubjectSpecifications;

namespace Book.Domain.Extensions;

public static class SubjectExtensions
{
    public static bool IsName(this Subject subject, string name)
    {
        var specification = new IsNameSpecification(name);

        return specification.IsSatisfied(subject);
    }
}