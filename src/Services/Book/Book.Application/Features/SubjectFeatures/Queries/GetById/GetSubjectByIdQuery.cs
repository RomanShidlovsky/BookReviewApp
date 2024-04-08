using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.SubjectFeatures.Queries.GetById;

public sealed record GetSubjectByIdQuery(int Id) : ISingleQuery<SubjectResponseDto>;