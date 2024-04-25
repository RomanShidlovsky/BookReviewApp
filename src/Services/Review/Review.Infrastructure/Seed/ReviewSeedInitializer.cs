using Review.Domain.Entities;
using Review.Domain.Interfaces.Repositories;

namespace Review.Infrastructure.Seed;

public class ReviewSeedInitializer(IReviewRepository _reviewRepository) : ISeedInitializer
{
    private readonly ReviewEntity[] _reviews =
    [
        new ReviewEntity
        {
            BookId = 1,
            UserId = 3,
            Likes = 0,
            Dislikes = 0,
            Rating = 8,
            Text = "Great book!"
        },
        new ReviewEntity
        {
            BookId = 2,
            UserId = 3,
            Likes = 0,
            Dislikes = 0,
            Rating = 9,
            Text = "Awesome"
        }
    ];

    public void Init()
    {
        foreach (var review in _reviews)
        {
            _reviewRepository.Create(review);
        }
    }
}