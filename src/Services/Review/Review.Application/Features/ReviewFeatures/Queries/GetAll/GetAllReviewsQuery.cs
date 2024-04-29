using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.ReviewFeatures.Queries.GetAll;

public sealed record GetAllReviewsQuery : IQuery<ReviewResponseDto>;