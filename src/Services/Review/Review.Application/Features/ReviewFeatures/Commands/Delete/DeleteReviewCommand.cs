using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.ReviewFeatures.Commands.Delete;

public sealed record DeleteReviewCommand(string Id) : IDeleteCommand;