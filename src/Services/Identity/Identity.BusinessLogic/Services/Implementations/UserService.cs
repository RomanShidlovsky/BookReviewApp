using AutoMapper;
using FluentValidation;
using Identity.BusinessLogic.DTOs.RequestDTOs.User;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Errors;
using Identity.BusinessLogic.Services.Interfaces;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Wrappers;

namespace Identity.BusinessLogic.Services.Implementations;

public class UserService(
    UserManager<User> _userManager,
    RoleManager<Role> _roleManager,
    IValidator<RegisterUserDto> _registerUserValidator,
    IValidator<UpdateUserDto> _updateUserValidator,
    IMapper _mapper) : IUserService
{
    public async Task<Response<UserDto>> CreateUserAsync(RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await _registerUserValidator.ValidateAsync(dto, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return ValidationFailedResponse<UserDto>.WithErrors(
                validationResult.Errors.Select(f => new Error(f.PropertyName, f.ErrorMessage)));
        }

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

        await _userManager.AddToRoleAsync(user, Roles.Client.ToString());

        return _mapper.Map<UserDto>(user);
    }

    public async Task<Response<UserDto>> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(dto.Id.ToString());
        
        if (user is not { DateDeleted: null })
        {
            return Response.Failure<UserDto>(DomainErrors.User.UserNotFoundById);
        }

        var validationResult = await _updateUserValidator.ValidateAsync(dto, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return ValidationFailedResponse<UserDto>.WithErrors(
                validationResult.Errors.Select(f => new Error(f.PropertyName, f.ErrorMessage)));
        }

        user.UserName = dto.UserName;
        user.Email = dto.Email;

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded
            ? _mapper.Map<UserDto>(user)
            : Response.Failure<UserDto>(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
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

        return result.Succeeded
            ? Response.Success()
            : Response.Failure(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
    }

    public async Task<Response> AddUserToRoleAsync(int userId, int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (role is null)
        {
            return Response.Failure(DomainErrors.Role.RoleNotFoundById);
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        
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

    public async Task<Response> RemoveUserFromRoleAsync(int userId, int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        
        if (role is null)
        {
            return Response.Failure(DomainErrors.Role.RoleNotFoundById);
        }

        var user = await _userManager.FindByIdAsync(userId.ToString());
        
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

    public async Task<Response<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken)
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

    public async Task<Response<UserDto>> GetUserByIdAsync(int id)
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

    public async Task<Response<UserDto>> GetUserByUserNameAsync(string userName)
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