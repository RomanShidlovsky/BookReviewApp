using System.Net;
using Identity.BusinessLogic.DTOs.RequestDTOs.User;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Services.Interfaces;
using Identity.DataAccess.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Wrappers;

namespace Identity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserService _userService, ILogger<UsersController> _logger) 
    : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await _userService.GetAllUsersAsync(cancellationToken);

        return ApiResponse.GetObjectResult(users, _logger);
    }
    
    [HttpGet("{id:int}")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var user = await _userService.GetUserByIdAsync(id);

        return ApiResponse.GetObjectResult(user, _logger);
    }
    
    [HttpGet("{userName}")]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetUserByName([FromRoute] string userName)
    {
        var user = await _userService.GetUserByUserNameAsync(userName);

        return ApiResponse.GetObjectResult(user, _logger);
    }
    
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateUserAsync(dto, cancellationToken);

        return ApiResponse.GetObjectResult(user, _logger);
    }
    
    [HttpPut]
    [Authorize(Roles = $"{Roles.Client}, {Roles.Admin}, {Roles.Reviewer}")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(IEnumerable<Error>), (int)HttpStatusCode.UnprocessableEntity)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userService.UpdateUserAsync(dto, cancellationToken);

        return ApiResponse.GetObjectResult(user, _logger);
    }
    
    [HttpDelete("{id:int}")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(bool) ,(int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var result = await _userService.DeleteUserByIdAsync(id);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpPut("{id:int}/roles")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRoleDto dto)
    {
        var result = await _userService.AddUserToRoleAsync(dto);

        return ApiResponse.GetObjectResult(result, _logger);
    }
    
    [HttpDelete("{id:int}/roles")]
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(Error), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RemoveUserFromRole([FromBody] RemoveUserFromRoleDto dto)
    {
        var result = await _userService.RemoveUserFromRoleAsync(dto);

        return ApiResponse.GetObjectResult(result, _logger);
    }
}