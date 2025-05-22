using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Badges.DeleteBadge
{
    public sealed class DeleteBadgeCommandValidation : AbstractValidator<DeleteBadgeCommand>
    {
        public DeleteBadgeCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.BadgeId).NotEmpty().WithErrorCode(DomainErrorCodes.Badge.EmptyId).WithMessage("Badge id may not be empty.");
        }
    }
}
