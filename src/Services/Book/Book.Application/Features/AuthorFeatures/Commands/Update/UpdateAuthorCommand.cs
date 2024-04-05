using Book.Application.DTOs.Author.RequestDTOs;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.AuthorFeatures.Commands.Update;

public sealed record UpdateAuthorCommand(UpdateAuthorDto Dto) : IUpdateCommand<AuthorResponseDto>;