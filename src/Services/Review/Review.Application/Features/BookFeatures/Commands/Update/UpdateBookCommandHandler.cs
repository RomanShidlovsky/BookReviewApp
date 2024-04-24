using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.BookFeatures.Commands.Update;

public class UpdateBookCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateBookCommand, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.BookRepository;
        var dto = request.Dto;

        var book = await repository.GetByIdAsync(dto.Id.ToString(), cancellationToken);

        if (book is null)
        {
            return Response.Failure<BookResponseDto>(DomainErrors.Book.BookNotFoundById);
        }

        _mapper.Map(dto, book);
        
        await repository.UpdateAsync(book, cancellationToken);
        

        return _mapper.Map<BookResponseDto>(book);
    }
}