using Book.Application.DTOs.Book.ResponseDTOs;
using Book.Application.Interfaces.Commands;
using RabbitMQ.EventBus.Interfaces.BookMessages;

namespace Book.Application.Features.BookFeatures.Commands.UpdateCriticRating;

public sealed record UpdateBookCriticRatingCommand(IBookCriticRatingUpdated Dto) : IUpdateCommand<BookResponseDto>;