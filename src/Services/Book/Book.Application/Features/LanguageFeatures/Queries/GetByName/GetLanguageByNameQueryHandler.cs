using AutoMapper;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.LanguageFeatures.Queries.GetByName;

public class GetLanguageByNameQueryHandler(ILanguageRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetLanguageByNameQuery, LanguageResponseDto>
{
    public async Task<Response<LanguageResponseDto>> Handle(GetLanguageByNameQuery request, CancellationToken cancellationToken)
    {
        var language = await _repository.GetByNameAsync(request.Name, cancellationToken);

        return language == null
            ? Response.Failure<LanguageResponseDto>(DomainErrors.Language.LanguageNotFoundByName)
            : _mapper.Map<LanguageResponseDto>(language);
    }
}