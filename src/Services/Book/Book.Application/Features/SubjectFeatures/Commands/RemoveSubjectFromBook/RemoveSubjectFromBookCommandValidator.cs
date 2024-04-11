using FluentValidation;

namespace Book.Application.Features.SubjectFeatures.Commands.RemoveSubjectFromBook;

public class RemoveSubjectFromBookCommandValidator : AbstractValidator<RemoveSubjectFromBookCommand>
{
    public RemoveSubjectFromBookCommandValidator()
    {
        RuleFor(s => s.Dto.SubjectId)
            .NotEmpty().WithMessage("LanguageId is required.");
        
        RuleFor(s => s.Dto.BookId)
            .NotEmpty().WithMessage("BookId is required.");
    }
}