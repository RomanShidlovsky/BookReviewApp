using Book.Domain.Entities;
using Book.Domain.Specifications.LanguageSpecifications;

namespace Book.Domain.Extensions;

public static class LanguageExtensions
{
    public static bool IsName(this Language language, string name)
    {
        var specification = new IsNameSpecification(name);

        return specification.IsSatisfied(language);
    }
}