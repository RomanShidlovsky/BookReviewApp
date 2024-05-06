namespace Review.Application.DTOs.RequestDTOs;

public sealed record CreateReviewDto(
    int BookId,
    int UserId,
    int Rating,
    string? Text);
