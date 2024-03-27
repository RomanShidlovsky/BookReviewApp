using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.DataAccess.Entities;

namespace Identity.BusinessLogic.Mappers;

public class RoleMapper : AutoMapper.Profile
{
    public RoleMapper()
    {
        CreateMap<CreateRoleDto, Role>();
        CreateMap<Role, RoleDto>()
            .ReverseMap();
    }
}