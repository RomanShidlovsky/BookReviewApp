using AutoMapper;
using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Commands.UpdateRating;

public class UpdateBookRatingCommandHandler(IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateBookRatingCommand>
{
    public async Task<Response> Handle(UpdateBookRatingCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();
        var dto = request.Dto;

        var bookExists = await repository.ExistsAsync(dto.Id, cancellationToken);

        if (!bookExists)
        {
            return Response.Failure<BookResponseDto>(DomainErrors.Book.BookNotFoundById);
        }
        
        await repository.UpdateRatingAsync(dto.Id, dto.Rating, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Response.Success();
    }
}