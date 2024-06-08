using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.BookFeatures.Queries.GetRecommendedBooks;

public sealed record GetRecommendedBooksQuery(int BookId, int Count) : IQuery<BookResponseDto>;