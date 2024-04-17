using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Update;

public class UpdateReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateReviewCommand, ReviewResponseDto>
{
    public async Task<Response<ReviewResponseDto>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IReviewRepository>();
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (review is null)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Review.ReviewNotFoundById);
        }

        _mapper.Map(dto, review);
        
        repository.Update(review);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<ReviewResponseDto>(review);
    }
}