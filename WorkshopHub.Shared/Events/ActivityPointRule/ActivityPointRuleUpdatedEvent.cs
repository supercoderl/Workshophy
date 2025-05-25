using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.ActivityPointRule
{
    public sealed class ActivityPointRuleUpdatedEvent : DomainEvent
    {
        public ActivityPointRuleUpdatedEvent(Guid activityPointRuleId) : base(activityPointRuleId)
        {
            
        }
    }
}
