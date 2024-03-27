using AutoMapper;
using FluentValidation;
using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Services.Interfaces;
using Identity.DataAccess.Entities;
using Identity.DataAccess.Errors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Identity.BusinessLogic.Services.Implementations;

public class RoleService(
    RoleManager<Role> roleManager,
    IValidator<CreateRoleDto> validator,
    IMapper mapper)
    : IRoleService
{
    public async Task<Response<RoleDto>> CreateRoleAsync(CreateRoleDto dto, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            return ValidationFailedResponse<RoleDto>.WithErrors(
                validationResult.Errors.Select(f => new Error(f.PropertyName, f.ErrorMessage)));
        }

        var existingRole = await roleManager.FindByNameAsync(dto.Name);
        if (existingRole is not null)
        {
            return Response.Failure<RoleDto>(DomainErrors.Role.NameConflict);
        }

        var role = mapper.Map<Role>(dto);
        role.ConcurrencyStamp = Guid.NewGuid().ToString();

        var result = await roleManager.CreateAsync(role);

        return result.Succeeded
            ? mapper.Map<RoleDto>(role)
            : Response.Failure<RoleDto>(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
    }

    public async Task<Response> DeleteRoleByIdAsync(int id, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        if (role is null)
        {
            return Response.Failure(DomainErrors.Role.RoleNotFoundById);
        }
        
        var result = await roleManager.DeleteAsync(role);
        
        return result.Succeeded
            ? Response.Success()
            : Response.Failure(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
    }

    public async Task<Response<RoleDto>> GetRoleByIdAsync(int id, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByIdAsync(id.ToString());
        
        return role is null 
            ? Response.Failure<RoleDto>(DomainErrors.Role.RoleNotFoundById) 
            : mapper.Map<RoleDto>(role);
    }

    public async Task<Response<RoleDto>> GetRoleByNameAsync(string name, CancellationToken cancellationToken)
    {
        var role = await roleManager.FindByNameAsync(name);

        return role is null
            ? Response.Failure<RoleDto>(DomainErrors.Role.RoleNotFoundByName)
            : mapper.Map<RoleDto>(role);
    }

    public async Task<Response<IEnumerable<RoleDto>>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await roleManager.Roles.ToListAsync(cancellationToken);

        return mapper.Map<List<RoleDto>>(roles);
    }
}