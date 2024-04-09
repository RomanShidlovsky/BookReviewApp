using AutoMapper;
using Book.Application.DTOs.Author.RequestDTOs;
using Book.Application.DTOs.Author.ResponseDTOs;
using Book.Domain.Entities;

namespace Book.Application.MapperProfiles;

public class AuthorProfile : Profile
{
    protected AuthorProfile()
    {
        CreateMap<CreateAuthorDto, Author>()
            .ForMember(dest => dest.OpenLibraryKey, opt => opt.MapFrom(src => src.OpenLibraryKey))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate))
            .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        CreateMap<Author, AuthorResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OpenLibraryKey, opt => opt.MapFrom(src => src.OpenLibraryKey))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate))
            .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        CreateMap<UpdateAuthorDto, Author>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OpenLibraryKey, opt => opt.MapFrom(src => src.OpenLibraryKey))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.DeathDate, opt => opt.MapFrom(src => src.DeathDate))
            .ForMember(dest => dest.Biography, opt => opt.MapFrom(src => src.Biography))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
    }
}