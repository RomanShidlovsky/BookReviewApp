using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Entities;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Create;

public class CreateReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICreateCommandHandler<CreateReviewCommand, ReviewResponseDto>
{
    public async Task<Response<ReviewResponseDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReviewRepository;
        var dto = request.Dto;

        /*var existingUser = await _unitOfWork.UserRepository
            .GetByIdAsync(dto.UserId, cancellationToken);

        if (existingUser is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.User.UserNotFoundById);
        }

        var book = await _unitOfWork.BookRepository
            .GetByIdAsync(dto.BookId, cancellationToken);

        if (book is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Book.BookNotFoundById);
        }

        var ratingsSum = book.Reviews.Sum(review => review.Rating) + dto.Rating;
        var reviewsCount = book.Reviews.Count + 1;
        var averageRating = ratingsSum / reviewsCount;

        book.AverageRating = averageRating;*/
        
        var review = _mapper.Map<ReviewEntity>(dto);
        
        await repository.CreateAsync(review, cancellationToken);

        return _mapper.Map<ReviewResponseDto>(review);
    }
}