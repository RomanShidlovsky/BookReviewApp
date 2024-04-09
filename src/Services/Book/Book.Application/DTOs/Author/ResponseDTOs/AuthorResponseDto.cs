namespace Book.Application.DTOs.Author.ResponseDTOs;

public sealed record AuthorResponseDto(
    int Id,
    string? OpenLibraryKey,
    string FirstName,
    string LastName,
    string FullName,
    DateOnly? BirthDate,
    DateOnly? DeathDate,
    string? Biography,
    string? ImageUrl);