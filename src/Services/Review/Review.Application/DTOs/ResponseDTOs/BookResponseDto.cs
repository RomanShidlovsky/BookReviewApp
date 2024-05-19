namespace Review.Application.DTOs.ResponseDTOs;

public sealed record BookResponseDto(
    int Id,
    string Title,
    double AverageRating,
    double AverageCriticRating,
    string? ImageUrl);

