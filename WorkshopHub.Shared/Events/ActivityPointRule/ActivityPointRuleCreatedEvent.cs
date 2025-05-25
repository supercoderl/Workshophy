using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.ActivityPointRule
{
    public sealed class ActivityPointRuleCreatedEvent : DomainEvent
    {
        public ActivityPointRuleCreatedEvent(Guid activityPointRuleId) : base(activityPointRuleId)
        {
            
        }
    }
}
