using FluentValidation;
using LinkDevTask.Application.ViewModels.Job;

namespace LinkDevTask.Application.Validators
{
    public class JobValidator : AbstractValidator<JobVM>
    {

        public JobValidator()
        {
            RuleFor(j => j.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} Is Required")
                .MaximumLength(20)
                .WithMessage("MaxLength is {MaxLength}")
                .Matches(@"^[a-zA-Z]+$")
                .WithMessage("Enter Letters Only");

            RuleFor(j => j.Description)
                .NotEmpty()
                .WithMessage("{PropertyName} Is Required")
                .MaximumLength(300)
                .WithMessage("MaxLength is {MaxLength}");

            RuleFor(j => j.CategoryId)
                .NotEmpty()
                .WithMessage("{PropertyName} Is Required");

            RuleFor(j => j.Skills)
                .NotEmpty()
                .WithMessage("{PropertyName} Is Required");
        }
    }
}
