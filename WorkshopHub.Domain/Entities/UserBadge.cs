using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class UserBadge : Entity
    {
        public Guid UserId { get; private set; }
        public Guid BadgeId { get; private set; }
        public DateTime AwardedAt { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("UserBadges")]
        public virtual User? User { get; set; }

        [ForeignKey("BadgeId")]
        [InverseProperty("UserBadges")]
        public virtual Badge? Badge { get; set; }

        public UserBadge(
            Guid id,
            Guid userId,
            Guid badgeId,
            DateTime awardedAt
        ) : base(id)
        {
            UserId = userId;
            BadgeId = badgeId;
            AwardedAt = awardedAt;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetBadgeId( Guid badgeId ) { BadgeId = badgeId; }
    }
}
