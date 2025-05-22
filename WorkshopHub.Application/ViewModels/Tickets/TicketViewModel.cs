using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Application.ViewModels.Tickets
{
    public sealed class TicketViewModel
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public Guid WorkshopId { get; set; }
        public DateTime PurchasedAt { get; set; }

        public static TicketViewModel FromTicket(Ticket ticket)
        {
            return new TicketViewModel
            {
                TicketId = ticket.Id,
                UserId = ticket.UserId,
                WorkshopId = ticket.WorkshopId,
                PurchasedAt = ticket.PurchasedAt,
            };
        }
    }
}
