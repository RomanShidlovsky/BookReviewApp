using AutoMapper;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Domain.Entities;

namespace Review.Application.Mappers;

public class ReviewMapper : Profile
{
    public ReviewMapper()
    {
        CreateMap<CreateReviewDto, ReviewEntity>()
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));
        
        CreateMap<UpdateReviewDto, ReviewEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text));
        
        CreateMap<ReviewEntity, ReviewResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Rating))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Text))
            .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes))
            .ForMember(dest => dest.Dislikes, opt => opt.MapFrom(src => src.Dislikes))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));
    }
}