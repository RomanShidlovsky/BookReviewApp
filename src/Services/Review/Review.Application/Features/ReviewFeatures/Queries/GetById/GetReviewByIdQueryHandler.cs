using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Queries.GetById;

public class GetReviewByIdQueryHandler(IReviewRepository _repository, IMapper _mapper) 
    : ISingleQueryHandler<GetReviewByIdQuery, ReviewResponseDto>
{
    public async Task<Response<ReviewResponseDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _repository.GetByIdAsync(request.Id, cancellationToken);

        return review is null
            ? Response.Failure<ReviewResponseDto>(DomainErrors.Review.ReviewNotFoundById)
            : _mapper.Map<ReviewResponseDto>(review);
    }
}