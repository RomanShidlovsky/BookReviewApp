using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.ReviewFeatures.Queries.GetBookReviews;
using Review.Application.Interfaces.Queries;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Queries.GetUserReviews;

public class GetUserReviewsQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IQueryHandler<GetUserReviewsQuery, ReviewResponseDto>
{
    public async Task<Response<IEnumerable<ReviewResponseDto>>> Handle(GetUserReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.ReviewRepository.GetUserReviewsAsync(request.UserId,
            cancellationToken);

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }
}