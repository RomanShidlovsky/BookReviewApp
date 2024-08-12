using RabbitMQ.EventBus.Interfaces.UserMessages;

namespace Identity.BusinessLogic.DTOs.ProducerDTOs;

public sealed record UserCreatedDto(
    int Id, 
    string UserName,
    string? ImageUrl) 
    : IUserCreated;