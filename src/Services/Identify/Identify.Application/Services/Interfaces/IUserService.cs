using Identify.Application.DTOs.RequestDTOs;
using Identify.Application.DTOs.ResponseDTOs;
using Identify.Domain.Shared;

namespace Identify.Application.Services.Interfaces;

public interface IUserServiceAsync
{
    Task<Response<UserDto>> CreateUserAsync(RegisterUserDto dto, CancellationToken cancellationToken);
    Task<Response<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken);
    Task<Response<UserDto>> GetUserByIdAsync(int id, CancellationToken cancellationToken);
    Task<Response<UserDto>> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken);
}