using RabbitMQ.EventBus.Interfaces.UserMessages;

namespace Identity.BusinessLogic.DTOs.ProducerDTOs;

public sealed record UserDeletedDto(int Id) : IUserDeleted;