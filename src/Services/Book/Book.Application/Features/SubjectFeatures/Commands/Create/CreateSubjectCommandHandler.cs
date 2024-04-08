using AutoMapper;
using Book.Application.DTOs.Subject.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using Book.Domain.Entities;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Commands.Create;

public class CreateSubjectCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICreateCommandHandler<CreateSubjectCommand, SubjectResponseDto>
{
    public async Task<Response<SubjectResponseDto>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ISubjectRepository>();
        var dto = request.Dto;

        var existingSubject = await repository.GetByNameAsync(dto.Name, cancellationToken);
        
        if (existingSubject != null)
            return Response.Failure<SubjectResponseDto>(DomainErrors.Subject.NameConflict);

        var subject = _mapper.Map<Subject>(dto);
        
        repository.Create(subject);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<SubjectResponseDto>(subject);
    }
}