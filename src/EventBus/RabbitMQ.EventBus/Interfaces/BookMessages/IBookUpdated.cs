namespace RabbitMQ.EventBus.Interfaces.BookMessages;

public interface IBookUpdated
{
    int Id { get; }
    string Title { get; }
    string? ImageUrl { get; }
}
    