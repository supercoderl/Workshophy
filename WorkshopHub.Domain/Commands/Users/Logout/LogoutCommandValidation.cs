using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Users.Logout
{
    public sealed class LogoutCommandValidation : AbstractValidator<LogoutCommand>
    {
        public LogoutCommandValidation()
        {
            RuleForToken();
        }

        public void RuleForToken()
        {
            RuleFor(cmd => cmd.Token).NotEmpty().WithErrorCode(DomainErrorCodes.RefreshToken.EmptyToken).WithMessage("Token may not be emtpy.");
        }
    }
}
