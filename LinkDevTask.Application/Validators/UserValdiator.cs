using FluentValidation;
using LinkDevTask.Application.ViewModels.User;

namespace LinkDevTask.Application.Validators
{
    public class UserValdiator: AbstractValidator<UserVM>
    {
        public UserValdiator()
        {
            RuleFor(u => u.UserName)
                .NotEmpty();

            RuleFor(u => u.FirstName)
                .MaximumLength(15).WithMessage("{PropertyName} Must be less than {MaxLength} characters")
                .WithName("First Name");

            RuleFor(u => u.LastName)
                .MaximumLength(15)
                .WithMessage("{PropertyName} Must be less than {MaxLength} characters")
                .WithName("Last Name");

            RuleFor(u => u.Email)
                .EmailAddress()
                .WithMessage("A valid {PropertyName} address is required.")
                .MaximumLength(30)
                .WithMessage("{PropertyName} Must be less than {MaxLength} characters");


            RuleFor(u => u.PhoneNumber)
                .Matches(@"^\d+$")
                .WithMessage("please enter a valid \"{PropertyName}.")
                .MaximumLength(11)
                .WithMessage("{PropertyName} Must be less than {MaxLength} characters")
                .WithName("Phone Number")
                .NotEmpty();
        }
    }
}
