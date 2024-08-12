using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Review.Application.DTOs.RequestDTOs;

public sealed record DeleteBookDto(int Id) : IBookDeleted;