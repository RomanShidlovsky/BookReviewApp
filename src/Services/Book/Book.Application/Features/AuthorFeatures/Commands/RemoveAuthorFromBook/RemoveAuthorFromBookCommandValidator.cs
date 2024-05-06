using FluentValidation;

namespace Book.Application.Features.AuthorFeatures.Commands.RemoveAuthorFromBook;

public class RemoveAuthorFromBookCommandValidator : AbstractValidator<RemoveAuthorFromBookCommand>
{
    public RemoveAuthorFromBookCommandValidator()
    {
        RuleFor(c => c.Dto.AuthorId)
            .NotEmpty().WithMessage("AuthorId is required.");
        
        RuleFor(c => c.Dto.BookId)
            .NotEmpty().WithMessage("BookId is required.");
    }
}