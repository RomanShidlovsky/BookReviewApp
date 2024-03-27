using AutoMapper;
using FluentValidation;
using Identify.Application.DTOs.RequestDTOs;
using Identify.Application.DTOs.ResponseDTOs;
using Identify.Application.Services.Interfaces;
using Identify.Domain.Entities;
using Identify.Domain.Enums;
using Identify.Domain.Errors;
using Identify.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identify.Application.Services.Implementations;

public class UserService(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IValidator<RegisterUserDto> registerUserValidator,
    IValidator<UpdateUserDto> updateUserValidator,
    IMapper mapper) : IUserServiceAsync
{
    public async Task<Response<UserDto>> CreateUserAsync(RegisterUserDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await registerUserValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailedResponse<UserDto>.WithErrors(
                validationResult.Errors.Select(f => new Error(f.PropertyName, f.ErrorMessage)));
        }

        var existingUser = await userManager.FindByNameAsync(dto.UserName);
        if (existingUser != null)
        {
            return Response.Failure<UserDto>(DomainErrors.User.UsernameConflict);
        }

        var user = mapper.Map<User>(dto);
        user.SecurityStamp = Guid.NewGuid().ToString();

        var result = await userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return Response.Failure<UserDto>(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description,
                400));
        }

        await userManager.AddToRoleAsync(user, Roles.Client.ToString());

        return mapper.Map<UserDto>(user);
    }

    public async Task<Response<UserDto>> UpdateUserAsync(UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(dto.Id.ToString());
        if (user == null)
        {
            return Response.Failure<UserDto>(DomainErrors.User.UserNotFoundById);
        }

        var validationResult = await updateUserValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailedResponse<UserDto>.WithErrors(
                validationResult.Errors.Select(f => new Error(f.PropertyName, f.ErrorMessage)));
        }
        
        user.UserName = dto.UserName;
        user.Email = dto.Email;

        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            return Response.Failure<UserDto>(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description,
                400));
        }

        return mapper.Map<UserDto>(user);
    }

    public Task<Response> DeleteUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Response> AddUserToRoleAsync(int userId, int roleId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<IEnumerable<UserDto>>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        var usersList = await userManager.Users.ToListAsync(cancellationToken);
        var userResponses = mapper.Map<List<UserDto>>(usersList);

        for (int i = 0; i < usersList.Count; i++)
        {
            userResponses[i].Roles = await userManager.GetRolesAsync(usersList[i]);
        }

        return userResponses;
    }

    public async Task<Response<UserDto>> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            return Response.Failure<UserDto>(DomainErrors.User.UserNotFoundById);
        }

        var userRoles = await userManager.GetRolesAsync(user);

        var response = mapper.Map<UserDto>(user);
        response.Roles = userRoles;

        return response;
    }

    public async Task<Response<UserDto>> GetUserByUserNameAsync(string userName, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(userName);
        if (user == null)
        {
            return Response.Failure<UserDto>(DomainErrors.User.UserNotFoundByUsername);
        }

        var userRoles = await userManager.GetRolesAsync(user);

        var response = mapper.Map<UserDto>(user);
        response.Roles = userRoles;

        return response;
    }
}