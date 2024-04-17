using Review.Application.Interfaces.Commands;

namespace Review.Application.Features.Review.Commands.Delete;

public sealed record DeleteReviewCommand(int Id) : IDeleteCommand;