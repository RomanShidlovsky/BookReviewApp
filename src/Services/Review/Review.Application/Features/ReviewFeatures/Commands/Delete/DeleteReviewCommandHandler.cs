using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.ReviewFeatures.Commands.Delete;

public class DeleteReviewCommandHandler(IUnitOfWork _unitOfWork) 
    : IDeleteCommandHandler<DeleteReviewCommand>
{
    public async Task<Response> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IReviewRepository>();

        var review = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (review is null)
        {
            return Response.Failure(DomainErrors.Review.ReviewNotFoundById);
        }
        
        repository.Delete(review);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Response.Success();
    }
}