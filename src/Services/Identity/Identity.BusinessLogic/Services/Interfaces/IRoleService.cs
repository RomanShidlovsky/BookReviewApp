using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Shared;

namespace Identity.BusinessLogic.Services.Interfaces;

public interface IRoleService
{
    Task<Response<RoleDto>> CreateRoleAsync(CreateRoleDto dto, CancellationToken cancellationToken);
    Task<Response> DeleteRoleByIdAsync(int id, CancellationToken cancellationToken);
    Task<Response<RoleDto>> GetRoleByIdAsync(int id, CancellationToken cancellationToken);
    Task<Response<RoleDto>> GetRoleByNameAsync(string name, CancellationToken cancellationToken);
    Task<Response<IEnumerable<RoleDto>>> GetAllRolesAsync(CancellationToken cancellationToken);
}