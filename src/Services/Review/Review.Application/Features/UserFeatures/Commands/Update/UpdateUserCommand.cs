using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.UserFeatures.Commands.Update;

public sealed record UpdateUserCommand(UpdateUserDto Dto) : IUpdateCommand<UserResponseDto>;