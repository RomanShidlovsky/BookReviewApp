using FluentValidation;

namespace Book.Application.Features.AuthorFeatures.Commands.AddAuthorToBook;

public class AddAuthorToBookCommandValidator : AbstractValidator<AddAuthorToBookCommand>
{
    public AddAuthorToBookCommandValidator()
    {
        RuleFor(c => c.Dto.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required.");
        
        RuleFor(c => c.Dto.BookId)
            .NotEmpty().WithMessage("BookId is required.");
    }
}