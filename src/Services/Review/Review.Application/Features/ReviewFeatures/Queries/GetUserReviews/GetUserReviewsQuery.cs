using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.ReviewFeatures.Queries.GetUserReviews;

public sealed record GetUserReviewsQuery(int UserId) : IQuery<ReviewResponseDto>;