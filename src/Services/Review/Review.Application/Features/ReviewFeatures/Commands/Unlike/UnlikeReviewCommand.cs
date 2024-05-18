using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.ReviewFeatures.Commands.Unlike;

public sealed record UnlikeReviewCommand(LikeReviewDto Dto) : ICommand;