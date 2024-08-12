using FluentValidation;

namespace Review.Application.Features.CriticReviewFeatures.Commands.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateCriticReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(command => command.Dto.BookId)
            .NotEmpty()
            .WithMessage("The BookId field is required.");

        RuleFor(command => command.Dto.UserId)
            .NotEmpty()
            .WithMessage("The UserId field is required.");

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