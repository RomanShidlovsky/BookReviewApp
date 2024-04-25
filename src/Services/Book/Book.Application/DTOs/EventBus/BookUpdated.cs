using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Book.Application.DTOs.EventBus;

public sealed record BookUpdated(int Id, string Title, string? ImageUrl) : IBookUpdated;