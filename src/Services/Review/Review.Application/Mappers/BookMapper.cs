using AutoMapper;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Domain.Entities;

namespace Review.Application.Mappers;

public class BookMapper : Profile
{
    public BookMapper()
    {
        CreateMap<CreateBookDto, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        
        CreateMap<UpdateBookDto, Book>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        
        CreateMap<Book, BookResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
    }
}