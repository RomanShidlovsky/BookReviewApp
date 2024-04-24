using Book.Application.DTOs.Author.RequestDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.AuthorFeatures.Commands.AddAuthorToBook;

public sealed record AddAuthorToBookCommand(AddAuthorToBookDto Dto) : ICommand;
