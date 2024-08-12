using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Undislike;

public sealed record UndislikeCriticReviewCommand(DislikeReviewDto Dto) : ICommand;