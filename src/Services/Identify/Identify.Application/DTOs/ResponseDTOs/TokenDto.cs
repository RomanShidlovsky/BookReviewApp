namespace Identify.Application.DTOs.ResponseDTOs;

public sealed record TokenDto(
    string AccessToken,
    DateTime Expires,
    string RefreshToken);