using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Review.Application.DTOs.EventBus;

public sealed record BookRatingUpdated(int Id, double Rating) : IBookRatingUpdated;