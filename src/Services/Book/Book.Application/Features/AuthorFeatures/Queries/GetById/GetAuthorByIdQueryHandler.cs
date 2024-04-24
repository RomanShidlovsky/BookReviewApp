using AutoMapper;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Queries.GetById;

public class GetAuthorByIdQueryHandler(IAuthorRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetAuthorByIdQuery, AuthorResponseDto>
{
    public async Task<Response<AuthorResponseDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        return author is null 
            ? Response.Failure<AuthorResponseDto>(DomainErrors.Author.AuthorNotFoundById) 
            : _mapper.Map<AuthorResponseDto>(author);
    }
}