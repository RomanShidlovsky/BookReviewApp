using FluentValidation;

namespace Book.Application.Features.SubjectFeatures.Commands.Update;

public class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidator()
    {
        RuleFor(s => s.Dto.Id)
            .NotEmpty().WithMessage("The Id field is required.");
        RuleFor(s => s.Dto.Name)
            .NotEmpty().WithMessage("The Name field is required.")
            .MaximumLength(255).WithMessage("The Name field must not exceed 255 characters.");
    }
}