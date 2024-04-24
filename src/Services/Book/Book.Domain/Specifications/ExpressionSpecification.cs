using System.Linq.Expressions;

namespace Book.Domain.Specifications;

public abstract class ExpressionSpecification<T>(Expression<Func<T, bool>> expression)
    : ISpecification<T>
{
    private Func<T, bool>? _expressionFunc;
    private Func<T, bool> ExpressionFunc => _expressionFunc ??= expression.Compile();
    
    public bool IsSatisfied(T obj)
    {
        return ExpressionFunc(obj);
    }
}