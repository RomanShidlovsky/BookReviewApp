using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.ReviewFeatures.Commands.Like;

public sealed record LikeReviewCommand(LikeReviewDto Dto) : ICommand;