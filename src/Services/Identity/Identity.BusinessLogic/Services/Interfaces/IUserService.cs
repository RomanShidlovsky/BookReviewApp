using Identity.BusinessLogic.DTOs.RequestDTOs;
using Identity.BusinessLogic.DTOs.RequestDTOs.User;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Shared;
using Shared.Wrappers;

namespace Identity.BusinessLogic.Services.Interfaces;

public interface IUserService
{
    Task<Response<UserDto>> CreateUserAsync(RegisterUserDto dto, CancellationToken cancellationToken);
    Task<Response<UserDto>> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken);
    Task<Response> DeleteUserByIdAsync(int id);
    Task<Response> AddUserToRoleAsync(AddUserToRoleDto dto);
    Task<Response> RemoveUserFromRoleAsync(RemoveUserFromRoleDto dto);
    Task<Response<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<Response<UserDto>> GetUserByIdAsync(int id);
    Task<Response<UserDto>> GetUserByUserNameAsync(string userName);
}