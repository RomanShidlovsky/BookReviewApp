using Review.Domain.Entities;

namespace Review.Application.DTOs.ResponseDTOs;

public sealed record ReviewResponseDto(
    int Id,
    int BookId,
    int UserId,
    int Rating,
    string? Text,
    int? Likes,
    int? Dislikes,
    IEnumerable<Comment>? Comments);