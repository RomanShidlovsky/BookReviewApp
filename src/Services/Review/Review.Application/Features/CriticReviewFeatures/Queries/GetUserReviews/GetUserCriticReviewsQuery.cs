using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetUserReviews;

public sealed record GetUserCriticReviewsQuery(int UserId) : IQuery<ReviewResponseDto>;
