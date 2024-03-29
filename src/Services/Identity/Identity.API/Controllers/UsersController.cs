using System.Net;
using Identity.BusinessLogic.DTOs.RequestDTOs.User;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Wrappers;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet(nameof(GetAll))]
    //[Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var users = await userService.GetAllUsersAsync(cancellationToken);

        return ApiResponse.GetObjectResult(users);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Roles = "Client, Admin, SuperAdmin")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var user = await userService.GetUserByIdAsync(id);

        return ApiResponse.GetObjectResult(user);
    }
    
    [HttpGet("{userName}")]
    [Authorize(Roles = "Client, Admin, SuperAdmin")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetByName([FromRoute] string userName)
    {
        var user = await userService.GetUserByUserNameAsync(userName);

        return ApiResponse.GetObjectResult(user);
    }
    
    [HttpPost(nameof(Register))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var user = await userService.CreateUserAsync(dto, cancellationToken);

        return ApiResponse.GetObjectResult(user);
    }
    
    [HttpPut(nameof(Update))]
    [Authorize(Roles = "Client, Admin, SuperAdmin")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await userService.UpdateUserAsync(dto, cancellationToken);

        return ApiResponse.GetObjectResult(user);
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "SuperAdmin")]
    [ProducesResponseType(typeof(bool) ,(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await userService.DeleteUserByIdAsync(id);

        return ApiResponse.GetObjectResult(result);
    }
    
    [HttpPut(nameof(AddToRole))]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddToRole([FromBody] AddUserToRoleDto dto)
    {
        var result = await userService.AddUserToRoleAsync(dto.UserId, dto.RoleId);

        return ApiResponse.GetObjectResult(result);
    }
    
    [HttpPut(nameof(RemoveFromRole))]
    [Authorize(Roles = "Admin, SuperAdmin")]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RemoveFromRole([FromBody] RemoveUserFromRoleDto dto)
    {
        var result = await userService.RemoveUserFromRoleAsync(dto.UserId, dto.RoleId);

        return ApiResponse.GetObjectResult(result);
    }
}