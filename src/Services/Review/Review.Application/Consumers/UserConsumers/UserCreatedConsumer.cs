using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.UserMessages;
using Review.Application.Features.UserFeatures.Commands.Create;

namespace Review.Application.Consumers.UserConsumers;

public class UserCreatedConsumer(ISender _sender) : IConsumer<IUserCreated>
{
    public async Task Consume(ConsumeContext<IUserCreated> context)
    {
        var user = context.Message;

        await Console.Out.WriteLineAsync($"UserCreated message with Id = {user.Id} consumed.");

        await _sender.Send(new CreateUserCommand(user));
    }
}