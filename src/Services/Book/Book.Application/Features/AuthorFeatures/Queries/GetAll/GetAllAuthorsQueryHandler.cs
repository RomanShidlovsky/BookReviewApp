using AutoMapper;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Queries.GetAll;

public class GetAllAuthorsQueryHandler(IAuthorRepository _repository, IMapper _mapper) 
    : IQueryHandler<GetAllAuthorsQuery, AuthorResponseDto>
{
    public async Task<Response<IEnumerable<AuthorResponseDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<AuthorResponseDto>>(authors);
    }
}