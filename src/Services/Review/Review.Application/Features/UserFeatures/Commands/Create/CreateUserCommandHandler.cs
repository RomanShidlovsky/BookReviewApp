using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Entities;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.UserFeatures.Commands.Create;

public class CreateUserCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICreateCommandHandler<CreateUserCommand, UserResponseDto>
{
    public async Task<Response<UserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IUserRepository>();
        var dto = request.Dto;

        var userExists = await repository.ExistsAsync(dto.Id, cancellationToken);

        if (userExists)
        {
            return Response.Failure<UserResponseDto>(DomainErrors.User.AlreadyExists);
        }

        var user = _mapper.Map<User>(dto);
        
        repository.Create(user);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<UserResponseDto>(user);
    }
}