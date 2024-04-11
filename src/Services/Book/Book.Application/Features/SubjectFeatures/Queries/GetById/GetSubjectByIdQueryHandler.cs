using AutoMapper;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Queries.GetById;

public class GetSubjectByIdQueryHandler(ISubjectRepository _repository, IMapper _mapper)
    : ISingleQueryHandler<GetSubjectByIdQuery, SubjectResponseDto>
{
    public async Task<Response<SubjectResponseDto>> Handle(GetSubjectByIdQuery request, CancellationToken cancellationToken)
    {
        var subject = await _repository.GetByIdAsync(request.Id, cancellationToken);

        return subject is null
            ? Response.Failure<SubjectResponseDto>(DomainErrors.Subject.SubjectNotFoundById)
            : _mapper.Map<SubjectResponseDto>(subject);
    }
}