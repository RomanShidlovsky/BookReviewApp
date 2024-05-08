using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.Features.BookFeatures.Commands.Create;

namespace Review.Application.Consumers.BookConsumers;

public class BookCreatedConsumer(ISender _sender) : IConsumer<IBookCreated>
{
    public async Task Consume(ConsumeContext<IBookCreated> context)
    {
        var book = context.Message;

        await Console.Out.WriteLineAsync($"BookCreated message with Id = {book.Id} consumed.");

        await _sender.Send(new CreateBookCommand(book));
    }
}