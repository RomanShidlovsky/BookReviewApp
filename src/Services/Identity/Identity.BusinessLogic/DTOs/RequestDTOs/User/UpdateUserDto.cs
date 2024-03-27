namespace Identity.BusinessLogic.DTOs.RequestDTOs.User;

public sealed record UpdateUserDto(
    int Id,
    string UserName,
    string Email);