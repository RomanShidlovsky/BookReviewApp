using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.ReviewFeatures.Queries.GetAll;
using Review.Application.Interfaces.Queries;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetAll;

public class GetAllCriticReviewsQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IQueryHandler<GetAllCriticReviewsQuery, ReviewResponseDto>
{
    public async Task<Response<IEnumerable<ReviewResponseDto>>> Handle(GetAllCriticReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.CriticReviewRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }
}