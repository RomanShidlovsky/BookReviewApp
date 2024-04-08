using FluentValidation;

namespace Book.Application.Features.SubjectFeatures.Commands.AddSubjectToBook;

public class AddSubjectToBookCommandValidator : AbstractValidator<AddSubjectToBookCommand>
{
    public AddSubjectToBookCommandValidator()
    {
        RuleFor(s => s.Dto.SubjectId)
            .NotEmpty().WithMessage("LanguageId is required.");
        RuleFor(s => s.Dto.BookId)
            .NotEmpty().WithMessage("BookId is required.");
    }
}