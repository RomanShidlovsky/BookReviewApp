using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Review.Application.DTOs.RequestDTOs;

public sealed record CreateBookDto(
    int Id,
    string Title,
    string? ImageUrl) 
    : IBookCreated;