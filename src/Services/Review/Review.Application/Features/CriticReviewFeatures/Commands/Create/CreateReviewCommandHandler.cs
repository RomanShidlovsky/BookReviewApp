using AutoMapper;
using MassTransit;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.DTOs.EventBus;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Entities;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Response = Shared.Wrappers.Response;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Create;

public class CreateReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper, IPublishEndpoint _publishEndpoint)
    : ICreateCommandHandler<CreateCriticReviewCommand, ReviewResponseDto>
{
    public async Task<Shared.Wrappers.Response<ReviewResponseDto>> Handle(CreateCriticReviewCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.CriticReviewRepository;
        var dto = request.Dto;

        var existingUser = await _unitOfWork.UserRepository
            .GetByIdAsync(dto.UserId.ToString(), cancellationToken);

        if (existingUser is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.User.UserNotFoundById);
        }

        var book = await _unitOfWork.BookRepository
            .GetByIdAsync(dto.BookId.ToString(), cancellationToken);

        if (book is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Book.BookNotFoundById);
        }

        var bookReviews = await repository.GetAsync(review => review.BookId.Equals(dto.BookId), 
            cancellationToken);

        var ratingsSum = bookReviews.Sum(review => review.Rating) + dto.Rating;
        var reviewsCount = bookReviews.Count + 1;
        var averageRating = (double)ratingsSum / reviewsCount;

        book.AverageCriticRating = averageRating;

        var review = _mapper.Map<ReviewEntity>(dto);
        
        await repository.CreateAsync(review, cancellationToken);
        await _unitOfWork.BookRepository.UpdateAsync(book, cancellationToken);

        await _publishEndpoint.Publish<IBookCriticRatingUpdated>(new BookCriticRatingUpdated(review.BookId, book.AverageCriticRating),
            cancellationToken);

        await Console.Out.WriteLineAsync($"BookRatingUpdated message with BookId = {review.BookId} published.");

        return _mapper.Map<ReviewResponseDto>(review);
    }
}