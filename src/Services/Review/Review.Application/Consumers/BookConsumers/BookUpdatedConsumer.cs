using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.Features.BookFeatures.Commands.Update;

namespace Review.Application.Consumers.BookConsumers;

public class BookUpdatedConsumer(ISender _sender) : IConsumer<IBookUpdated>
{
    public async Task Consume(ConsumeContext<IBookUpdated> context)
    {
        var book = context.Message;

        await Console.Out.WriteLineAsync($"BookUpdated message with Id = {book.Id} consumed.");

        await _sender.Send(new UpdateBookCommand(book));
    }
}