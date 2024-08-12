using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.ReviewFeatures.Commands.Dislike;

public sealed record DislikeReviewCommand(DislikeReviewDto Dto) : ICommand;