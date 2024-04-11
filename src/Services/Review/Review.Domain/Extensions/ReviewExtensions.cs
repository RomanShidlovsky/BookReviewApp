using Review.Domain.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Review.Domain.Extensions;

public static class ReviewExtensions
{
    public static IQueryable<ReviewEntity> Sort(this IQueryable<ReviewEntity> reviews, string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return reviews.OrderByDescending(r => r.DateCreated);
        }
        
        var orderParams = orderByQueryString.Trim().Split(',');

        var orderQuery = string.Join(",", orderParams);

        if (string.IsNullOrWhiteSpace(orderQuery))
        {
            return reviews.OrderByDescending(r => r.DateCreated);
        }
        
        return reviews.OrderBy(orderQuery);
    }
    
    public static IQueryable<ReviewEntity> Paginate(this IQueryable<ReviewEntity> reviews, int pageNumber, int pageSize)
    {
        return reviews.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);
    }

    public static IQueryable<ReviewEntity> Filter(this IQueryable<ReviewEntity> reviews, string filterQueryString)
    {
        if (string.IsNullOrWhiteSpace(filterQueryString))
            return reviews;

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
            
            if (paramName.Equals("Rating", StringComparison.OrdinalIgnoreCase))
            {
                expression = GetRatingRangeExpression(paramName, paramValue);
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
            return reviews;

        var predicateLambda = Expression.Lambda<Func<ReviewEntity, bool>>(predicateBody, parameter);
        return reviews.Where(predicateLambda);
    }
    
    private static Expression GetRatingRangeExpression(string paramName, string paramValue)
    {
        var parameter = Expression.Parameter(typeof(ReviewEntity), "x");
        
        var ratingRange = paramValue.Split('-');
        if (ratingRange.Length == 2 && int.TryParse(ratingRange[0], out var startYear) &&
            int.TryParse(ratingRange[1], out var endYear))
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