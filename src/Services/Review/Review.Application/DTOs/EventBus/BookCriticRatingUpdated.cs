using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Review.Application.DTOs.EventBus;

public sealed record BookCriticRatingUpdated(int Id, double CriticRating) : IBookCriticRatingUpdated;