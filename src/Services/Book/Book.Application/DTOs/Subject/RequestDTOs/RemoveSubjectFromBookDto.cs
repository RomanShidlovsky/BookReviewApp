namespace Book.Application.DTOs.Subject.RequestDTOs;

public record RemoveSubjectFromBookDto(int SubjectId, int BookId);