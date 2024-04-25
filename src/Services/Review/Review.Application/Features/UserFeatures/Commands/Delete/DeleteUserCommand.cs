using RabbitMQ.EventBus.Interfaces.UserMessages;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.UserFeatures.Commands.Delete;

public sealed record DeleteUserCommand(IUserDeleted Dto) : IDeleteCommand;