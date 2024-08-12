namespace Review.Application.DTOs.ResponseDTOs;

public sealed record BookResponseDto(
    string Id,
    string Title,
    double AverageRating,
    double AverageCriticRating,
    string? ImageUrl);

