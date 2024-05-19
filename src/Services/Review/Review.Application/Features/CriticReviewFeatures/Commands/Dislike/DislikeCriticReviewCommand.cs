using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Dislike;

public sealed record DislikeCriticReviewCommand(DislikeReviewDto Dto) : ICommand;