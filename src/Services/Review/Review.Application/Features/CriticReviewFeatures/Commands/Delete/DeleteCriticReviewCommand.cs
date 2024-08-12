using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Delete;

public sealed record DeleteCriticReviewCommand(string Id) : IDeleteCommand;