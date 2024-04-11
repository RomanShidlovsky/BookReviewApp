using AutoMapper;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Book.Application.Features.LanguageFeatures.Commands.Delete;

public class DeleteLanguageCommandHandler(IUnitOfWork _unitOfWork)
    : IDeleteCommandHandler<DeleteLanguageCommand>
{
    public async Task<Response> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<ILanguageRepository>();

        var language = await repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (language is null)
            return Response.Failure(DomainErrors.Language.LanguageNotFoundById);
        
        repository.Delete(language);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        return Response.Success();
    }
}