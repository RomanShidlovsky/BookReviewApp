using AutoMapper;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Queries;
using Book.Domain.Interfaces.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Queries.GetAll;

public class GetAllSubjectsQueryHandler(ISubjectRepository _repository, IMapper _mapper)
    : IQueryHandler<GetAllSubjectsQuery, SubjectResponseDto>
{
    public async Task<Response<IEnumerable<SubjectResponseDto>>> Handle(GetAllSubjectsQuery request, CancellationToken cancellationToken)
    {
        var subjects = await _repository.GetAllAsync(cancellationToken);

        return _mapper.Map<List<SubjectResponseDto>>(subjects);
    }
}