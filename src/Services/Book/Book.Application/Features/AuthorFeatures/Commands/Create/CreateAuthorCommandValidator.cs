using Book.Application.DTOs.Author.RequestDTOs;
using FluentValidation;

namespace Book.Application.Features.AuthorFeatures.Commands.Create;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(a => a.Dto.FirstName)
            .NotEmpty().WithMessage("The FirstName field is required.")
            .MaximumLength(25).WithMessage("The FirstName must not exceed 25 characters.");
        RuleFor(a => a.Dto.LastName)
            .NotEmpty().WithMessage("The LastName field is required.")
            .MaximumLength(25).WithMessage("The LastName must not exceed 25 characters.");
        RuleFor(a => a.Dto.FullName)
            .NotEmpty().WithMessage("The FullName field is required.")
            .MaximumLength(100).WithMessage("The FullName must not exceed 100 characters.");
        RuleFor(a => a.Dto.OpenLibraryKey)
            .MaximumLength(50).WithMessage("The OpenLibraryKey must not exceed 50 characters.");
        RuleFor(a => a.Dto.ImageUrl)
            .MaximumLength(2000).WithMessage("The ImageUrl field must not exceed 2000 characters.");
    }
}