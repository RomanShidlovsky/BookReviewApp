using AutoMapper;
using Book.Application.DTOs.Book.RequestDTOs;
using Book.Application.DTOs.Book.ResponseDTOs;
using BookEntity = Book.Domain.Entities.Book;

namespace Book.Application.MapperProfiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<CreateBookDto, BookEntity>()
            .ForMember(dest => dest.OpenLibraryKey, opt => opt.MapFrom(src => src.OpenLibraryKey))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.EditionCount, opt => opt.MapFrom(src => src.EditionCount))
            .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        
        CreateMap<UpdateBookDto, BookEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OpenLibraryKey, opt => opt.MapFrom(src => src.OpenLibraryKey))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.EditionCount, opt => opt.MapFrom(src => src.EditionCount))
            .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<BookEntity, BookResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OpenLibraryKey, opt => opt.MapFrom(src => src.OpenLibraryKey))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.EditionCount, opt => opt.MapFrom(src => src.EditionCount))
            .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
            .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subjects))
            .ForMember(dest => dest.AverageCriticRating, opt => opt.MapFrom(src => src.AverageCriticRating));
        
        CreateMap<BookEntity, BookWithReviewsResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OpenLibraryKey, opt => opt.MapFrom(src => src.OpenLibraryKey))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.EditionCount, opt => opt.MapFrom(src => src.EditionCount))
            .ForMember(dest => dest.PublicationYear, opt => opt.MapFrom(src => src.PublicationYear))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
            .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Languages))
            .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.Subjects))
            .ForMember(dest => dest.Reviews, opt => opt.Ignore())
            .ForMember(dest => dest.CriticReviews, opt => opt.Ignore())
            .ForMember(dest => dest.AverageCriticRating, opt => opt.MapFrom(src => src.AverageCriticRating));;
    }
}