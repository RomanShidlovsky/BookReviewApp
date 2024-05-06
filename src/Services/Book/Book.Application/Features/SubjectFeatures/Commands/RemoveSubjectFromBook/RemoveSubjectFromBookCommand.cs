using Book.Application.DTOs.Subject.RequestDTOs;
using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.SubjectFeatures.Commands.RemoveSubjectFromBook;

public sealed record RemoveSubjectFromBookCommand(RemoveSubjectFromBookDto Dto) : ICommand;