using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Workshop
{
    public sealed class WorkshopCreatedEvent : DomainEvent
    {
        public WorkshopCreatedEvent(Guid workshopId) : base(workshopId)
        {
            
        }
    }
}
