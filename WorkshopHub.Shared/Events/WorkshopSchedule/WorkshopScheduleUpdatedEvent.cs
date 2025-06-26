using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.WorkshopSchedule
{
    public sealed class WorkshopScheduleUpdatedEvent : DomainEvent
    {
        public WorkshopScheduleUpdatedEvent(Guid workshopScheduleId) : base(workshopScheduleId) 
        {
            
        }
    }
}
