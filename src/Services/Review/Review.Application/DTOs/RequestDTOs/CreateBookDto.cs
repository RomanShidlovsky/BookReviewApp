namespace Review.Application.DTOs.RequestDTOs;

public sealed record CreateBookDto(
    int Id,
    string Title,
    string? ImageUrl);