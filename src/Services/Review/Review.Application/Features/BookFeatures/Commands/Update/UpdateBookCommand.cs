using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.BookFeatures.Commands.Update;

public sealed record UpdateBookCommand(UpdateBookDto Dto) : IUpdateCommand<BookResponseDto>;