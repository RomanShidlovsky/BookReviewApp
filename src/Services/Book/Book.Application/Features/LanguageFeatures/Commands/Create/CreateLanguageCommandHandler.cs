using AutoMapper;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Entities;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.LanguageFeatures.Commands.Create;

public class CreateLanguageCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICreateCommandHandler<CreateLanguageCommand, LanguageResponseDto>
{
    public async Task<Response<LanguageResponseDto>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ILanguageRepository>();
        var dto = request.Dto;

        var existingLanguage = await repository.GetByNameAsync(dto.Name, cancellationToken);
        
        if (existingLanguage is not null)
            return Response.Failure<LanguageResponseDto>(DomainErrors.Language.NameConflict);

        var language = _mapper.Map<Language>(dto);
        
        repository.Create(language);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<LanguageResponseDto>(language);
    }
}