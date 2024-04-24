namespace Book.Application.DTOs.Author.RequestDTOs;

public sealed record RemoveAuthorFromBookDto(int AuthorId, int BookId);