using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Badge
{
    public sealed class BadgeDeletedEvent : DomainEvent
    {
        public BadgeDeletedEvent(Guid badgeId) : base(badgeId)
        {
            
        }
    }
}
