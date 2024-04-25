using RabbitMQ.EventBus.Interfaces.UserMessages;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.UserFeatures.Commands.Create;

public sealed record CreateUserCommand(IUserCreated Dto) : ICreateCommand<UserResponseDto>;