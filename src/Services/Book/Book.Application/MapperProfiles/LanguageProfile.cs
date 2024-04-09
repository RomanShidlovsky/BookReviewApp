using AutoMapper;
using Book.Application.DTOs.Language.RequestDTOs;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Domain.Entities;

namespace Book.Application.MapperProfiles;

public class LanguageProfile : Profile
{
    protected LanguageProfile()
    {
        CreateMap<CreateLanguageDto, Language>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<UpdateLanguageDto, Language>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<Language, LanguageResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}