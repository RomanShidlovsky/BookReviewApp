using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Commands.RemoveSubjectFromBook;

public class RemoveSubjectFromBookCommandHandler(IUnitOfWork _unitOfWork)
    : ICommandHandler<RemoveSubjectFromBookCommand>
{
    public async Task<Response> Handle(RemoveSubjectFromBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ISubjectRepository>();
        var dto = request.Dto;

        var subjectExists = await repository.ExistsAsync(dto.SubjectId, cancellationToken);
        
        if (!subjectExists)
            return Response.Failure(DomainErrors.Subject.SubjectNotFoundById);

        var book = await _unitOfWork.GetRepository<IBookRepository>().GetByIdAsync(dto.BookId, cancellationToken);
        
        if (book == null)
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        
        if (!book.ContainsSubject(dto.SubjectId))
            return Response.Failure(DomainErrors.Book.NotContainSubject);

        var result = await repository.RemoveSubjectFromBookAsync(dto.SubjectId, dto.BookId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return result
            ? Response.Success()
            : Response.Failure(DomainErrors.Subject.SubjectNotRemovedFromBook);
    }
}