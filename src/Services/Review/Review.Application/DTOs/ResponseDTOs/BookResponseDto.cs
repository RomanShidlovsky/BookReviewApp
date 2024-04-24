namespace Review.Application.DTOs.ResponseDTOs;

public sealed record BookResponseDto(
    int Id,
    string Title,
    int AverageRating,
    string? ImageUrl);

