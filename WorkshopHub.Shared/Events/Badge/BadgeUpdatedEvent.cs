using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Badge
{
    public sealed class BadgeUpdatedEvent : DomainEvent
    {
        public BadgeUpdatedEvent(Guid badgeId) : base(badgeId)
        {
            
        }
    }
}
