using System.Linq;
using BLL.DTO.Requests;
using FluentValidation;

namespace BLL.Validation
{
    public class UserSingUpRequestValidator : AbstractValidator<UserSignUpRequest>
    {
        public UserSingUpRequestValidator()
        {
            RuleFor(request => request.UserName)
                .NotEmpty()
                .WithMessage("UserName can't be empty.");

            RuleFor(request => request.Email)
                .EmailAddress()
                .WithMessage("Wrong email address.");

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Password)} can't be empty.")
                .Must(password => password is not null && password.Any(char.IsUpper))
                .WithMessage(request => $"{nameof(request.Password)} must contain an uppercase character.")
                .Must(password => password is not null && password.Any(char.IsLower))
                .WithMessage(request => $"{nameof(request.Password)} must contain a lowercase character.")
                .Must(password => password is not null && password.Any(char.IsDigit))
                .WithMessage(request => $"{nameof(request.Password)} must contain a digit.")
                .MinimumLength(8)
                .WithMessage(request => $"{nameof(request.Password)} must be longer then 8 character");

          /*  RuleFor(request => request.FirstName)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.FirstName)} can't be empty.")
                .MaximumLength(50)
                .WithMessage(request => $"{nameof(request.FirstName)} should be less than 50 characters.");

            RuleFor(request => request.LastName)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.LastName)} can't be empty.")
                .MaximumLength(50)
                .WithMessage(request => $"{nameof(request.LastName)} should be less than 50 characters.");*/


        }
    }
}
