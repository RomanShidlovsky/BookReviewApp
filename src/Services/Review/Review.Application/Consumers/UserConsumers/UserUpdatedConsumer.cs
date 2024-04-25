using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.UserMessages;
using Review.Application.Features.UserFeatures.Commands.Update;

namespace Review.Application.Consumers.UserConsumers;

public class UserUpdatedConsumer(ISender _sender) : IConsumer<IUserUpdated>
{
    public async Task Consume(ConsumeContext<IUserUpdated> context)
    {
        var user = context.Message;

        await Console.Out.WriteLineAsync($"UserUpdated message with Id = {user.Id} consumed.");

        await _sender.Send(new UpdateUserCommand(user));
    }
}