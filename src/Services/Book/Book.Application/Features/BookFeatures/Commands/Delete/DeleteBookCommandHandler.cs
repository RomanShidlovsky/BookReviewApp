using AutoMapper;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.BookFeatures.Commands.Delete;

public class DeleteBookCommandHandler(IUnitOfWork _unitOfWork)
    : IDeleteCommandHandler<DeleteBookCommand>
{
    public async Task<Response> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();

        var book = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (book == null)
            return Response.Failure(DomainErrors.Book.BookNotFoundById);

        repository.Delete(book);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Response.Success();
    }
}