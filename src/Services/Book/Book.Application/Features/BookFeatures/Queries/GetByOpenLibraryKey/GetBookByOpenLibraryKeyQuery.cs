using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.BookFeatures.Queries.GetByOpenLibraryKey;

public sealed record GetBookByOpenLibraryKeyQuery(string OpenLibraryKey) : ISingleQuery<BookResponseDto>;
