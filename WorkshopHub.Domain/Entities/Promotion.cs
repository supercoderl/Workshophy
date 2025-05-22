using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class Promotion : Entity
    {
        public string Code { get; private set; }
        public string? Description { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public DateTime ValidFrom { get; private set; }
        public DateTime ValidUntil { get; private set; }

        [InverseProperty("Promotion")]
        public virtual ICollection<WorkshopPromotion> WorkshopPromotions { get; set; } = new List<WorkshopPromotion>();

        public Promotion(
            Guid id, 
            string code, 
            string? description,
            decimal discountPercent, 
            DateTime validFrom, 
            DateTime validUntil
        ) : base(id)
        {
            Code = code;
            Description = description;
            DiscountPercent = discountPercent;
            ValidFrom = validFrom;
            ValidUntil = validUntil;
        }

        public void SetCode( string code ) { Code = code; }
        public void SetDescription( string? description ) { Description = description; }
        public void SetDiscountPercent( decimal discountPercent ) { DiscountPercent = discountPercent; }
        public void SetValidFrom( DateTime validFrom ) { ValidFrom = validFrom; }
        public void SetValidUntil( DateTime validUntil ) { ValidUntil = validUntil; }
    }
}
