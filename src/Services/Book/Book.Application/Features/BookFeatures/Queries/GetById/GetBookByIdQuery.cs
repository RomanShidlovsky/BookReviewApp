using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Queries;

namespace Book.Application.Features.BookFeatures.Queries.GetById;

public sealed record GetBookByIdQuery(int Id) : ISingleQuery<BookWithReviewsResponseDto>;