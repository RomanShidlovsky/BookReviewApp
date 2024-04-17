using AutoMapper;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;
using Review.Domain.Entities;
using Review.Domain.Errors;
using Review.Domain.Interfaces.Repositories;
using Review.Infrastructure.Repositories;
using Shared.Wrappers;

namespace Review.Application.Features.Review.Commands.Create;

public class CreateReviewCommandHandler(IUnitOfWork _unitOfWork, IMapper _mapper)
    : ICreateCommandHandler<CreateReviewCommand, ReviewResponseDto>
{
    public async Task<Response<ReviewResponseDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IReviewRepository>();
        var dto = request.Dto;

        var userExists = await _unitOfWork.GetRepository<IUserRepository>()
            .ExistsAsync(dto.UserId, cancellationToken);

        /*if (!userExists)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.User.UserNotFoundById);
        }

        var bookExists = await _unitOfWork.GetRepository<IBookRepository>()
            .ExistsAsync(dto.BookId, cancellationToken);

        if (!bookExists)
        {
            return Response.Failure<ReviewResponseDto>(DomainErrors.Book.BookNotFoundById);
        }*/
        
        var review = _mapper.Map<ReviewEntity>(dto);
        
        repository.Create(review);
        await _unitOfWork.SaveAsync(cancellationToken);

        return _mapper.Map<ReviewResponseDto>(review);
    }
}