using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Bookings
{
    public class BookingGroupByOwnerViewModel
    {
        public string OwnerName { get; set; } = string.Empty;
        public string AccountBank { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<WorkshopBookingGroupViewModel> Workshops { get; set; } = new();
    }

    public class WorkshopBookingGroupViewModel
    {
        public Guid WorkshopId { get; set; }
        public string WorkshopName { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public List<BookingViewModel> Bookings { get; set; } = new();
    }
}
