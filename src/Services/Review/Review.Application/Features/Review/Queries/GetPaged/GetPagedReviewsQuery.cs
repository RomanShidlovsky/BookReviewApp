using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Queries;

namespace Review.Application.Features.Review.Queries.GetPaged;

public sealed record GetPagedReviewsQuery(
    string FilterQueryString,
    string OrderByQueryString,
    int PageNumber,
    int PageSize)
    : IQuery<ReviewResponseDto>;