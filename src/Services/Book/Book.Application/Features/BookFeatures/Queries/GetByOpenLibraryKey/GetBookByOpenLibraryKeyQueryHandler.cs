using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.GrpcServices;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Queries.GetByOpenLibraryKey;

public class GetBookByOpenLibraryKeyQueryHandler(
    IBookRepository _repository, 
    IMapper _mapper,
    ReviewService.ReviewServiceClient _client,
    ILogger<GetBookByOpenLibraryKeyQueryHandler> _logger)
    : ISingleQueryHandler<GetBookByOpenLibraryKeyQuery, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(GetBookByOpenLibraryKeyQuery request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByOpenLibraryKeyAsync(request.OpenLibraryKey, cancellationToken);

        return _mapper.Map<BookResponseDto>(book);
    }
    
    private async Task<IEnumerable<Review>> GetBookReviewsAsync(int bookId, CancellationToken cancellationToken)
    {
        var request = new GetBookReviewsRequest { BookId = bookId };

        _logger.LogInformation("Send GetBookReviewsRequest for bookId = {bookId}", bookId);
        
        var response = await _client.GetBookReviewsAsync(request, cancellationToken: cancellationToken);
        
        _logger.LogInformation("Receive GetBookReviewsResponse for bookId = {bookId}", bookId);

        return response.Reviews.ToList();
    }
}