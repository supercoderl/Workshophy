using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Tickets.DeleteTicket
{
    public sealed class DeleteTicketCommand : CommandBase, IRequest
    {
        private static readonly DeleteTicketCommandValidation s_validation = new();

        public Guid TicketId { get; }

        public DeleteTicketCommand(Guid ticketId) : base(Guid.NewGuid())
        {
            TicketId = ticketId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
