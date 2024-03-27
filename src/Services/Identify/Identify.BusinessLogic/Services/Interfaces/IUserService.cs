using Identify.BusinessLogic.DTOs.RequestDTOs;
using Identify.BusinessLogic.DTOs.ResponseDTOs;
using Shared;

namespace Identify.BusinessLogic.Services.Interfaces;

public interface IUserServiceAsync
{
    Task<Response<UserDto>> CreateUserAsync(RegisterUserDto dto, CancellationToken cancellationToken);
    Task<Response<UserDto>> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken);
    Task<Response> DeleteUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<Response> AddUserToRoleAsync(int userId, int roleId, CancellationToken cancellationToken);
    Task<Response<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<Response<UserDto>> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<Response<UserDto>> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);
}