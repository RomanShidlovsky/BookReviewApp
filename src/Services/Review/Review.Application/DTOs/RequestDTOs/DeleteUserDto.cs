using RabbitMQ.EventBus.Interfaces.UserMessages;

namespace Review.Application.DTOs.RequestDTOs;

public sealed record DeleteUserDto(int Id) : IUserDeleted;