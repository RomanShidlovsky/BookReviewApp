using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.BookFeatures.Queries.GetAll;

public sealed record GetAllBooksQuery : IQuery<BookResponseDto>;