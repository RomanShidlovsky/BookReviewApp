using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.ReviewFeatures.Queries.GetById;

public sealed record GetReviewByIdQuery(string Id) : ISingleQuery<ReviewResponseDto>;