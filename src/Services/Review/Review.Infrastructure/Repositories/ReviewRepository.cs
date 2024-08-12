using System.Linq.Expressions;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;
using Shared.Cache;

namespace Review.Infrastructure.Repositories;

public class ReviewRepository(
    IMongoCollection<ReviewEntity> _reviewsCollection,
    IMongoCollection<User> _usersCollection,
    IMongoCollection<Book> _booksCollection,
    IDistributedCache _cache)
    : BaseRepository<ReviewEntity>(_reviewsCollection), IReviewRepository
{
    private const string BaseCacheKey = "Review";
    private const string BookReviewsKey = "BookReviews";

    public override async Task<List<ReviewEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        var reviews = await base.GetAllAsync(cancellationToken);

        await LoadRelativeData(reviews, cancellationToken);

        return reviews;
    }

    public async Task<List<ReviewEntity>> GetBookReviewsAsync(int bookId, CancellationToken cancellationToken)
    {
        var reviews = await GetAsync(review => review.BookId == bookId, cancellationToken);

        return reviews;
    }

    public override async Task<ReviewEntity?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var cacheKey = BaseCacheKey + id;

        var reviewCache = await _cache.GetAsync(cacheKey, cancellationToken);

        ReviewEntity? review;

        if (reviewCache is not null)
        {
            review = Cache<ReviewEntity>.GetData(reviewCache);
        }
        else
        {
            review = await base.GetByIdAsync(id, cancellationToken);

            await LoadRelativeData(review, cancellationToken);

            reviewCache = Cache<ReviewEntity>.GetCache(review, out var options);

            await _cache.SetAsync(cacheKey, reviewCache, options, cancellationToken);
        }

        return review;
    }

    public override async Task<List<ReviewEntity>> GetAsync(Expression<Func<ReviewEntity, bool>> condition,
        CancellationToken cancellationToken)
    {
        var reviews = await base.GetAsync(condition, cancellationToken);

        await LoadRelativeData(reviews, cancellationToken);

        return reviews;
    }

    public override async Task CreateAsync(ReviewEntity entity, CancellationToken cancellationToken)
    {
        await base.CreateAsync(entity, cancellationToken);

        await RemoveCacheAsync(entity.Id, entity.BookId, cancellationToken);
    }

    public override async Task UpdateAsync(ReviewEntity entity, CancellationToken cancellationToken)
    {
        await base.UpdateAsync(entity, cancellationToken);

        await RemoveCacheAsync(entity.Id, entity.BookId, cancellationToken);
    }

    public override async Task DeleteAsync(ReviewEntity entity, CancellationToken cancellationToken)
    {
        await base.DeleteAsync(entity, cancellationToken);

        await RemoveCacheAsync(entity.Id, entity.BookId, cancellationToken);
    }

    public async Task<List<ReviewEntity>> GetBookReviewsAsync(int bookId, CancellationToken cancellationToken)
    {
        var cacheKey = BookReviewsKey + bookId;

        var reviewsCache = await _cache.GetAsync(cacheKey, cancellationToken);

        List<ReviewEntity>? reviews;

        if (reviewsCache is not null)
        {
            reviews = Cache<List<ReviewEntity>>.GetData(reviewsCache);
        }
        else
        {
            reviews = await GetAsync(review => review.BookId == bookId, cancellationToken);

            reviewsCache = Cache<List<ReviewEntity>>.GetCache(reviews, out var options);

            await _cache.SetAsync(cacheKey, reviewsCache, options, cancellationToken);
        }
        
        return reviews;
    }

    public async Task AddCommentToReviewAsync(string reviewId, Comment comment, CancellationToken cancellationToken)
    {
        var review = await GetByIdAsync(reviewId, cancellationToken);

        review.Comments.Add(comment);

        await _reviewsCollection.ReplaceOneAsync(entity => entity.Id.Equals(reviewId), review,
            cancellationToken: cancellationToken);

        await RemoveCacheAsync(reviewId, review.BookId, cancellationToken);
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

    private async Task RemoveCacheAsync(string id, int bookId, CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(BaseCacheKey + id, cancellationToken);
        await _cache.RemoveAsync(BookReviewsKey + bookId, cancellationToken);
    }
}