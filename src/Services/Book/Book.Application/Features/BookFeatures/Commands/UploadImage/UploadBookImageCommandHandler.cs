using Book.Application.DTOs.EventBus;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Net.Http.Headers;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Response = Shared.Wrappers.Response;

namespace Book.Application.Features.BookFeatures.Commands.UploadImage;

public class UploadBookImageCommandHandler(IUnitOfWork _unitOfWork, IPublishEndpoint _publishEndpoint) 
    : ICommandHandler<UploadBookImageCommand, string>
{
    public async Task<Shared.Wrappers.Response<string>> Handle(UploadBookImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.File.Length <= 0)
            {
                return Response.Failure<string>(DomainErrors.Author.AuthorImageNotUploaded);
            }

            var repository = _unitOfWork.GetRepository<IBookRepository>();

            var book = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (book is null)
            {
                return Response.Failure<string>(DomainErrors.Book.BookImageNotUploaded);
            }
            
            var dbPath = await SaveImage(request, cancellationToken);
            var resourcePath = Path.Combine("https://localhost:5000", dbPath);
            
            book.ImageUrl = resourcePath;
            repository.Update(book);

            await _unitOfWork.SaveAsync(cancellationToken);
            
            await _publishEndpoint.Publish<IBookUpdated>(new BookUpdated(book.Id, book.Title, book.ImageUrl),
                cancellationToken);
        
            await Console.Out.WriteLineAsync($"BookUpdated with Id = {book.Id} published.");

            return resourcePath;
        }
        catch
        {
            return Response.Failure<string>(DomainErrors.Book.BookImageNotUploaded);
        }
    }
    
    private async Task<string> SaveImage(UploadBookImageCommand request, CancellationToken cancellationToken)
    {
        var folderName = Path.Combine("Resources", "Images", "Books");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        if (!Directory.Exists(pathToSave))
        {
            Directory.CreateDirectory(pathToSave);
        }
        
        var fileName = ContentDispositionHeaderValue
            .Parse(request.File.ContentDisposition).FileName.Trim().Value!.Replace(' ', '_');

        var fullPath = Path.Combine(pathToSave, fileName);
        var dbPath = Path.Combine(folderName, fileName);
            
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await request.File.CopyToAsync(stream, cancellationToken);

        return dbPath;
    }
}