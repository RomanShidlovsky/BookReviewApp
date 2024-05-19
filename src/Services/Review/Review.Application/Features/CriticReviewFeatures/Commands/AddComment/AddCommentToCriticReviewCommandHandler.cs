using AutoMapper;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.CriticReviewFeatures.Commands.AddComment;

public class AddCommentToCriticReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICommandHandler<AddCommentToCriticReviewCommand>
{
    public async Task<Response> Handle(AddCommentToCriticReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.CriticReviewRepository;
        var dto = request.Dto;

        var review = await repository.GetByIdAsync(dto.ReviewId, cancellationToken);

        if (review is null)
        {
            return Response.Failure(DomainErrors.Review.ReviewNotFoundById);
        }
        
        await repository.AddCommentToReviewAsync(dto.ReviewId, dto.Comment, cancellationToken);
        
        return Response.Success();
    }
}