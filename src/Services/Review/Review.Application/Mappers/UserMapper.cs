using AutoMapper;
using RabbitMQ.EventBus.Interfaces.UserMessages;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Domain.Entities;

namespace Review.Application.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<IUserCreated, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        
        CreateMap<IUserUpdated, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => int.Parse(src.Id)))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
    }
}