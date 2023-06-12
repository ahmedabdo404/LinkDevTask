using FluentValidation;
using LinkDevTask.Application.ViewModels.Account;
using System.Text.RegularExpressions;

namespace LinkDevTask.Application.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterVM>
    {
        public RegisterValidator()
        {
            RuleFor(u => u.UserName)
                .Length(3, 30)
                .WithMessage("{PropertyName} Must be be between {MinLength} and {MaxLength} characters")
                .NotEmpty();

            RuleFor(u => u.FirstName)
                .MaximumLength(15)
                .WithMessage("{PropertyName} Must be less than {MaxLength} characters")
                .WithName("First Name")
                .NotEmpty();

            RuleFor(u => u.LastName)
                .MaximumLength(15)
                .WithMessage("{PropertyName} Must be less than {MaxLength} characters")
                .WithName("Last Name")
                .NotEmpty();

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("A valid email address is required.")
                .MaximumLength(30)
                .WithMessage("{PropertyName} Must be less than {MaxLength} characters")
                .NotEmpty();

            RuleFor(u => u.PhoneNumber)
                .Matches(@"^\d+$")
                .WithMessage("please enter a valid \"{PropertyName}.")
                .MaximumLength(11)
                .WithMessage("{PropertyName} Must be less than Or Equal{MaxLength} characters")
                .WithName("Phone Number")
                .NotEmpty();


            RuleFor(x => x.Password)
                .MinimumLength(8)
                .WithMessage("{PropertyName} Must be More than {MinLength} characters")
                .MaximumLength(30)
                .WithMessage("{PropertyName} Must be less than {MaxLength} characters")
                .Matches(@"^(?=.*?[#?!@$%^&*-])(?=.*[a-z])(?=.*[A-Z]).{4,}$", RegexOptions.Singleline)
                .WithMessage("{PropertyName} must include at least one upperCase and Special characters")
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .Equal(u => u.Password)
                .WithMessage("Password not match")
                .NotEmpty();
        }
    }
}
