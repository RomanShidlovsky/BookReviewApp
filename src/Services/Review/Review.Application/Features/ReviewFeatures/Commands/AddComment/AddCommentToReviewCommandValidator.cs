using FluentValidation;

namespace Review.Application.Features.ReviewFeatures.Commands.AddComment;

public class AddCommentToReviewCommandValidator : AbstractValidator<AddCommentToReviewCommand>
{
    public AddCommentToReviewCommandValidator()
    {
        RuleFor(command => command.Dto.ReviewId)
            .NotEmpty().WithMessage("The ReviewId field is required.");

        RuleFor(command => command.Dto.Comment.UserId)
            .NotEmpty().WithMessage("The Comment.UserId is required.");

        RuleFor(command => command.Dto.Comment.Text)
            .NotEmpty().WithMessage("The Comment.Text is required.")
            .MaximumLength(10000).WithMessage("The Comment.Text must not exceeds 10000 characters.");
    }
}