using FluentValidation;
using POS.Application.Dtos.Request;

namespace POS.Application.Validators.Category;

public class CategoryValidator : AbstractValidator<CategoryRequestDto>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Name is required.")
            .NotEmpty().WithMessage("Name must not be empty.")
            .Must(name => !string.IsNullOrWhiteSpace(name)).WithMessage("Name must not be only whitespace.");
    }
}
