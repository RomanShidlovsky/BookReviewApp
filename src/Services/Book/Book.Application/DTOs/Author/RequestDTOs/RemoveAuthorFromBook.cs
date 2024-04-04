namespace Book.Application.DTOs.Author.RequestDTOs;

public sealed record RemoveAuthorFromBook(int AuthorId, int BookId);