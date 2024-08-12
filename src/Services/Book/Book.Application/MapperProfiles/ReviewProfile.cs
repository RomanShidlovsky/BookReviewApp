using AutoMapper;
using Book.Application.DTOs.Review.ResponseDTOs;

namespace Book.Application.MapperProfiles;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<GrpcServices.Review, ReviewResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes))
            .ForMember(dest => dest.Dislikes, opt => opt.MapFrom(src => src.Dislikes));
    }
}