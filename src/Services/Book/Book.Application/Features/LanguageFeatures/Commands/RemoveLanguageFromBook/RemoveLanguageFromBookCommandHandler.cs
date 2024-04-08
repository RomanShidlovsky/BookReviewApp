using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Extensions;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.LanguageFeatures.Commands.RemoveLanguageFromBook;

public class RemoveLanguageFromBookCommandHandler(IUnitOfWork _unitOfWork)
    : ICommandHandler<RemoveLanguageFromBookCommand>
{
    public async Task<Response> Handle(RemoveLanguageFromBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ILanguageRepository>();
        var dto = request.Dto;

        var languageExists = await repository.ExistsAsync(dto.LanguageId, cancellationToken);
        
        if (!languageExists)
            return Response.Failure(DomainErrors.Language.LanguageNotFoundById);

        var book = await _unitOfWork.GetRepository<IBookRepository>().GetByIdAsync(dto.BookId, cancellationToken);
        
        if (book == null)
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        
        if (!book.ContainsLanguage(dto.LanguageId))
            return Response.Failure(DomainErrors.Book.NotContainLanguage);

        var result = await repository.RemoveLanguageFromBookAsync(dto.LanguageId, dto.BookId, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        return result
            ? Response.Success()
            : Response.Failure(DomainErrors.Language.LanguageNotRemovedFromBook);
    }
}