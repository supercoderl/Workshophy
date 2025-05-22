using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Tickets.UpdateTicket
{
    public sealed class UpdateTicketCommandValidation : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketCommandValidation()
        {
            RuleForId();
            RuleForUserId();
            RuleForWorkshopId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.TicketId).NotEmpty().WithErrorCode(DomainErrorCodes.Ticket.EmptyId).WithMessage("Ticket id may not be empty.");
        }

        public void RuleForUserId()
        {
            RuleFor(cmd => cmd.UserId).NotEmpty().WithErrorCode(DomainErrorCodes.Ticket.EmptyUserId).WithMessage("User id may not be empty.");
        }

        public void RuleForWorkshopId()
        {
            RuleFor(cmd => cmd.WorkshopId).NotEmpty().WithErrorCode(DomainErrorCodes.Ticket.EmptyWorkshopId).WithMessage("Workshop id may not be empty.");
        }
    }
}
