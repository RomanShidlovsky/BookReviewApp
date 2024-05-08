using System.ComponentModel.Design;
using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.BookFeatures.Commands.Delete;

public sealed record DeleteBookCommand(IBookDeleted Dto) : IDeleteCommand;