namespace Book.Application.DTOs.Book.RequestDTOs;

public sealed record UpdateBookDto(
    int Id,
    string? OpenLibraryKey,
    string Title,
    int EditionCount,
    int PublicationYear,
    string? ImageUrl);