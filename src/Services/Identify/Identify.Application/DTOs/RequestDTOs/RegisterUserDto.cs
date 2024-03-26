namespace Identify.Application.DTOs.RequestDTOs;

public sealed record RegisterUserDto(
    string UserName,
    string Email,
    string Password);