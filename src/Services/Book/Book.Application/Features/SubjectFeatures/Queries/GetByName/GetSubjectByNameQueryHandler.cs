using AutoMapper;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Queries.GetByName;

public class GetSubjectByNameQueryHandler(ISubjectRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetSubjectByNameQuery, SubjectResponseDto>
{
    public async Task<Response<SubjectResponseDto>> Handle(GetSubjectByNameQuery request, CancellationToken cancellationToken)
    {
        var subject = await _repository.GetByNameAsync(request.Name, cancellationToken);

        return subject is null
            ? Response.Failure<SubjectResponseDto>(DomainErrors.Subject.SubjectNotFoundByName)
            : _mapper.Map<SubjectResponseDto>(subject);
    }
}