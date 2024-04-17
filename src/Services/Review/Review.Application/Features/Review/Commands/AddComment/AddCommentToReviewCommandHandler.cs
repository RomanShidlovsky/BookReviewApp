using AutoMapper;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.Review.Commands.AddComment;

public class AddCommentToReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICommandHandler<AddCommentToReviewCommand>
{
    public async Task<Response> Handle(AddCommentToReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IReviewRepository>();
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.ReviewId, cancellationToken);

        if (review is null)
        {
            return Response.Failure(DomainErrors.Review.ReviewNotFoundById);
        }
        
        repository.AddCommentToReviewAsync(dto.ReviewId, dto.Comment, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Response.Success();
    }
}