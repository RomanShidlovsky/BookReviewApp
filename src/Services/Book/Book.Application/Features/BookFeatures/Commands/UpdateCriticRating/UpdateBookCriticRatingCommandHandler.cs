using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Features.BookFeatures.Commands.UpdateRating;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Commands.UpdateCriticRating;

public class UpdateBookCriticRatingCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICommandHandler<UpdateBookCriticRatingCommand>
{
    public async Task<Response> Handle(UpdateBookCriticRatingCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();
        var dto = request.Dto;

        var bookExists = await repository.ExistsAsync(dto.Id, cancellationToken);

        if (!bookExists)
        {
            return Response.Failure<BookResponseDto>(DomainErrors.Book.BookNotFoundById);
        }
        
        await repository.UpdateCriticRatingAsync(dto.Id, dto.CriticRating, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Response.Success();
    }
}