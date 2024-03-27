using Identity.BusinessLogic.DTOs.RequestDTOs.User;
using Identity.BusinessLogic.DTOs.ResponseDTOs;
using Identity.DataAccess.Entities;

namespace Identity.BusinessLogic.Mappers;

public class UserMapper : AutoMapper.Profile
{
    public UserMapper()
    {
        CreateMap<RegisterUserDto, User>();
        CreateMap<User, UserDto>()
            .ReverseMap();
    }
}