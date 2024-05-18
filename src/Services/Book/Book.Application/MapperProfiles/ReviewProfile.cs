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
            .ForMember(dest => dest.LikeUserIds, opt => opt.MapFrom(src => src.LikeUserIds))
            .ForMember(dest => dest.DislikeUserIds, opt => opt.MapFrom(src => src.DislikeUserIds))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
    }
}