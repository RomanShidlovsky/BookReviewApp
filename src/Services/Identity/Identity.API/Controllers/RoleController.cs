using System.Net;
using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var roles = await roleService.GetAllRolesAsync(cancellationToken);

        return ApiResponse.GetObjectResult(roles);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var role = await roleService.GetRoleByIdAsync(id, cancellationToken);

        return ApiResponse.GetObjectResult(role);
    }
    
    [HttpGet("{name}")]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        var role = await roleService.GetRoleByNameAsync(name, cancellationToken);

        return ApiResponse.GetObjectResult(role);
    }
    
    [HttpPost]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    public async Task<IActionResult> Create(CreateRoleDto dto, CancellationToken cancellationToken)
    {
        var role = await roleService.CreateRoleAsync(dto, cancellationToken);

        return ApiResponse.GetObjectResult(role);
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await roleService.DeleteRoleByIdAsync(id, cancellationToken);

        return ApiResponse.GetObjectResult(result);
    }
}