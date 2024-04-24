using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Queries.GetPaged;

public class GetPagedReviewsQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IQueryHandler<GetPagedReviewsQuery, ReviewResponseDto>
{
    public async Task<Response<IEnumerable<ReviewResponseDto>>> Handle(GetPagedReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _unitOfWork.ReviewRepository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }
}