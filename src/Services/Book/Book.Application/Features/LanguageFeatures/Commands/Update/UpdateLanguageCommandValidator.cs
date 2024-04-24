using FluentValidation;

namespace Book.Application.Features.LanguageFeatures.Commands.Update;

public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
{
    public UpdateLanguageCommandValidator()
    {
        RuleFor(l => l.Dto.Id)
            .NotEmpty().WithMessage("The Id field is required.");
        
        RuleFor(l => l.Dto.Name)
            .NotEmpty().WithMessage("The Name field is required.")
            .MaximumLength(15).WithMessage("The Name field must not exceed 15 characters.");
    }
}