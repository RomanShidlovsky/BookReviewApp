using FluentValidation;

namespace Book.Application.Features.LanguageFeatures.Commands.RemoveLanguageFromBook;

public class RemoveLanguageFromBookCommandValidator : AbstractValidator<RemoveLanguageFromBookCommand>
{
    public RemoveLanguageFromBookCommandValidator()
    {
        RuleFor(c => c.Dto.LanguageId)
            .NotEmpty().WithMessage("LanguageId is required.");
        
        RuleFor(c => c.Dto.BookId)
            .NotEmpty().WithMessage("BookId is required.");
    }
}