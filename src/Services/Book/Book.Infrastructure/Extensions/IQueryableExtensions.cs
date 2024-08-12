using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Infrastructure.Extensions;

public static class IQueryableExtensions
{
     public static IQueryable<BookEntity> Sort(this IQueryable<BookEntity> books, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return books.OrderByDescending(b => b.DateCreated);
        }
        
        var orderParams = orderByQueryString.Trim().Split(',');

        var orderQuery = string.Join(",", orderParams);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return books.OrderByDescending(b => b.DateCreated);
        }
        
        return books.OrderBy(orderQuery);
    }

    public static IQueryable<BookEntity> Paginate(this IQueryable<BookEntity> books, int pageNumber, int pageSize)
    {
        return books.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static IQueryable<BookEntity> Filter(this IQueryable<BookEntity> books, string filterQueryString)
    {
        if (string.IsNullOrWhiteSpace(filterQueryString))
        {
            return books;
        }
        
        var filterParams = filterQueryString.Trim().Split(',');

        var parameter = Expression.Parameter(typeof(BookEntity), "x");
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
            
            if (paramName.Equals("PublicationYearStart", StringComparison.OrdinalIgnoreCase))
            {
                var property = Expression.Property(parameter, "PublicationYear");
                var value = Expression.Constant(int.Parse(paramValue));
                expression = Expression.GreaterThanOrEqual(property, value);
            }
            else if (paramName.Equals("PublicationYearEnd", StringComparison.OrdinalIgnoreCase))
            {
                var property = Expression.Property(parameter, "PublicationYear");
                var value = Expression.Constant(int.Parse(paramValue));
                expression = Expression.LessThanOrEqual(property, value);
            }
            else if (paramName.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                var property = Expression.Property(parameter, paramName);
                var value = Expression.Constant(paramValue);
            
                // Create the expression for EF.Functions.Like
                var methodName = nameof(string.Contains);
                var method = typeof(string).GetMethod(methodName, new[] { typeof(string) });
                var callExpression = Expression.Call(property, method!, value);

                expression = callExpression;
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

        var predicateLambda = Expression.Lambda<Func<BookEntity, bool>>(predicateBody, parameter);
        return books.Where(predicateLambda);
    }
    
    private static Expression GetYearRangeExpression(string paramName, string paramValue)
    {
        var parameter = Expression.Parameter(typeof(BookEntity), "x");
        
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