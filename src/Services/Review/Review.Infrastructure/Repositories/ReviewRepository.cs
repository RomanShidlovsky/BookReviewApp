using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities;
using Review.Domain.Extensions;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Context;

namespace Review.Infrastructure.Repositories;

public class ReviewRepository(ReviewContext context) : BaseRepository<ReviewEntity>(context), IReviewRepository
{
    protected override IQueryable<ReviewEntity> GetEntitySet()
    {
        return base.GetEntitySet()
            .Include(r => r.User)
            .Include(r => r.Book)
            .Include(r => r.Comments);
    }

    public Task<List<ReviewEntity>> GetPagedAsync(int pageNumber, int pageSize, string filterQueryString, string orderByQueryString,
        CancellationToken cancellationToken)
    {
        return GetEntitySet()
            .Filter(filterQueryString)
            .Sort(orderByQueryString)
            .Paginate(pageNumber, pageSize)
            .ToListAsync(cancellationToken);
    }

    public async void AddCommentToReviewAsync(int reviewId, Comment comment, CancellationToken cancellationToken)
    {
        var review = await GetByIdAsync(reviewId, cancellationToken);
        
        review?.Comments.Add(comment);
    }
}