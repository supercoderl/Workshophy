using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Application.ViewModels.Bookings
{
    public sealed class BookingViewModel
    {
        public Guid BookingId { get; set; }
        public Guid UserId  { get; set; }
        public Guid WorkshopId { get; set; }
        public int Quantity { get; set; }
        public long OrderCode { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PurchasedAt { get; set; }

        public static BookingViewModel FromBooking(Booking booking)
        {
            return new BookingViewModel
            {
                BookingId = booking.Id,
                UserId = booking.UserId,
                WorkshopId = booking.WorkshopId,
                Quantity = booking.Quantity,
                OrderCode = booking.OrderCode,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status,
                CreatedAt = booking.CreatedAt,
                PurchasedAt = booking.PurchasedAt,
            };
        }
    }
}
