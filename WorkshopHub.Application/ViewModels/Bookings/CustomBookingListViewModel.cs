using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Application.ViewModels.Bookings
{
    public sealed class CustomBookingListViewModelForCustomer
    {
        public string AccountBank { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public ICollection<BookingViewModel> Bookings { get; set; } = new List<BookingViewModel>();
    }
}
