using System.Net.Mime;
using Book.Application.Interfaces.Commands;
using Book.Domain.Errors;
using Book.Domain.Interfaces.Repositories;
using Book.Infrastructure.Repositories;
using Microsoft.Net.Http.Headers;
using Shared.Wrappers;

namespace Book.Application.Features.AuthorFeatures.Commands.UploadImage;

public class UploadAuthorImageCommandHandler(IUnitOfWork _unitOfWork) : ICommandHandler<UploadAuthorImageCommand, string>
{
    public async Task<Response<string>> Handle(UploadAuthorImageCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.File.Length <= 0)
            {
                return Response.Failure<string>(DomainErrors.Author.AuthorImageNotUploaded);
            }

            var repository = _unitOfWork.GetRepository<IAuthorRepository>();

            var author = await repository.GetByIdAsync(request.Id, cancellationToken);

            if (author is null)
            {
                return Response.Failure<string>(DomainErrors.Author.AuthorNotFoundById);
            }
            
            var dbPath = await SaveImage(request, cancellationToken);
            var resourcePath = Path.Combine("https://localhost:5000", dbPath);
            
            author.ImageUrl = resourcePath;
            repository.Update(author);

            await _unitOfWork.SaveAsync(cancellationToken);

            return resourcePath;
        }
        catch
        {
            return Response.Failure<string>(DomainErrors.Author.AuthorImageNotUploaded);
        }
    }

    private async Task<string> SaveImage(UploadAuthorImageCommand request, CancellationToken cancellationToken)
    {
        var folderName = Path.Combine("Resources", "Images", "Authors");
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