using RabbitMQ.EventBus.Interfaces.UserMessages;

namespace Review.Application.DTOs.RequestDTOs;

public sealed record CreateUserDto(
    int Id,
    string UserName,
    string? ImageUrl)
    : IUserCreated;