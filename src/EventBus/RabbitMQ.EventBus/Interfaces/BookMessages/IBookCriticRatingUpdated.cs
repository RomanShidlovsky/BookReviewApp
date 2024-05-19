namespace RabbitMQ.EventBus.Interfaces.BookMessages;

public interface IBookCriticRatingUpdated
{
    int Id { get; }
    double CriticRating { get; }
}