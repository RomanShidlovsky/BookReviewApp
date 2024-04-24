namespace Book.Application.DTOs.Subject.RequestDTOs;

public sealed record RemoveSubjectFromBookDto(int SubjectId, int BookId);