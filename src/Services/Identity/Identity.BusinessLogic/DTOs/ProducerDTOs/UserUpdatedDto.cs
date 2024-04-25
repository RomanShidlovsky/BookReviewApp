using RabbitMQ.EventBus.Interfaces.UserMessages;

namespace Identity.BusinessLogic.DTOs.ProducerDTOs;

public sealed record UserUpdatedDto(
    int Id, 
    string UserName,
    string? ImageUrl) 
    : IUserUpdated;