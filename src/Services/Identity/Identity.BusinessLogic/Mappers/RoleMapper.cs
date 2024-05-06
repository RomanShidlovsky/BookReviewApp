using Identity.BusinessLogic.DTOs.RequestDTOs.Role;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.DataAccess.Entities;

namespace Identity.BusinessLogic.Mappers;

public class RoleMapper : AutoMapper.Profile
{
    public RoleMapper()
    {
        CreateMap<CreateRoleDto, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ReverseMap();
    }
}