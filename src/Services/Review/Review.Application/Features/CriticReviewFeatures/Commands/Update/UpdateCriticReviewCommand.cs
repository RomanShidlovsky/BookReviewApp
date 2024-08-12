using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Update;

public sealed record UpdateCriticReviewCommand(UpdateReviewDto Dto) : IUpdateCommand<ReviewResponseDto>;