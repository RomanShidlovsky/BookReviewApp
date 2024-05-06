using Book.Application.DTOs.Book.RequestDTOs;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.BookFeatures.Commands.Update;

public sealed record UpdateBookCommand(UpdateBookDto Dto) : IUpdateCommand<BookResponseDto>;