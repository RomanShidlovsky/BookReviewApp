using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Update;

public class UpdateReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateReviewCommand, ReviewResponseDto>
{
    public async Task<Response<ReviewResponseDto>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReviewRepository;
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (review is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Review.ReviewNotFoundById);
        }
        
        /*var book = await _unitOfWork.BookRepository
            .GetByIdAsync(review.BookId, cancellationToken);

        if (book is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Book.BookNotFoundById);
        }

        var ratingsSum = book.Reviews.Sum(r => r.Rating) - review.Rating + dto.Rating;
        var reviewsCount = book.Reviews.Count;
        var averageRating = ratingsSum / reviewsCount;

        book.AverageRating = averageRating;*/
        
        _mapper.Map(dto, review);
        
        await repository.UpdateAsync(review, cancellationToken);

        return _mapper.Map<ReviewResponseDto>(review);
    }
}