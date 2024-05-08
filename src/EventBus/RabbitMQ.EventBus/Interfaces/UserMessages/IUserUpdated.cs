namespace RabbitMQ.EventBus.Interfaces.UserMessages;

public interface IUserUpdated
{
    int Id { get; }
    string UserName { get; }
    string? ImageUrl { get; }
}