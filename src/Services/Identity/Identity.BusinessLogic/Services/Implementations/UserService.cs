using AutoMapper;
using Identity.BusinessLogic.DTOs.ProducerDTOs;
using Identity.BusinessLogic.DTOs.RequestDTOs.User;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Errors;
using Identity.BusinessLogic.Services.Interfaces;
using Identity.DataAccess.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.EventBus.Interfaces.UserMessages;
using Shared;
using Shared.Constants;
using Response = Shared.Wrappers.Response;

namespace Identity.BusinessLogic.Services.Implementations;

public class UserService(
    UserManager<User> _userManager,
    RoleManager<Role> _roleManager,
    IMapper _mapper,
    IPublishEndpoint _publishEndpoint) : IUserService
{
    public async Task<Shared.Wrappers.Response<UserDto>> CreateUserAsync(RegisterUserDto dto,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByNameAsync(dto.UserName);

        if (existingUser is not null)
        {
            return Response.Failure<UserDto>(DomainErrors.User.UsernameConflict);
        }

        var user = _mapper.Map<User>(dto);
        user.SecurityStamp = Guid.NewGuid().ToString();

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
        {
            return Response.Failure<UserDto>(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
        }

        await _userManager.AddToRoleAsync(user, Roles.Client);

        await _publishEndpoint.Publish<IUserCreated>(new UserCreatedDto(user.Id, user.UserName, user.ImageUrl),
            cancellationToken);

        await Console.Out.WriteLineAsync($"UserCreated with Id = {user.Id} published.");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<Shared.Wrappers.Response<UserDto>> UpdateUserAsync(UpdateUserDto dto,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(dto.Id.ToString());

        if (user is not { DateDeleted: null })
        {
            return Response.Failure<UserDto>(DomainErrors.User.UserNotFoundById);
        }

        user.UserName = dto.UserName;
        user.Email = dto.Email;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Response.Failure<UserDto>(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
        }

        await _publishEndpoint.Publish<IUserUpdated>(new UserUpdatedDto(user.Id, user.UserName, user.ImageUrl),
            cancellationToken);

        await Console.Out.WriteLineAsync($"UserCreated with Id = {user.Id} published.");

        return _mapper.Map<UserDto>(user);
    }

    public async Task<Response> DeleteUserByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is not { DateDeleted: null })
        {
            return Response.Failure(DomainErrors.User.UserNotFoundById);
        }

        user.DateDeleted = DateTimeOffset.UtcNow;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return Response.Failure(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
        }

        await _publishEndpoint.Publish<IUserDeleted>(new UserDeletedDto(user.Id));

        await Console.Out.WriteLineAsync($"UserCreated with Id = {user.Id} published.");

        return Response.Success();
    }

    public async Task<Response> AddUserToRoleAsync(AddUserToRoleDto dto)
    {
        var role = await _roleManager.FindByIdAsync(dto.RoleId.ToString());

        if (role is null)
        {
            return Response.Failure(DomainErrors.Role.RoleNotFoundById);
        }

        var user = await _userManager.FindByIdAsync(dto.UserId.ToString());

        if (user is not { DateDeleted: null })
        {
            return Response.Failure(DomainErrors.User.UserNotFoundById);
        }

        if (await _userManager.IsInRoleAsync(user, role.Name))
        {
            return Response.Failure(DomainErrors.User.AlreadyInRole);
        }

        var result = await _userManager.AddToRoleAsync(user, role.Name);

        return result.Succeeded
            ? Response.Success()
            : Response.Failure(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
    }

    public async Task<Response> RemoveUserFromRoleAsync(RemoveUserFromRoleDto dto)
    {
        var role = await _roleManager.FindByIdAsync(dto.RoleId.ToString());

        if (role is null)
        {
            return Response.Failure(DomainErrors.Role.RoleNotFoundById);
        }

        var user = await _userManager.FindByIdAsync(dto.UserId.ToString());

        if (user is not { DateDeleted: null })
        {
            return Response.Failure(DomainErrors.User.UserNotFoundById);
        }

        if (await _userManager.IsInRoleAsync(user, role.Name))
        {
            return Response.Failure(DomainErrors.User.UserNotInRole);
        }

        var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

        return result.Succeeded
            ? Response.Success()
            : Response.Failure(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
    }

    public async Task<Shared.Wrappers.Response<IEnumerable<UserDto>>> GetAllUsersAsync(
        CancellationToken cancellationToken)
    {
        var usersList = await _userManager.Users
            .Where(u => u.DateDeleted == null)
            .ToListAsync(cancellationToken);

        var userResponses = _mapper.Map<List<UserDto>>(usersList);

        for (var i = 0; i < usersList.Count; i++)
        {
            userResponses[i].Roles = await _userManager.GetRolesAsync(usersList[i]);
        }

        return userResponses;
    }

    public async Task<Shared.Wrappers.Response<UserDto>> GetUserByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user is not { DateDeleted: null })
        {
            return Response.Failure<UserDto>(DomainErrors.User.UserNotFoundById);
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var response = _mapper.Map<UserDto>(user);
        response.Roles = userRoles;

        return response;
    }

    public async Task<Shared.Wrappers.Response<UserDto>> GetUserByUserNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is not { DateDeleted: null })
        {
            return Response.Failure<UserDto>(DomainErrors.User.UserNotFoundByUsername);
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var response = _mapper.Map<UserDto>(user);
        response.Roles = userRoles;

        return response;
    }
}