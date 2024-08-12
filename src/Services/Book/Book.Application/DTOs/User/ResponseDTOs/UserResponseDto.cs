namespace Book.Application.DTOs.User.ResponseDTOs;

public sealed record UserResponseDto (
    int Id,
    string UserName,
    string? ImageUrl);