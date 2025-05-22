using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.User
{
    public sealed class UserCreatedEvent : DomainEvent
    {
        public UserCreatedEvent(Guid userId) : base(userId)
        {

        }
    }
}
