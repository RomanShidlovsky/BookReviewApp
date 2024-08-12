using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.DTOs.Review.ResponseDTOs;
using Book.Application.GrpcServices;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Queries.GetById;

public class GetBookByIdQueryHandler(
    IBookRepository _repository, 
    IMapper _mapper, 
    ReviewService.ReviewServiceClient _client,
    ILogger<GetBookByIdQueryHandler> _logger)
    : ISingleQueryHandler<GetBookByIdQuery, BookWithReviewsResponseDto>
{
    public async Task<Response<BookWithReviewsResponseDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (book is null)
        {
            return Response.Failure<BookWithReviewsResponseDto>(DomainErrors.Book.BookNotFoundById);
        }
        
        var response = _mapper.Map<BookWithReviewsResponseDto>(book);
            
        var allReviews = await GetBookReviewsAsync(request.Id, cancellationToken);

        response.Reviews = _mapper.Map<List<ReviewResponseDto>>(allReviews.Reviews.ToList());
        response.CriticReviews = _mapper.Map<List<ReviewResponseDto>>(allReviews.CriticReviews.ToList());

        return response;
    }

    private async Task<GetBookReviewsResponse> GetBookReviewsAsync(int bookId, CancellationToken cancellationToken)
    {
        var request = new GetBookReviewsRequest { BookId = bookId };

        _logger.LogInformation("Send GetBookReviewsRequest for bookId = {bookId}", bookId);
        
        var response = await _client.GetBookReviewsAsync(request, cancellationToken: cancellationToken);
        
        _logger.LogInformation("Receive GetBookReviewsResponse for bookId = {bookId}", bookId);

        return response;
    }
}