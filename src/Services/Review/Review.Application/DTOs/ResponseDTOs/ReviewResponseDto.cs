using Review.Domain.Entities;

namespace Review.Application.DTOs.ResponseDTOs;

public sealed record ReviewResponseDto(
    string Id,
    int BookId,
    int UserId,
    int Rating,
    string? Text,
    IEnumerable<int> LikeUserIds,
    IEnumerable<int> DislikeUserIds,
    IEnumerable<Comment>? Comments,
    UserResponseDto User,
    BookResponseDto Book);