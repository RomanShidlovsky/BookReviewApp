namespace Identify.Application.DTOs.RequestDTOs;

public sealed record UpdateUserDto(
    int Id,
    string UserName,
    string Email);