using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Constanst;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Users.ForgotPassword
{
    public sealed class ForgotPasswordCommandValidation : AbstractValidator<ForgotPasswordCommand>
    {
        public ForgotPasswordCommandValidation()
        {
            RuleForEmail();
        }

        public void RuleForEmail()
        {
            RuleFor(cmd => cmd.Email)
                .EmailAddress()
                .WithErrorCode(DomainErrorCodes.User.InvalidEmail)
                .WithMessage("Email is not a valid email address")
                .MaximumLength(MaxLengths.User.Email)
                .WithErrorCode(DomainErrorCodes.User.EmailExceedsMaxLength)
                .WithMessage($"Email may not be longer than {MaxLengths.User.Email} characters");
        }
    }
}
