using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.ReviewFeatures.Queries.GetBookReviews;

public sealed record GetBookReviewsQuery(int BookId) : IQuery<ReviewResponseDto>;