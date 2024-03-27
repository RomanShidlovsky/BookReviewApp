using FluentValidation;
using Identity.BusinessLogic.DTOs.RequestDTOs.User;

namespace Identity.BusinessLogic.Validators.User;

public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty().WithMessage("Id is required");
        RuleFor(u => u.UserName)
            .NotEmpty().WithMessage("UserName is required.")
            .MaximumLength(50).WithMessage("UserName must not exceed 50 characters.");
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .MaximumLength(256).WithMessage("Email must not exceed 256 characters.")
            .EmailAddress().WithMessage("Invalid email address.");
    }
}