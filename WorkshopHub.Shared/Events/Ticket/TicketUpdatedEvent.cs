using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Ticket
{
    public sealed class TicketUpdatedEvent : DomainEvent
    {
        public TicketUpdatedEvent(Guid ticketId) : base(ticketId)
        {
            
        }
    }
}
