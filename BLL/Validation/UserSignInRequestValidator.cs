using BLL.DTO.Requests;
using FluentValidation;

namespace BLL.Validation
{
    public class UserSignInRequestValidator : AbstractValidator<UserSignInRequest>
    {
        public UserSignInRequestValidator()
        {
            RuleFor(request => request.UserName)
                .NotEmpty()
                .WithMessage("UserName can't be empty.");

            RuleFor(request => request.Password)
                .NotEmpty()
                .WithMessage("Password can't be empty.")
                .MinimumLength(8)
                .WithMessage(request => $"{nameof(request.Password)} must be longer then 8 character");
        }
    }
}
