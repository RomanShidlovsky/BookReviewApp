using AutoMapper;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Entities;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Commands.Update;

public class UpdateAuthorCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateAuthorCommand, AuthorResponseDto>
{
    public async Task<Response<AuthorResponseDto>> Handle(UpdateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IAuthorRepository>();
        var dto = request.Dto;
        
        var author = await repository.GetByIdAsync(dto.Id, cancellationToken);
        
        if (author == null)
            return Response.Failure<AuthorResponseDto>(DomainErrors.Author.AuthorNotFoundById);
        
        if (dto.OpenLibraryKey != null)
        {
            var openLibraryKeyAuthor =
                await repository.GetAsync(a => a.Id != dto.Id && a.IsOpenLibraryKey(dto.OpenLibraryKey),
                    cancellationToken);
            
            if (openLibraryKeyAuthor.Count != 0)
                return Response.Failure<AuthorResponseDto>(DomainErrors.Author.OpenLibraryKeyConflict);
        }
        
        _mapper.Map(dto, author);
        
        repository.Update(author);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<AuthorResponseDto>(author);
    }
}