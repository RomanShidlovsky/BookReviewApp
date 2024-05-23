using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Book.Application.Features.BookFeatures.Commands.UpdateRating;

public sealed record UpdateBookRatingCommand(IBookRatingUpdated Dto) : ICommand;