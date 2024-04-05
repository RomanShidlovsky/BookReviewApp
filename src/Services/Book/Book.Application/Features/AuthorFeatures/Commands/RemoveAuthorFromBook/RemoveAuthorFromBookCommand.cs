using Book.Application.DTOs.Author.RequestDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.AuthorFeatures.Commands.RemoveAuthorFromBook;

public sealed record RemoveAuthorFromBookCommand(RemoveAuthorFromBookDto Dto) : ICommand;