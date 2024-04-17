using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;
using Review.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.Review.Queries.GetPaged;

public class GetPagedReviewsQueryHandler(IReviewRepository _repository, IMapper _mapper)
    : IQueryHandler<GetPagedReviewsQuery, ReviewResponseDto>
{
    public async Task<Response<IEnumerable<ReviewResponseDto>>> Handle(GetPagedReviewsQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _repository.GetPagedAsync(request.PageNumber, request.PageSize,
            request.FilterQueryString, request.OrderByQueryString, cancellationToken);

        return _mapper.Map<List<ReviewResponseDto>>(reviews);
    }
}