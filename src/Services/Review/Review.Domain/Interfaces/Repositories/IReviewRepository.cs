using Review.Domain.Entities;

namespace Review.Domain.Interfaces.Repositories;

public interface IReviewRepository : IBaseRepository<ReviewEntity>
{
    Task<List<ReviewEntity>> GetBookReviewsAsync(int bookId, CancellationToken cancellationToken);
    Task<List<ReviewEntity>> GetUserReviewsAsync(int userId, CancellationToken cancellationToken);
    Task AddCommentToReviewAsync(string reviewId, Comment comment, CancellationToken cancellationToken);
    Task<bool> LikeExists(string reviewId, int userId, CancellationToken cancellationToken);
    Task Like(string reviewId, int userId, CancellationToken cancellationToken);
    Task Unlike(string reviewId, int userId, CancellationToken cancellationToken);
    Task<bool> DislikeExists(string reviewId, int userId, CancellationToken cancellationToken);
    Task Dislike(string reviewId, int userId, CancellationToken cancellationToken);
    Task Undislike(string reviewId, int userId, CancellationToken cancellationToken);
}