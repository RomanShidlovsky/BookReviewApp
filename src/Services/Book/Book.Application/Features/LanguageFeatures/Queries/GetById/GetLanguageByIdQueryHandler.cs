using AutoMapper;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.LanguageFeatures.Queries.GetById;

public class GetLanguageByIdQueryHandler(ILanguageRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetLanguageByIdQuery, LanguageResponseDto>
{
    public async Task<Response<LanguageResponseDto>> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
    {
        var language = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        return language == null 
            ? Response.Failure<LanguageResponseDto>(DomainErrors.Language.LanguageNotFoundById) 
            : _mapper.Map<LanguageResponseDto>(language);
    }
}