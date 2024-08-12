using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.BookFeatures.Queries.GetBooks;

public sealed record GetBooksQuery(
    string FilterQueryString,
    string OrderByQueryString,
    int[]? SelectedSubjects,
    int[]? SelectedLanguages,
    int[]? SelectedAuthors,
    int PageNumber,
    int PageSize) 
    : IQuery<BookResponseDto>;