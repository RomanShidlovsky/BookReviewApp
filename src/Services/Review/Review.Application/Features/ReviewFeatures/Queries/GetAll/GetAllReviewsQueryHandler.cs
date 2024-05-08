using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Queries.GetAll;

public class GetAllReviewsQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IQueryHandler<GetAllReviewsQuery, ReviewResponseDto>
{
    public async Task<Response<IEnumerable<ReviewResponseDto>>> Handle(GetAllReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.ReviewRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }
}