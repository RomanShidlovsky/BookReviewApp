using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Delete;

public class DeleteReviewCommandHandler(IUnitOfWork _unitOfWork) 
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

        /*var book = await _unitOfWork.BookRepository
            .GetByIdAsync(review.BookId.ToString(), cancellationToken);

        if (book is null)
        {
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        }

        var ratingsSum = book.Reviews.Sum(r => r.Rating) - review.Rating;
        var reviewsCount = book.Reviews.Count - 1;
        var averageRating = ratingsSum / reviewsCount;

        book.AverageRating = averageRating;*/
        
        await repository.DeleteAsync(review, cancellationToken);
        
        return Response.Success();
    }
}