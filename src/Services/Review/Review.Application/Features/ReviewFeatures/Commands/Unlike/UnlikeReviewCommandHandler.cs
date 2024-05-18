using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Unlike;

public class UnlikeReviewCommandHandler(IUnitOfWork _unitOfWork) : ICommandHandler<UnlikeReviewCommand>
{
    public async Task<Response> Handle(UnlikeReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReviewRepository;
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.ReviewId, cancellationToken);

        if (review is null)
        {
            return Response.Failure(DomainErrors.Review.ReviewNotFoundById);
        }
        
        var likeExists = await repository.LikeExists(dto.ReviewId, dto.UserId, cancellationToken);
        
        if (likeExists)
        {
            await repository.Unlike(dto.ReviewId, dto.UserId, cancellationToken);
        }
        
        return Response.Success();
    }
}