using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;

namespace WorkshopHub.Domain.Commands.Tickets.DeleteTicket
{
    public sealed class DeleteTicketCommandValidation : AbstractValidator<DeleteTicketCommand>
    {
        public DeleteTicketCommandValidation()
        {
            RuleForId();
        }

        public void RuleForId()
        {
            RuleFor(cmd => cmd.TicketId).NotEmpty().WithErrorCode(DomainErrorCodes.Ticket.EmptyId).WithMessage("Ticket id may not be empty.");
        }
    }
}
