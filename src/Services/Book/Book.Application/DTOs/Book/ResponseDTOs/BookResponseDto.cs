using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.DTOs.Subject.ResponseDTOs;

namespace Book.Application.DTOs.Book.ResponseDTOs;

public record BookResponseDto(
    int Id,
    string? OpenLibraryKey,
    string Title,
    int EditionCount,
    int PublicationYear,
    double AverageRating,
    string? ImageUrl,
    IEnumerable<AuthorResponseDto> Authors,
    IEnumerable<LanguageResponseDto> Languages,
    IEnumerable<SubjectResponseDto> Subjects);