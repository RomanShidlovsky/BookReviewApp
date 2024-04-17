namespace Review.Application.DTOs.ResponseDTOs;

public sealed record UserResponseDto(
    int Id,
    string UserName,
    string? ImageUrl);