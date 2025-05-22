using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events
{
    public class FanoutDomainEvent : DomainEvent
    {
        public DomainEvent DomainEvent { get; }
        public Guid? UserId { get; }

        public FanoutDomainEvent(
            Guid aggregateId,
            DomainEvent domainEvent,
            Guid? userId) : base(aggregateId)
        {
            DomainEvent = domainEvent;
            UserId = userId;
        }
    }
}
