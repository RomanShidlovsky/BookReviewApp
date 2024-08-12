using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Like;

public class LikeCriticReviewCommandHandler(IUnitOfWork _unitOfWork) : ICommandHandler<LikeCriticReviewCommand>
{
    public async Task<Response> Handle(LikeCriticReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.CriticReviewRepository;
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.ReviewId, cancellationToken);

        if (review is null)
        {
            return Response.Failure(DomainErrors.Review.ReviewNotFoundById);
        }
        
        var likeExists = await repository.LikeExists(dto.ReviewId, dto.UserId, cancellationToken);

        if (likeExists)
        {
            return Response.Failure(DomainErrors.Review.AlreadyLiked);
        }

        await repository.Like(dto.ReviewId, dto.UserId, cancellationToken);

        return Response.Success();
    }
}