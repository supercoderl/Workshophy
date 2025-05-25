using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.ActivityPointRule
{
    public sealed class ActivityPointRuleDeletedEvent : DomainEvent
    {
        public ActivityPointRuleDeletedEvent(Guid activityPointRuleId) : base(activityPointRuleId)
        {
            
        }
    }
}
