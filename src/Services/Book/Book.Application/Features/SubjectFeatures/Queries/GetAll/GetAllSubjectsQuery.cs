using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.SubjectFeatures.Queries.GetAll;

public sealed record GetAllSubjectsQuery : IQuery<SubjectResponseDto>;