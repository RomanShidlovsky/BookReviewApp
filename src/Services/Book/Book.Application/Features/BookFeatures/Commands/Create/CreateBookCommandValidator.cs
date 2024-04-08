using FluentValidation;

namespace Book.Application.Features.BookFeatures.Commands.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(b => b.Dto.OpenLibraryKey)
            .MaximumLength(50).WithMessage("The OpenLibraryKey field must not exceed 50 characters.");
        RuleFor(b => b.Dto.Title)
            .NotEmpty().WithMessage("The Title field is required.")
            .MaximumLength(100).WithMessage("The Title field must not exceed 100 characters.");
        RuleFor(b => b.Dto.EditionCount)
            .NotEmpty().WithMessage("The EditionCount field is required.");
        RuleFor(b => b.Dto.ImageUrl)
            .MaximumLength(2000).WithMessage("The ImageUrl field must not exceed 2000 characters.");
        RuleFor(b => b.Dto.PublicationYear)
            .NotEmpty().WithMessage("The PublicationYear field is required.");
    }
}