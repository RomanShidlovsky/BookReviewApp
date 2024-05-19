using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.ReviewFeatures.Queries.GetBookReviews;
using Review.Application.Interfaces.Queries;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetBookReviews;

public class GetBookCriticReviewsQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IQueryHandler<GetBookCriticReviewsQuery, ReviewResponseDto>
{
    public async Task<Response<IEnumerable<ReviewResponseDto>>> Handle(GetBookCriticReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.CriticReviewRepository.GetBookReviewsAsync(request.BookId,
            cancellationToken);

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }
}