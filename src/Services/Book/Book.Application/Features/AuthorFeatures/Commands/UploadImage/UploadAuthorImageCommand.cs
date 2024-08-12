using Book.Application.Interfaces.Commands;
using Microsoft.AspNetCore.Http;

namespace Book.Application.Features.AuthorFeatures.Commands.UploadImage;

public sealed record UploadAuthorImageCommand(int Id, IFormFile File) : ICommand<string>;