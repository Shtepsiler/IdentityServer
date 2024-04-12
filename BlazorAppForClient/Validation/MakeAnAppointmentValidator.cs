using BlazorAppForClient.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlazorAppForClient.Validation
{
    public class MakeAnAppointmentValidator : AbstractValidator<NewJoobViewModel>
    {
        public MakeAnAppointmentValidator()
        {
            RuleFor(p=>p.ModelName).NotEmpty()
                .WithMessage("ModelName can't be empty.");
            RuleFor(p=>p.IssueDate).NotEmpty()
                .WithMessage("Issue Date can't be empty.");
            RuleFor(p=>p.Description).NotEmpty()
                .WithMessage("Description can't be empty.");
        }
    }
}
