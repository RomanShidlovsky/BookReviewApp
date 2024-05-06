using Book.Application.DTOs.Book.RequestDTOs;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.BookFeatures.Commands.Create;

public sealed record CreateBookCommand(CreateBookDto Dto) : ICreateCommand<BookResponseDto>;