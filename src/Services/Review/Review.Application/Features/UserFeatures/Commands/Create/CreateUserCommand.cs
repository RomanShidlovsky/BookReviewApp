using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.UserFeatures.Commands.Create;

public sealed record CreateUserCommand(CreateUserDto Dto) : ICreateCommand<UserResponseDto>;