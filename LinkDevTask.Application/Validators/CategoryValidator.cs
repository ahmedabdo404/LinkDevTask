using FluentValidation;
using LinkDevTask.Application.ViewModels.Category;

namespace LinkDevTask.Application.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryVM>
    {
        public CategoryValidator()
        {
            RuleFor(cat => cat.Name)
                .MaximumLength(20)
                .WithMessage("MaxLength is {MaxLength}")
                .NotEmpty();
        }
    }
}
