using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.ReviewFeatures.Commands.Undislike;

public sealed record UndislikeReviewCommand(DislikeReviewDto Dto) : ICommand;