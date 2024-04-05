using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Commands.AddAuthorToBook;

public class AddAuthorToBookCommandHandler(IUnitOfWork _unitOfWork) 
    : ICommandHandler<AddAuthorToBookCommand>
{
    public async Task<Response> Handle(AddAuthorToBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IAuthorRepository>();
        var dto = request.Dto;

        var author = await repository.GetByIdAsync(dto.AuthorId, cancellationToken);
        
        if (author == null)
            return Response.Failure(DomainErrors.Author.AuthorNotFoundById);

        var book = await _unitOfWork.GetRepository<IBookRepository>().GetByIdAsync(dto.BookId, cancellationToken);

        if (book == null)
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        
        if (book.ContainsAuthor(author.Id))
            return Response.Failure(DomainErrors.Book.AlreadyContainsAuthor);

        var result = await repository.AddAuthorToBookAsync(dto.AuthorId, dto.BookId, cancellationToken);

        return result
            ? Response.Success()
            : Response.Failure(DomainErrors.Author.AuthorNotAddedToBook);
    }
}