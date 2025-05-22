using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Commands.Tickets.CreateTicket
{
    public sealed class CreateTicketCommand : CommandBase, IRequest
    {
        private static readonly CreateTicketCommandValidation s_validation = new();

        public Guid TicketId { get; }
        public Guid UserId { get; }
        public Guid WorkshopId { get; }

        public CreateTicketCommand(
            Guid ticketId,
            Guid userId,
            Guid workshopId
        ) : base(Guid.NewGuid())
        {
            TicketId = ticketId;
            UserId = userId;
            WorkshopId = workshopId;
        }

        public override bool IsValid()
        {
            ValidationResult = s_validation.Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
