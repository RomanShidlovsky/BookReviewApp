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
    : IUpdateCommandHandler<UpdateBookCriticRatingCommand, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(UpdateBookCriticRatingCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();
        var dto = request.Dto;

        var book = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (book is null)
        {
            return Response.Failure<BookResponseDto>(DomainErrors.Book.BookNotFoundById);
        }

        book.AverageCriticRating = dto.CriticRating;
        
        repository.Update(book);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return _mapper.Map<BookResponseDto>(book);
    }
}