using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Dislike;

public class DislikeReviewCommandHandler(IUnitOfWork _unitOfWork) : ICommandHandler<DislikeReviewCommand>
{
    public async Task<Response> Handle(DislikeReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReviewRepository;
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.ReviewId, cancellationToken);

        if (review is null)
        {
            return Response.Failure(DomainErrors.Review.ReviewNotFoundById);
        }
        
        var dislikeExists = await repository.DislikeExists(dto.ReviewId, dto.UserId, cancellationToken);

        if (dislikeExists)
        {
            return Response.Failure(DomainErrors.Review.AlreadyDisliked);
        }

        await repository.Dislike(dto.ReviewId, dto.UserId, cancellationToken);

        return Response.Success();
    }
}