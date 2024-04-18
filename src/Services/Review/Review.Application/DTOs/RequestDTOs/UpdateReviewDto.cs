namespace Review.Application.DTOs.RequestDTOs;

public sealed record UpdateReviewDto(
    string Id,
    int Rating,
    string? Text);