using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events
{
    public abstract class Message : IRequest
    {
        public Guid AggregateId { get; private set; }
        public string MessageType { get; protected set; }

        protected Message(Guid aggregateId)
        {
            AggregateId = aggregateId;
            MessageType = GetType().Name;
        }

        protected Message(Guid aggregateId, string? messageType)
        {
            AggregateId = aggregateId;
            MessageType = messageType ?? string.Empty;
        }
    }
}
