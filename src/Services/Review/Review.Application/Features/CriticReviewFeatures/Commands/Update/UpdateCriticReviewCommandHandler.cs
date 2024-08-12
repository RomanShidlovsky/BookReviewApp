using AutoMapper;
using MassTransit;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.DTOs.EventBus;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.ReviewFeatures.Commands.Update;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Response = Shared.Wrappers.Response;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Update;

public class UpdateCriticReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IPublishEndpoint _publishEndpoint)
    : IUpdateCommandHandler<UpdateCriticReviewCommand, ReviewResponseDto>
{
    public async Task<Shared.Wrappers.Response<ReviewResponseDto>> Handle(UpdateCriticReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.CriticReviewRepository;
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (review is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Review.ReviewNotFoundById);
        }
        
        var book = await _unitOfWork.BookRepository
            .GetByIdAsync(review.BookId.ToString(), cancellationToken);

        if (book is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Book.BookNotFoundById);
        }
        
        var bookReviews = await repository.GetAsync(r => r.BookId.Equals(review.BookId), 
            cancellationToken);

        var ratingsSum = bookReviews.Sum(r => r.Rating) - review.Rating + dto.Rating;
        var reviewsCount = bookReviews.Count;
        var averageRating = (double)ratingsSum / reviewsCount;

        book.AverageCriticRating = averageRating;
        
        _mapper.Map(dto, review);
        
        await repository.UpdateAsync(review, cancellationToken);
        await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);
        
        await _publishEndpoint.Publish<IBookRatingUpdated>(new BookRatingUpdated(review.BookId, book.AverageCriticRating),
            cancellationToken);

        await Console.Out.WriteLineAsync($"BookRatingUpdated message with BookId = {review.BookId} published.");

        return _mapper.Map<ReviewResponseDto>(review);
    }
}