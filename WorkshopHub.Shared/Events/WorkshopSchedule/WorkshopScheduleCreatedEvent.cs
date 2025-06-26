using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.WorkshopSchedule
{
    public sealed class WorkshopScheduleCreatedEvent : DomainEvent
    {
        public WorkshopScheduleCreatedEvent(Guid workshopScheduleId) : base(workshopScheduleId) 
        {
            
        }
    }
}
