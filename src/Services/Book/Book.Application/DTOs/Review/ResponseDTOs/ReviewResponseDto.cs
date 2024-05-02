namespace Book.Application.DTOs.Review.ResponseDTOs;

public sealed record ReviewResponseDto(
    string Id,
    int BookId,
    int UserId,
    int Rating,
    string? Text,
    int? Likes,
    int? Dislikes);