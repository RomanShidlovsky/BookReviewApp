using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Dislike;

public class DislikeCriticReviewCommandHandler(IUnitOfWork _unitOfWork) : ICommandHandler<DislikeCriticReviewCommand>
{
    public async Task<Response> Handle(DislikeCriticReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.CriticReviewRepository;
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