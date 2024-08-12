using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetAll;

public sealed record GetAllCriticReviewsQuery : IQuery<ReviewResponseDto>;