using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Book.Domain.Specifications.BookSpecifications;

namespace Book.Domain.Extensions;

public static class BookExtensions
{
    public static IQueryable<Entities.Book> Sort(this IQueryable<Entities.Book> books, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return books.OrderBy(b => b.Title);
        }
        
        var orderParams = orderByQueryString.Trim().Split(',');

        var orderQuery = string.Join(",", orderParams);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return books.OrderBy(b => b.Title);
        }
        
        return books.OrderBy(orderQuery);
    }

    public static IQueryable<Entities.Book> Paginate(this IQueryable<Entities.Book> books, int pageNumber, int pageSize)
    {
        return books.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static IQueryable<Entities.Book> Filter(this IQueryable<Entities.Book> books, string filterQueryString)
    {
        if (string.IsNullOrWhiteSpace(filterQueryString))
            return books;

        var filterParams = filterQueryString.Trim().Split(',');

        var parameter = Expression.Parameter(typeof(Entities.Book), "x");
        Expression? predicateBody = null;

        foreach (var filterParam in filterParams)
        {
            var filterPair = filterParam.Split('=');
            if (filterPair.Length != 2)
                throw new ArgumentException("Invalid filterQueryString format. " +
                                            "Expected format: 'param1=value1,param2=value2,...'.");

            var paramName = filterPair[0].Trim();
            var paramValue = filterPair[1].Trim();

            Expression expression;
            
            if (paramName.Equals("PublicationYear", StringComparison.OrdinalIgnoreCase))
            {
                expression = GetYearRangeExpression(paramName, paramValue);
            }
            else
            {
                var property = Expression.Property(parameter, paramName);
                var value = Expression.Constant(paramValue);
                expression = Expression.Equal(property, value);
            }

            predicateBody = predicateBody is null
                ? expression
                : Expression.AndAlso(predicateBody, expression);
        }

        if (predicateBody is null)
            return books;

        var predicateLambda = Expression.Lambda<Func<Entities.Book, bool>>(predicateBody, parameter);
        return books.Where(predicateLambda);
    }

    public static bool ContainsAuthor(this Entities.Book book, int authorId)
    {
        var specification = new ContainsAuthorSpecification(authorId);

        return specification.IsSatisfied(book);
    }

    public static bool ContainsSubject(this Entities.Book book, int subjectId)
    {
        var specification = new ContainsSubjectSpecification(subjectId);

        return specification.IsSatisfied(book);
    }

    public static bool ContainsLanguage(this Entities.Book book, int languageId)
    {
        var specification = new ContainsLanguageSpecification(languageId);

        return specification.IsSatisfied(book);
    }

    public static bool IsOpenLibraryKey(this Entities.Book book, string openLibraryKey)
    {
        var specification = new IsOpenLibraryKeySpecification(openLibraryKey);

        return specification.IsSatisfied(book);
    }

    private static Expression GetYearRangeExpression(string paramName, string paramValue)
    {
        var parameter = Expression.Parameter(typeof(Entities.Book), "x");
        
        var yearRange = paramValue.Split('-');
        if (yearRange.Length == 2 && int.TryParse(yearRange[0], out var startYear) &&
            int.TryParse(yearRange[1], out var endYear))
        {
            var property = Expression.Property(parameter, paramName);
            var startValue = Expression.Constant(startYear);
            var endValue = Expression.Constant(endYear);
            var greaterThanOrEqual = Expression.GreaterThanOrEqual(property, startValue);
            var lessThanOrEqual = Expression.LessThanOrEqual(property, endValue);
            var withinRange = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);

            return withinRange;
        }
        else
        {
            var property = Expression.Property(parameter, paramName);
            var value = Expression.Constant(paramValue);
            var equalExpression = Expression.Equal(property, value);

            return equalExpression;
        }
    }
}