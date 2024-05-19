using MassTransit;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.DTOs.EventBus;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Response = Shared.Wrappers.Response;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Delete;

public class DeleteCriticReviewCommandHandler(IUnitOfWork _unitOfWork, IPublishEndpoint _publishEndpoint) 
    : IDeleteCommandHandler<DeleteCriticReviewCommand>
{
    public async Task<Response> Handle(DeleteCriticReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.CriticReviewRepository;

        var review = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (review is null)
        {
            return Response.Failure(DomainErrors.Review.ReviewNotFoundById);
        }

        var book = await _unitOfWork.BookRepository
            .GetByIdAsync(review.BookId.ToString(), cancellationToken);

        if (book is null)
        {
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        }

        var bookReviews = await repository.GetAsync(r => r.BookId.Equals(review.BookId), 
            cancellationToken);
        
        var ratingsSum = bookReviews.Sum(r => r.Rating) - review.Rating;
        var reviewsCount = bookReviews.Count - 1;
        var averageRating = (double)ratingsSum / reviewsCount;

        book.AverageCriticRating = averageRating;
        
        await repository.DeleteAsync(review, cancellationToken);
        await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
        
        await _publishEndpoint.Publish<IBookRatingUpdated>(new BookRatingUpdated(review.BookId, book.AverageCriticRating),
            cancellationToken);

        await Console.Out.WriteLineAsync($"BookRatingUpdated message with BookId = {review.BookId} published.");
        
        return Response.Success();
    }
}