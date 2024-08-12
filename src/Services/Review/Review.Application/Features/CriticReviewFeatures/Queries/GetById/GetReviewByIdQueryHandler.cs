using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Features.ReviewFeatures.Queries.GetById;
using Review.Application.Interfaces.Queries;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.CriticReviewFeatures.Queries.GetById;

public class GetReviewByIdQueryHandler(IUnitOfWork _unitOfWork, IMapper _mapper) 
    : ISingleQueryHandler<GetCriticReviewByIdQuery, ReviewResponseDto>
{
    public async Task<Response<ReviewResponseDto>> Handle(GetCriticReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var review = await _unitOfWork.CriticReviewRepository.GetByIdAsync(request.Id, cancellationToken);

        return review is null
            ? Response.Failure<ReviewResponseDto>(DomainErrors.Review.ReviewNotFoundById)
            : _mapper.Map<ReviewResponseDto>(review);
    }
}