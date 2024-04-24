using FluentValidation;

namespace Book.Application.Features.SubjectFeatures.Commands.Create;

public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(s => s.Dto.Name)
            .NotEmpty().WithMessage("The Name field is required.")
            .MaximumLength(255).WithMessage("The Name field must not exceed 255 characters.");
    }
}