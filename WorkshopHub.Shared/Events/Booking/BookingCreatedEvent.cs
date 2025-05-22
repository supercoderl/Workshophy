using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Shared.Events.Booking
{
    public sealed class BookingCreatedEvent : DomainEvent
    {
        public BookingCreatedEvent(Guid bookingId) : base(bookingId)
        {
            
        }
    }
}
