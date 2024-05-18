using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Undislike;

public class UndislikeCommandHandler(IUnitOfWork _unitOfWork) : ICommandHandler<UndislikeCommand>
{
    public async Task<Response> Handle(UndislikeCommand request, CancellationToken cancellationToken)
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
            await repository.Undislike(dto.ReviewId, dto.UserId, cancellationToken);
        }
        
        return Response.Success();
    }
}