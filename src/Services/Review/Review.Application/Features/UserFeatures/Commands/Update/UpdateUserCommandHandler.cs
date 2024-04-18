using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.UserFeatures.Commands.Update;

public class UpdateUserCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : IUpdateCommandHandler<UpdateUserCommand, UserResponseDto>
{
    public async Task<Response<UserResponseDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.UserRepository;
        var dto = request.Dto;

        var user = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (user is null)
        {
            return Response.Failure<UserResponseDto>(DomainErrors.User.UserNotFoundById);
        }

        _mapper.Map(dto, user);
        
        await repository.UpdateAsync(user, cancellationToken);

        return _mapper.Map<UserResponseDto>(user);
    }
}