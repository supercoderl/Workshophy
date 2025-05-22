using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.User
{
    public sealed class UserResetPasswordEvent : DomainEvent
    {
        public UserResetPasswordEvent(Guid userId) : base(userId)
        {
            
        }
    }
}
