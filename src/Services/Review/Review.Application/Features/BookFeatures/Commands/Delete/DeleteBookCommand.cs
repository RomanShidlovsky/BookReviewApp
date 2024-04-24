using System.ComponentModel.Design;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.BookFeatures.Commands.Delete;

public sealed record DeleteBookCommand(int Id) : IDeleteCommand;