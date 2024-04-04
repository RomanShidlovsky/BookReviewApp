namespace Book.Application.DTOs.Author.RequestDTOs;

public sealed record AddAuthorToBookDto(int AuthorId, int BookId);