using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.UserFeatures.Commands.Delete;

public class DeleteUserCommandHandler(IUnitOfWork _unitOfWork)
    : IDeleteCommandHandler<DeleteUserCommand>
{
    public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.UserRepository;
        var dto = request.Dto;
        
        var user = await repository.GetByIdAsync(dto.Id.ToString(), cancellationToken);

        if (user is null)
        {
            return Response.Failure(DomainErrors.User.UserNotFoundById);
        }
        
        await repository.DeleteAsync(user, cancellationToken);
        
        return Response.Success();
    }
}