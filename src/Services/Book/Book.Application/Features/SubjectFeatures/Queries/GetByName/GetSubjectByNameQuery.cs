using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.SubjectFeatures.Queries.GetByName;

public sealed record GetSubjectByNameQuery(string Name) : ISingleQuery<SubjectResponseDto>; 