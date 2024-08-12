using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetBookReviews;

public sealed record GetBookCriticReviewsQuery(int BookId) : IQuery<ReviewResponseDto>;