using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Domain.Entities
{
    public class Booking : Entity
    {
        public Guid UserId { get; private set; }
        public Guid WorkshopId { get; private set; }
        public int Quantity { get; private set; }
        public BookingStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? PurchasedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Bookings")]
        public virtual User? User { get; set; }

        [ForeignKey("WorkshopId")]
        [InverseProperty("Bookings")]
        public virtual Workshop? Workshop { get; set; }

        public Booking(
            Guid id,
            Guid userId,
            Guid workshopId,
            int quantity
        ) : base(id)
        {
            UserId = userId;
            WorkshopId = workshopId;
            Quantity = quantity;
            Status = BookingStatus.Pending;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetWorkshopId( Guid workshopId ) { WorkshopId = workshopId; }
        public void SetQuantity( int quantity ) { Quantity = quantity; }
        public void SetStatus( BookingStatus status ) { Status = status; }
        public void SetPurchasedAt( DateTime? purchasedAt ) { PurchasedAt = purchasedAt; }
    }
}
