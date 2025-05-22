using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class Recommendation : Entity
    {
        public Guid UserId { get; private set; }
        public Guid WorkshopId { get; private set; }
        public DateTime RecommendedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("Recommendations")]
        public virtual User? User { get; set; }

        [ForeignKey("WorkshopId")]
        [InverseProperty("Recommendations")]
        public virtual Workshop? Workshop { get; set; }

        public Recommendation(
            Guid id,
            Guid userId,
            Guid workshopId,
            DateTime recommendedAt
        ) : base(id)
        {
            UserId = userId;
            WorkshopId = workshopId;
            RecommendedAt = recommendedAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetWorkshopId( Guid workshopId ) { WorkshopId = workshopId; }
    }
}
