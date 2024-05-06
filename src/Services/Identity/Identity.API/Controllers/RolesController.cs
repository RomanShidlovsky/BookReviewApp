using System.Net;
using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Constants;
using Shared.Wrappers;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController(IRoleService _roleService, ILogger<RolesController> _logger) 
    : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(IEnumerable<RoleDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAllRolesAsync(cancellationToken);

        return ApiResponse.GetObjectResult(roles, _logger);
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetRoleById([FromRoute] int id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);

        return ApiResponse.GetObjectResult(role, _logger);
    }

    [HttpGet("{name}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetRoleByName([FromRoute] string name)
    {
        var role = await _roleService.GetRoleByNameAsync(name);

        return ApiResponse.GetObjectResult(role, _logger);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto, CancellationToken cancellationToken)
    {
        var role = await _roleService.CreateRoleAsync(dto, cancellationToken);

        return ApiResponse.GetObjectResult(role, _logger);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(RoleDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteRole([FromRoute] int id)
    {
        var result = await _roleService.DeleteRoleByIdAsync(id);

        return ApiResponse.GetObjectResult(result, _logger);
    }
}