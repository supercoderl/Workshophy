using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class Ticket : Entity
    {
        public Guid UserId { get; private set; }
        public Guid WorkshopId { get; private set; }
        public DateTime PurchasedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Tickets")]
        public virtual User? User { get; set; }

        [ForeignKey("WorkshopId")]
        [InverseProperty("Tickets")]
        public virtual Workshop? Workshop { get; set; }

        public Ticket(
            Guid id,
            Guid userId,
            Guid workshopId
        ) : base(id)
        {
            UserId = userId;
            WorkshopId = workshopId;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetWorkshopId( Guid workshopId ) { WorkshopId = workshopId; }
    }
}
