namespace Review.Application.DTOs.RequestDTOs;

public sealed record UpdateReviewDto(
    int Id,
    int Rating,
    string? Text);