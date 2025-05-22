using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Ticket
{
    public sealed class TicketCreatedEvent : DomainEvent
    {
        public TicketCreatedEvent(Guid ticketId) : base(ticketId)
        {
            
        }
    }
}
