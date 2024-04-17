using AutoMapper;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Commands.Update;

public class UpdateSubjectCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateSubjectCommand, SubjectResponseDto>
{
    public async Task<Response<SubjectResponseDto>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ISubjectRepository>();
        var dto = request.Dto;

        var existingSubject = await repository.GetAsync(s => 
            s.Id != dto.Id && s.IsName(dto.Name), cancellationToken);

        if (existingSubject.Count != 0)
        {
            return Response.Failure<SubjectResponseDto>(DomainErrors.Subject.NameConflict);
        }
        
        var subject = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (subject is null)
        {
            return Response.Failure<SubjectResponseDto>(DomainErrors.Subject.SubjectNotFoundById);
        }
        
        _mapper.Map(dto, subject);
        
        repository.Update(subject);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<SubjectResponseDto>(subject);
    }
}