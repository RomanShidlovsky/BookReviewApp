using Book.Application.DTOs.Subject.RequestDTOs;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.SubjectFeatures.Commands.Create;

public sealed record CreateSubjectCommand(CreateSubjectDto Dto) : ICreateCommand<SubjectResponseDto>;