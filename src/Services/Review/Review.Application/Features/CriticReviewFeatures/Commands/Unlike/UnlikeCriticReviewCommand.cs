using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Unlike;

public sealed record UnlikeCriticReviewCommand(LikeReviewDto Dto) : ICommand;