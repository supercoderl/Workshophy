using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Helpers;

namespace WorkshopHub.Domain.Entities
{
    public class Booking : Entity
    {
        public Guid UserId { get; private set; }
        public Guid WorkshopId { get; private set; }
        public int Quantity { get; private set; }
        public long OrderCode { get; private set; }
        public BookingStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
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
            long orderCode,
            int quantity,
            decimal totalPrice
        ) : base(id)
        {
            UserId = userId;
            WorkshopId = workshopId;
            Quantity = quantity;
            OrderCode = orderCode;
            Status = BookingStatus.Pending;
            TotalPrice = totalPrice;
            CreatedAt = TimeHelper.GetTimeNow();
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetWorkshopId( Guid workshopId ) { WorkshopId = workshopId; }
        public void SetOrderCode(long orderCode) { OrderCode = orderCode; }
        public void SetQuantity( int quantity ) { Quantity = quantity; }
        public void SetTotalPrice(decimal totalPrice) { TotalPrice = totalPrice; }   
        public void SetStatus( BookingStatus status ) { Status = status; }
        public void SetPurchasedAt( DateTime? purchasedAt ) { PurchasedAt = purchasedAt; }
    }
}
