using FluentValidation;

namespace Book.Application.Features.BookFeatures.Commands.Update;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(b => b.Dto.Id)
            .NotEmpty().WithMessage("The Id field is required.");
        
        RuleFor(b => b.Dto.OpenLibraryKey)
            .MaximumLength(50).WithMessage("The OpenLibraryKey field must not exceed 50 characters.");
        
        RuleFor(b => b.Dto.Title)
            .NotEmpty().WithMessage("The Title field is required.")
            .MaximumLength(100).WithMessage("The Title field must not exceed 100 characters.");
        
        RuleFor(b => b.Dto.EditionCount)
            .NotEmpty().WithMessage("The EditionCount field is required.");
        
        RuleFor(b => b.Dto.PublicationYear)
            .NotEmpty().WithMessage("The PublicationYear field is required.");
    }
}