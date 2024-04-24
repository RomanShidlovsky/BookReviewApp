using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.BookFeatures.Commands.Create;

public record CreateBookCommand(CreateBookDto Dto) : ICreateCommand<BookResponseDto>;