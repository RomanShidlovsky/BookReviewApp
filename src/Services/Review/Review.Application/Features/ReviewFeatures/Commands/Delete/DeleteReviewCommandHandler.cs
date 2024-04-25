using MassTransit;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.DTOs.EventBus;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Response = Shared.Wrappers.Response;

namespace Review.Application.Features.ReviewFeatures.Commands.Delete;

public class DeleteReviewCommandHandler(IUnitOfWork _unitOfWork, IPublishEndpoint _publishEndpoint) 
    : IDeleteCommandHandler<DeleteReviewCommand>
{
    public async Task<Response> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReviewRepository;

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

        book.AverageRating = averageRating;
        
        await repository.DeleteAsync(review, cancellationToken);
        await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
        
        await _publishEndpoint.Publish<IBookRatingUpdated>(new BookRatingUpdated(review.BookId, book.AverageRating),
            cancellationToken);

        await Console.Out.WriteLineAsync($"BookRatingUpdated message with BookId = {review.BookId} published.");
        
        return Response.Success();
    }
}