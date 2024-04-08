using Book.Application.Interfaces.Commands;

namespace Book.Application.Features.SubjectFeatures.Commands.Delete;

public sealed record DeleteSubjectCommand(int Id) : IDeleteCommand;