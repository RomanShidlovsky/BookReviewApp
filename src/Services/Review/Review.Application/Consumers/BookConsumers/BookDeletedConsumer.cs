using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.Features.BookFeatures.Commands.Delete;

namespace Review.Application.Consumers.BookConsumers;

public class BookDeletedConsumer(ISender _sender) : IConsumer<IBookDeleted>
{
    public async Task Consume(ConsumeContext<IBookDeleted> context)
    {
        var book = context.Message;

        await Console.Out.WriteLineAsync($"BookDeleted message with Id = {book.Id} consumed.");

        await _sender.Send(new DeleteBookCommand(book));
    }
}