using FluentValidation;

namespace Review.Application.Features.ReviewFeatures.Commands.Update;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(command => command.Dto.Id)
            .NotEmpty()
            .WithMessage("The Id field is required.");
        
        RuleFor(command => command.Dto.Rating)
            .NotEmpty()
            .WithMessage("The Rating field is required.")
            .Must(rating => rating is > 0 and <= 10)
            .WithMessage("The Rating filed should be greater than 0 and less or equal than 10");

        RuleFor(command => command.Dto.Text)
            .MaximumLength(10000)
            .WithMessage("The Text field must not exceed 10000 characters");
    }
}