using Review.Domain.Entities;

namespace Review.Domain.Interfaces.Repositories;

public interface IReviewRepository : IBaseRepository<ReviewEntity>
{
    Task AddCommentToReviewAsync(string reviewId, Comment comment, CancellationToken cancellationToken);
}