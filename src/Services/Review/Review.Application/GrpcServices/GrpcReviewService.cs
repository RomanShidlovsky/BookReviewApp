using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Review.Application.Features.ReviewFeatures.Queries.GetBookReviews;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;

namespace Review.Application.GrpcServices;

public class GrpcReviewService(IUnitOfWork _unitOfWork, IMapper _mapper, ILogger<GrpcReviewService> _logger) : ReviewService.ReviewServiceBase
{
    public override async Task<GetBookReviewsResponse> GetBookReviews(GetBookReviewsRequest request,
        ServerCallContext context)
    {
        _logger.LogInformation("GrpcReviewService: getting review of book with id = {id}", request.BookId);
        
        var reviews = await _unitOfWork.ReviewRepository.GetBookReviewsAsync(request.BookId, context.CancellationToken);
        var criticReviews = await _unitOfWork.CriticReviewRepository.GetBookReviewsAsync(request.BookId, context.CancellationToken);

        _logger.LogInformation("GrpcReviewService: sending review of book with id = {id}", request.BookId);
        
        return new GetBookReviewsResponse
        {
            Reviews = { _mapper.Map<List<Review>>(reviews) },
            CriticReviews = {  _mapper.Map<List<Review>>(criticReviews) }
        };
    }
}