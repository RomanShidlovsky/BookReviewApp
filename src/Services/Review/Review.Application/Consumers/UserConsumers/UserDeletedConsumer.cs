using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.UserMessages;
using Review.Application.Features.UserFeatures.Commands.Delete;

namespace Review.Application.Consumers.UserConsumers;

public class UserDeletedConsumer(ISender _sender) : IConsumer<IUserDeleted>
{
    public async Task Consume(ConsumeContext<IUserDeleted> context)
    {
        var user = context.Message;

        await Console.Out.WriteLineAsync($"UserDeleted message with Id = {user.Id} consumed.");

        await _sender.Send(new DeleteUserCommand(user));
    }
}