using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetById;

public sealed record GetCriticReviewByIdQuery(string Id) : ISingleQuery<ReviewResponseDto>;