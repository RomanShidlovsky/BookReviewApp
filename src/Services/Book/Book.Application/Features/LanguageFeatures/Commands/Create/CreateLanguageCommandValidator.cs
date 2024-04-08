using FluentValidation;

namespace Book.Application.Features.LanguageFeatures.Commands.Create;

public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
{
    public CreateLanguageCommandValidator()
    {
        RuleFor(l => l.Dto.Name)
            .NotEmpty().WithMessage("The Name field is required.")
            .MaximumLength(15).WithMessage("The Name field must not exceed 15 characters.");
    }
}