using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Workshops.ApproveWorkshop
{
    public sealed class ApproveWorkshopCommandValidation : AbstractValidator<ApproveWorkshopCommand>
    {
        public ApproveWorkshopCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.WorkshopId).NotEmpty().WithErrorCode(DomainErrorCodes.Workshop.EmptyId).WithMessage("Id may not be empty.");
        }
    }
}
