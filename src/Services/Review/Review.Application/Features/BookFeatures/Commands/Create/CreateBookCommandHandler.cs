using AutoMapper;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Entities;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.BookFeatures.Commands.Create;

public class CreateBookCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICreateCommandHandler<CreateBookCommand, BookResponseDto>
{
    public async Task<Response<BookResponseDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();
        var dto = request.Dto;

        var bookExists = await repository.ExistsAsync(dto.Id, cancellationToken);

        if (bookExists)
        {
            return Response.Failure<BookResponseDto>(DomainErrors.Book.AlreadyExists);
        }

        var book = _mapper.Map<Book>(dto);
        
        repository.Create(book);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<BookResponseDto>(book);
    }
}