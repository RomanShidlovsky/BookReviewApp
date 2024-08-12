using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Review.Application.DTOs.RequestDTOs;

public sealed record UpdateBookDto(
    int Id,
    string Title,
    string? ImageUrl)
    : IBookUpdated;