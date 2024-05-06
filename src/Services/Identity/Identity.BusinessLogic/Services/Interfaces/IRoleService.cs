using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Shared;
using Shared.Wrappers;

namespace Identity.BusinessLogic.Services.Interfaces;

public interface IRoleService
{
    Task<Response<RoleDto>> CreateRoleAsync(CreateRoleDto dto, CancellationToken cancellationToken);
    Task<Response> DeleteRoleByIdAsync(int id);
    Task<Response<RoleDto>> GetRoleByIdAsync(int id);
    Task<Response<RoleDto>> GetRoleByNameAsync(string name);
    Task<Response<IEnumerable<RoleDto>>> GetAllRolesAsync(CancellationToken cancellationToken);
}