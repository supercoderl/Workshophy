using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Users.AddPoint
{
    public sealed class AddPointCommandValidation : AbstractValidator<AddPointCommand>
    {
        public AddPointCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty().WithErrorCode(DomainErrorCodes.User.EmptyId).WithMessage("Id may not be empty.");
        }
    }
}
