using Review.Domain.Entities;

namespace Review.Domain.Interfaces.Repositories;

public interface IReviewRepository : IBaseRepository<ReviewEntity>
{
    Task<List<ReviewEntity>> GetBookReviewsAsync(int bookId, CancellationToken cancellationToken);
    Task AddCommentToReviewAsync(string reviewId, Comment comment, CancellationToken cancellationToken);
}