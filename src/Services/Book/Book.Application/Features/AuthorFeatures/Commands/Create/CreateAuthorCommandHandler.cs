using AutoMapper;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Entities;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Commands.Create;

public class CreateAuthorCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper) 
    : ICreateCommandHandler<CreateAuthorCommand, AuthorResponseDto>
{
    public async Task<Response<AuthorResponseDto>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IAuthorRepository>();
        var dto = request.Dto;

        if (dto.OpenLibraryKey is not null)
        {
            var existingAuthor = await repository.GetByOpenLibraryKeyAsync(dto.OpenLibraryKey, cancellationToken);
            
            if (existingAuthor != null)
                return Response.Failure<AuthorResponseDto>(DomainErrors.Author.OpenLibraryKeyConflict);
        }
        
        var author = _mapper.Map<Author>(dto);

        repository.Create(author);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<AuthorResponseDto>(author);
    }
}