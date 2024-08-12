namespace Book.Application.DTOs.Book.RequestDTOs;

public sealed record CreateBookDto(
    string? OpenLibraryKey,
    string Title,
    int EditionCount,
    int PublicationYear);