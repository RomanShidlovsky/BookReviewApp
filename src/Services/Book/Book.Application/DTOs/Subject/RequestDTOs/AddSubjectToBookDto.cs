namespace Book.Application.DTOs.Subject.RequestDTOs;

public sealed record AddSubjectToBookDto(int SubjectId, int BookId);