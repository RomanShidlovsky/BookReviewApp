namespace RabbitMQ.EventBus.Interfaces.BookMessages;

public interface IBookRatingUpdated
{
    int Id { get; }
    double Rating { get; }
}