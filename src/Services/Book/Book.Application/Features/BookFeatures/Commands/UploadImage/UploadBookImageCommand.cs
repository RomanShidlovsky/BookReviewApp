using Book.Application.Interfaces.Commands;
using Microsoft.AspNetCore.Http;

namespace Book.Application.Features.BookFeatures.Commands.UploadImage;

public sealed record UploadBookImageCommand(int Id, IFormFile File) : ICommand<string>;