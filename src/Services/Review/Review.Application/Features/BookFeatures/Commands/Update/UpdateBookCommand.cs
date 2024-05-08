using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.DTOs.RequestDTOs;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.BookFeatures.Commands.Update;

public sealed record UpdateBookCommand(IBookUpdated Dto) : IUpdateCommand<BookResponseDto>;