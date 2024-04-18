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
        var repository = _unitOfWork.UserRepository;
        var dto = request.Dto;

        var existingUser = await repository.GetByIdAsync(dto.Id, cancellationToken);

        if (existingUser is not null)
        {
            return Response.Failure<UserResponseDto>(DomainErrors.User.AlreadyExists);
        }

        var user = _mapper.Map<User>(dto);
        
        await repository.CreateAsync(user, cancellationToken);

        return _mapper.Map<UserResponseDto>(user);
    }
}