namespace Book.Application.DTOs.Author.RequestDTOs;

public sealed record CreateAuthorDto(
    string? OpenLibraryKey,
    string FirstName,
    string LastName,
    string FullName,
    DateOnly? BirthDate,
    DateOnly? DeathDate,
    string? Biography,
    string? ImageUrl);