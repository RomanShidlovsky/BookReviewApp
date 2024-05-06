using FluentValidation;

namespace Book.Application.Features.LanguageFeatures.Commands.AddLanguageToBook;

public class AddLanguageToBookCommandValidator : AbstractValidator<AddLanguageToBookCommand>
{
    public AddLanguageToBookCommandValidator()
    {
        RuleFor(c => c.Dto.LanguageId)
            .NotEmpty().WithMessage("LanguageId is required.");
        
        RuleFor(c => c.Dto.BookId)
            .NotEmpty().WithMessage("BookId is required.");
    }
}