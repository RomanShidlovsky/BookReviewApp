using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Book.Application.DTOs.EventBus;

public sealed record BookCreated(int Id, string Title, string? ImageUrl) : IBookCreated;
