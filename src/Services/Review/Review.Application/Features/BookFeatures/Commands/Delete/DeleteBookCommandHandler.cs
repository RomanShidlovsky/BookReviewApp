using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.BookFeatures.Commands.Delete;

public class DeleteBookCommandHandler(IUnitOfWork _unitOfWork)
    : IDeleteCommandHandler<DeleteBookCommand>
{
    public async Task<Response> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.BookRepository;

        var book = await repository.GetByIdAsync(request.Id.ToString(), cancellationToken);

        if (book is null)
        {
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        }
        
        await repository.DeleteAsync(book, cancellationToken);
        
        return Response.Success();
    }
}