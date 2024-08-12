using Book.Application.DTOs.User.ResponseDTOs;

namespace Book.Application.DTOs.Review.ResponseDTOs;

public sealed record ReviewResponseDto(
    string Id,
    int BookId,
    int UserId,
    int Rating,
    string? Text,
    IEnumerable<int> LikeUserIds,
    IEnumerable<int> DislikeUserIds,
    UserResponseDto User);