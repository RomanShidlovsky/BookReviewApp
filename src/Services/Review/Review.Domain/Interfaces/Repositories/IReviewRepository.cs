using Review.Domain.Entities;

namespace Review.Domain.Interfaces.Repositories;

public interface IReviewRepository : IBaseRepository<ReviewEntity>
{
    Task<List<ReviewEntity>> GetPagedAsync(int pageNumber, int pageSize, string filterQueryString, string orderByQueryString, 
        CancellationToken cancellationToken);
    Task<bool> AddCommentToReviewAsync(int reviewId, Comment comment, CancellationToken cancellationToken);
}