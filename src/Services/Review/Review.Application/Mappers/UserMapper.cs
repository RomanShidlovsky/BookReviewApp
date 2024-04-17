using AutoMapper;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Domain.Entities;

namespace Review.Application.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
    }
}