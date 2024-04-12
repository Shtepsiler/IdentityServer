using System.Linq;
using BlazorAppForClient.ViewModels;
using FluentValidation;

namespace BlazorAppForClient.Validation
{
    public class UserSignUpViewModelValidator : AbstractValidator<UserSignUpViewModel>
    {
        public UserSignUpViewModelValidator()
        {
            RuleFor(request => request.userName)
                .NotEmpty()
                .WithMessage("UserName can't be empty.");

            RuleFor(request => request.email)
                .EmailAddress()
                .WithMessage("Wrong email address.");

            RuleFor(request => request.password)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.password)} can't be empty.")
                .Must(password => password is not null && password.Any(char.IsUpper))
                .WithMessage(request => $"{nameof(request.password)} must contain an uppercase character.")
                .Must(password => password is not null && password.Any(char.IsLower))
                .WithMessage(request => $"{nameof(request.password)} must contain a lowercase character.")
                .Must(password => password is not null && password.Any(char.IsDigit))
                .WithMessage(request => $"{nameof(request.password)} must contain a digit.");
            //add a non diggit validation
                /*
                .Must(password => password is not null && password.Any(char.Id))*/
            /*
                        RuleFor(request =>  request)
                            .Must(p => p.ConfirmPassword.Equals(p.ConfirmPassword))
                            .WithMessage("Password is not equel each other");
            */
            RuleFor(request => request.ConfirmPassword)
                      .Equal(p => p.password)
                      .WithMessage($"Passwords are different.");

            RuleFor(request => request.firstName)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.firstName)} can't be empty.")
                .MaximumLength(50)
                .WithMessage(request => $"{nameof(request.firstName)} should be less than 50 characters.");

            RuleFor(request => request.lastName)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.lastName)} can't be empty.")
                .MaximumLength(50)
                .WithMessage(request => $"{nameof(request.lastName)} should be less than 50 characters.");

          
        }
        
    }
}
