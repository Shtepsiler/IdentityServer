using BlazorAppForClient.ViewModels;
using FluentValidation;

namespace BlazorAppForClient.Validation
{
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(request => request.email)
                .EmailAddress()
                .WithMessage("Wrong email address.");

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
