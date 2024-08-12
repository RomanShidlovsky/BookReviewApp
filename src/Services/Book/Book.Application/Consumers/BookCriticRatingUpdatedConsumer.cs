using Book.Application.Features.BookFeatures.Commands.UpdateCriticRating;
using MassTransit;
using MediatR;
using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Book.Application.Consumers;

public class BookCriticRatingUpdatedConsumer(ISender _sender) : IConsumer<IBookCriticRatingUpdated>
{
    public async Task Consume(ConsumeContext<IBookCriticRatingUpdated> context)
    {
        var book = context.Message;
        
        await Console.Out.WriteLineAsync($"BookRatingUpdated message with Id = {book.Id} consumed.");

        await _sender.Send(new UpdateBookCriticRatingCommand(book));
    }
}