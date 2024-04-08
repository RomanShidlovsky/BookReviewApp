using AutoMapper;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.LanguageFeatures.Queries.GetAll;

public class GetAllLanguagesQueryHandler(ILanguageRepository _repository, IMapper _mapper)
    : IQueryHandler<GetAllLanguagesQuery, LanguageResponseDto>
{
    public async Task<Response<IEnumerable<LanguageResponseDto>>> Handle(GetAllLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<LanguageResponseDto>>(languages);
    }
}