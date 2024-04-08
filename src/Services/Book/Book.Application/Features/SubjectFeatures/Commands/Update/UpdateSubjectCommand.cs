using Book.Application.DTOs.Subject.RequestDTOs;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.SubjectFeatures.Commands.Update;

public sealed record UpdateSubjectCommand(UpdateSubjectDto Dto) : IUpdateCommand<SubjectResponseDto>;
