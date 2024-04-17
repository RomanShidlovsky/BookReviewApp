using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.ReviewFeatures.Queries.GetById;

public sealed record GetReviewByIdQuery(int Id) : ISingleQuery<ReviewResponseDto>;