using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.Review.Commands.Create;

public sealed record CreateReviewCommand(CreateReviewDto Dto) : ICreateCommand<ReviewResponseDto>;