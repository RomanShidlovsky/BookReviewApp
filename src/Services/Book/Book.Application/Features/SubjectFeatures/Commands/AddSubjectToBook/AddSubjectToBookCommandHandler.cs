using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.SubjectFeatures.Commands.AddSubjectToBook;

public class AddSubjectToBookCommandHandler(IUnitOfWork _unitOfWork)
    : ICommandHandler<AddSubjectToBookCommand>
{
    public async Task<Response> Handle(AddSubjectToBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ISubjectRepository>();
        var dto = request.Dto;

        var subjectExists = await repository.ExistsAsync(dto.SubjectId, cancellationToken);

        if (!subjectExists)
        {
            return Response.Failure(DomainErrors.Subject.SubjectNotFoundById);
        }
        
        var book = await _unitOfWork.GetRepository<IBookRepository>().GetByIdAsync(dto.BookId, cancellationToken);

        if (book is null)
        {
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        }

        if (book.ContainsSubject(dto.SubjectId))
        {
            return Response.Failure(DomainErrors.Book.AlreadyContainsSubject);
        }
        
        var result = await repository.AddSubjectToBookAsync(dto.SubjectId, dto.BookId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return result
            ? Response.Success()
            : Response.Failure(DomainErrors.Subject.SubjectNotAddedToBook);
    }
}