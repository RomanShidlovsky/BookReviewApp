using Book.Application.DTOs.Author.RequestDTOs;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.AuthorFeatures.Commands.Create;

public sealed record CreateAuthorCommand(CreateAuthorDto Dto) : ICreateCommand<AuthorResponseDto>;