using AutoMapper;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Commands.Delete;

public class DeleteAuthorCommandHandler(IUnitOfWork _unitOfWork)
    : IDeleteCommandHandler<DeleteAuthorCommand>
{
    public async Task<Response> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IAuthorRepository>();

        var author = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (author == null)
            return Response.Failure(DomainErrors.Author.AuthorNotFoundById);

        repository.Delete(author);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Response.Success();
    }
}