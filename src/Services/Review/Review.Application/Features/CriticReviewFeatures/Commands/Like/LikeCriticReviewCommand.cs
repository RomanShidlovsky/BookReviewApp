using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Like;

public sealed record LikeCriticReviewCommand(LikeReviewDto Dto) : ICommand;