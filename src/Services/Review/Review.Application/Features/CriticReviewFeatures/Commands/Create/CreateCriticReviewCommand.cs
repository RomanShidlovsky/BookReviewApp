using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Create;

public sealed record CreateCriticReviewCommand(CreateReviewDto Dto) : ICreateCommand<ReviewResponseDto>;