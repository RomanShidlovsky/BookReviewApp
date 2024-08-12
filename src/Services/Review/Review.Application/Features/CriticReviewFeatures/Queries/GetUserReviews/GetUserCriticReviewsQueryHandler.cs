using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.CriticReviewFeatures.Queries.GetBookReviews;
using Review.Application.Interfaces.Queries;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetUserReviews;

public class GetUserCriticReviewsQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IQueryHandler<GetUserCriticReviewsQuery, ReviewResponseDto>
{
    public async Task<Response<IEnumerable<ReviewResponseDto>>> Handle(GetUserCriticReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.CriticReviewRepository.GetUserReviewsAsync(request.UserId,
            cancellationToken);

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }
}