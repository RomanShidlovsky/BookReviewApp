using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Commands.Delete;

public class DeleteSubjectCommandHandler(IUnitOfWork _unitOfWork)
    : IDeleteCommandHandler<DeleteSubjectCommand>
{
    public async Task<Response> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ISubjectRepository>();

        var subject = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (subject is null)
        {
            return Response.Failure(DomainErrors.Subject.SubjectNotFoundById);
        }
        
        repository.Delete(subject);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Response.Success();
    }
}