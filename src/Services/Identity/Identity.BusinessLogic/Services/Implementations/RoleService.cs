using AutoMapper;
using FluentValidation;
using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.BusinessLogic.Errors;
using Identity.BusinessLogic.Services.Interfaces;
using Identity.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Wrappers;

namespace Identity.BusinessLogic.Services.Implementations;

public class RoleService(
    RoleManager<Role> _roleManager,
    IValidator<CreateRoleDto> _validator,
    IMapper _mapper)
    : IRoleService
{
    public async Task<Response<RoleDto>> CreateRoleAsync(CreateRoleDto dto, CancellationToken cancellationToken)
    {
        var existingRole = await _roleManager.FindByNameAsync(dto.Name);
        
        if (existingRole is not null)
        {
            return Response.Failure<RoleDto>(DomainErrors.Role.NameConflict);
        }

        var role = _mapper.Map<Role>(dto);
        role.ConcurrencyStamp = Guid.NewGuid().ToString();

        var result = await _roleManager.CreateAsync(role);

        return result.Succeeded
            ? _mapper.Map<RoleDto>(role)
            : Response.Failure<RoleDto>(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
    }

    public async Task<Response> DeleteRoleByIdAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        
        if (role is null)
        {
            return Response.Failure(DomainErrors.Role.RoleNotFoundById);
        }
        
        var result = await _roleManager.DeleteAsync(role);
        
        return result.Succeeded
            ? Response.Success()
            : Response.Failure(new Error(
                result.Errors.First().Code,
                result.Errors.First().Description));
    }

    public async Task<Response<RoleDto>> GetRoleByIdAsync(int id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());
        
        return role is null 
            ? Response.Failure<RoleDto>(DomainErrors.Role.RoleNotFoundById) 
            : _mapper.Map<RoleDto>(role);
    }

    public async Task<Response<RoleDto>> GetRoleByNameAsync(string name)
    {
        var role = await _roleManager.FindByNameAsync(name);

        return role is null
            ? Response.Failure<RoleDto>(DomainErrors.Role.RoleNotFoundByName)
            : _mapper.Map<RoleDto>(role);
    }

    public async Task<Response<IEnumerable<RoleDto>>> GetAllRolesAsync(CancellationToken cancellationToken)
    {
        var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

        return _mapper.Map<List<RoleDto>>(roles);
    }
}