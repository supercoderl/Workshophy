using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Constanst;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Extensions.Validation;

namespace WorkshopHub.Domain.Commands.Users.Login
{
    public sealed class LoginCommandValidation : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidation()
        {
            AddRuleForEmail();
            AddRuleForPassword();
        }

        private void AddRuleForEmail()
        {
            RuleFor(cmd => cmd.Email)
                .EmailAddress()
                .WithErrorCode(DomainErrorCodes.User.InvalidEmail)
                .WithMessage("Email is not a valid email address")
                .MaximumLength(MaxLengths.User.Email)
                .WithErrorCode(DomainErrorCodes.User.EmailExceedsMaxLength)
                .WithMessage($"Email may not be longer than {MaxLengths.User.Email} characters");
        }

        private void AddRuleForPassword()
        {
            RuleFor(cmd => cmd.Password)
                .Password();
        }
    }
}
