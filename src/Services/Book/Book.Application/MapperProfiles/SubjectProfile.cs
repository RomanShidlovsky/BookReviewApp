using AutoMapper;
using Book.Application.DTOs.Subject.RequestDTOs;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Domain.Entities;

namespace Book.Application.MapperProfiles;

public class SubjectProfile : Profile
{
    public SubjectProfile()
    {
        CreateMap<CreateSubjectDto, Subject>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<UpdateSubjectDto, Subject>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<Subject, SubjectResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}