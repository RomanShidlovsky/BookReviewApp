namespace RabbitMQ.EventBus.Interfaces.UserMessages;

public interface IUserCreated
{
    int Id { get; }
    string UserName { get; }
    string? ImageUrl { get; }
}