using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Workshop
{
    public sealed class WorkshopDeletedEvent : DomainEvent
    {
        public WorkshopDeletedEvent(Guid workshopId) : base(workshopId)
        {
            
        }
    }
}
