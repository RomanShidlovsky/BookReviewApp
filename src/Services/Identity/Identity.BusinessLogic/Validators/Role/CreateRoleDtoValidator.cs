using FluentValidation;
using Identity.BusinessLogic.DTOs.RequestDTOs.Role;

namespace Identity.BusinessLogic.Validators.Role;

public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
{
    public CreateRoleDtoValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(15).WithMessage("Name must not exceed 15 characters.");
    }
}