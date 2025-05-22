using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Badges.CreateBadge
{
    public sealed class CreateBadgeCommandValidation : AbstractValidator<CreateBadgeCommand>
    {
        public CreateBadgeCommandValidation()
        {
            RuleForName();
            RuleForArea();
            RuleForImageUrl();
        }

        public void RuleForName()
        {
            RuleFor(cmd => cmd.Name).NotEmpty().WithErrorCode(DomainErrorCodes.Badge.EmptyName).WithMessage("Name may not be empty.");
        }

        public void RuleForArea()
        {
            RuleFor(cmd => cmd.Area).NotEmpty().WithErrorCode(DomainErrorCodes.Badge.EmptyArea).WithMessage("Area may not be empty.");
        }

        public void RuleForImageUrl()
        {
            RuleFor(cmd => cmd.ImageUrl).NotEmpty().WithErrorCode(DomainErrorCodes.Badge.EmptyImageUrl).WithMessage("Image url may not be empty.");
        }
    }
}
