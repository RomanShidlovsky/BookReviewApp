using AutoMapper;
using Book.Application.DTOs.Language.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Entities;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.LanguageFeatures.Commands.Update;

public class UpdateLanguageCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateLanguageCommand, LanguageResponseDto>
{
    public async Task<Response<LanguageResponseDto>> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ILanguageRepository>();
        var dto = request.Dto;

        var existingLanguage = await repository.GetAsync(l => 
            l.Id != dto.Id && l.IsName(dto.Name), cancellationToken);
        
        if (existingLanguage.Count != 0)
            return Response.Failure<LanguageResponseDto>(DomainErrors.Language.NameConflict);

        var language = await repository.GetByIdAsync(dto.Id, cancellationToken);
        
        if (language is null)
            return Response.Failure<LanguageResponseDto>(DomainErrors.Language.LanguageNotFoundById);

        _mapper.Map(dto, language);
        
        repository.Update(language);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<LanguageResponseDto>(language);
    }
}