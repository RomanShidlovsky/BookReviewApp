using System.Linq.Expressions;
using MongoDB.Driver;
using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Repositories;

public class ReviewRepository(
    IMongoCollection<ReviewEntity> _reviewsCollection,
    IMongoCollection<User> _usersCollection,
    IMongoCollection<Book> _booksCollection)
    : BaseRepository<ReviewEntity>(_reviewsCollection), IReviewRepository
{
    public override async Task<List<ReviewEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var reviews = await base.GetAllAsync(cancellationToken);

        await LoadRelativeData(reviews, cancellationToken);

        return reviews;
    }

    public override async Task<ReviewEntity?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var review = await base.GetByIdAsync(id, cancellationToken);

        await LoadRelativeData(review, cancellationToken);

        return review;
    }

    public override async Task<List<ReviewEntity>> GetAsync(Expression<Func<ReviewEntity, bool>> condition, CancellationToken cancellationToken)
    {
        var reviews = await base.GetAsync(condition, cancellationToken);

        await LoadRelativeData(reviews, cancellationToken);

        return reviews;
    }

    public async Task AddCommentToReviewAsync(string reviewId, Comment comment, CancellationToken cancellationToken)
    {
        var review = await GetByIdAsync(reviewId, cancellationToken);

        review.Comments.Add(comment);

        await _reviewsCollection.ReplaceOneAsync(entity => entity.Id.Equals(reviewId), review,
            cancellationToken: cancellationToken);
    }

    private async Task LoadRelativeData(IEnumerable<ReviewEntity> reviews, CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(reviews, cancellationToken, async (review, token) =>
        {
            var user = await _usersCollection
                .Find(user => user.Id == review.UserId.ToString())
                .FirstOrDefaultAsync(token);

            var book = await _booksCollection
                .Find(book => book.Id == review.BookId.ToString())
                .FirstOrDefaultAsync(token);

            review.User = user;
            review.Book = book;
        });
    }

    private async Task LoadRelativeData(ReviewEntity review, CancellationToken cancellationToken)
    {
        var user = await _usersCollection
            .Find(user => user.Id.Equals(review.UserId.ToString()))
            .FirstOrDefaultAsync(cancellationToken);

        var book = await _booksCollection
            .Find(book => book.Id.Equals(review.BookId.ToString()))
            .FirstOrDefaultAsync(cancellationToken);

        review.User = user;
        review.Book = book;
    }
}