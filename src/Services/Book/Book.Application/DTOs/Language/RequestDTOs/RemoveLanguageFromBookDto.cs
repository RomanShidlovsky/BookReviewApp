namespace Book.Application.DTOs.Language.RequestDTOs;

public sealed record RemoveLanguageFromBookDto(int LanguageId, int BookId);