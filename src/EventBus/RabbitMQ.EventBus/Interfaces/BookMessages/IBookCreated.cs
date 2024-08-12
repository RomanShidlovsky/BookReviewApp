namespace RabbitMQ.EventBus.Interfaces.BookMessages;

public interface IBookCreated
{
    int Id { get; }
    string Title { get; }
    string? ImageUrl { get; }
}