using Book.Application.Features.BookFeatures.Commands.UpdateRating;
using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Book.Application.Consumers;

public class BookRatingUpdatedConsumer(ISender _sender) : IConsumer<IBookRatingUpdated>
{
    public async Task Consume(ConsumeContext<IBookRatingUpdated> context)
    {
        var book = context.Message;
        
        await Console.Out.WriteLineAsync($"BookRatingUpdated message with Id = {book.Id} consumed.");

        await _sender.Send(new UpdateBookRatingCommand(book));
    }
}