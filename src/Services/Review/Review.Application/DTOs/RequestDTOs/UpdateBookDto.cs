namespace Review.Application.DTOs.RequestDTOs;

public sealed record UpdateBookDto(
    int Id,
    string Title,
    string? ImageUrl);