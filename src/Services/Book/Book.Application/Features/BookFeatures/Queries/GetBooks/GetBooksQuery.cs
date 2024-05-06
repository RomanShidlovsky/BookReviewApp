using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.BookFeatures.Queries.GetBooks;

public sealed record GetBooksQuery(
    string FilterQueryString,
    string OrderByQueryString,
    int PageNumber,
    int PageSize) 
    : IQuery<BookResponseDto>;