namespace Identify.Application.DTOs.ResponseDTOs;

public sealed class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public IList<string> Roles { get; set; } 
}