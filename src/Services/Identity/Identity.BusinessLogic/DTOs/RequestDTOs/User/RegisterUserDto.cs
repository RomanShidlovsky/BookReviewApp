namespace Identity.BusinessLogic.DTOs.RequestDTOs.User;

public sealed record RegisterUserDto(
    string UserName,
    string Email,
    string Password);