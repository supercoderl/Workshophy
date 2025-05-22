using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Workshops.DeleteWorkshop
{
    public sealed class DeleteWorkshopCommandValidation : AbstractValidator<DeleteWorkshopCommand>
    {
        public DeleteWorkshopCommandValidation()
        {
            RuleForWorkshopId();
        }

        public void RuleForWorkshopId()
        {
            RuleFor(cmd => cmd.WorkshopId).NotEmpty().WithErrorCode(DomainErrorCodes.Workshop.EmptyId).WithMessage("Workshop id may not be empty.");
        }
    }
}
