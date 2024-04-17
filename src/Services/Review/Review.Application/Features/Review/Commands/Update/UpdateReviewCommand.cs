using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.Review.Commands.Update;

public sealed record UpdateReviewCommand(UpdateReviewDto Dto) : IUpdateCommand<ReviewResponseDto>;