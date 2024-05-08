using RabbitMQ.EventBus.Interfaces.BookMessages;
using Review.Application.DTOs.ResponseDTOs;
using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.BookFeatures.Commands.Create;

public record CreateBookCommand(IBookCreated Dto) : ICreateCommand<BookResponseDto>;