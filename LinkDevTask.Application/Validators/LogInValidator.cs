using FluentValidation;
using LinkDevTask.Application.ViewModels.Account;

namespace LinkDevTask.Application.Validators
{
    public class LogInValidator : AbstractValidator<LogInVM>
    {
        public LogInValidator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty()
                .WithMessage("Please Enter Your {PropertyName}")
                .WithName("UserName Or Email");

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
