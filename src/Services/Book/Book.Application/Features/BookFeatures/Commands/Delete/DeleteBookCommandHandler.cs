using AutoMapper;
using Book.Application.DTOs.EventBus;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using MassTransit;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Response = Shared.Wrappers.Response;

namespace Book.Application.Features.BookFeatures.Commands.Delete;

public class DeleteBookCommandHandler(IUnitOfWork _unitOfWork, IPublishEndpoint _publishEndpoint)
    : IDeleteCommandHandler<DeleteBookCommand>
{
    public async Task<Response> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.GetRepository<IBookRepository>();

        var book = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (book is null)
        {
            return Response.Failure(DomainErrors.Book.BookNotFoundById);
        }
        
        repository.Delete(book);
        await _unitOfWork.SaveAsync(cancellationToken);
        
        await _publishEndpoint.Publish<IBookDeleted>(new BookDeleted(book.Id), cancellationToken);
        
        await Console.Out.WriteLineAsync($"BookDeleted with Id = {book.Id} published.");
        
        return Response.Success();
    }
}