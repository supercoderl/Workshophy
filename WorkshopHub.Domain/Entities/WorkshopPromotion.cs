using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class WorkshopPromotion : Entity
    {
        public Guid WorkshopId { get; set; }
        public Guid PromotionId { get; set; }

        [ForeignKey("WorkshopId")]
        [InverseProperty("WorkshopPromotions")]
        public virtual Workshop? Workshop { get; set; }

        [ForeignKey("PromotionId")]
        [InverseProperty("WorkshopPromotions")]
        public virtual Promotion? Promotion { get; set; }

        public WorkshopPromotion(
            Guid id,
            Guid workshopId,
            Guid promotionId
        ) : base(id)
        {
            WorkshopId = workshopId;
            PromotionId = promotionId;
        }

        public void SetWorkshopId( Guid workshopId ) {  WorkshopId = workshopId; }
        public void SetPromotionId( Guid promotionId ) { PromotionId = promotionId; }
    }
}
