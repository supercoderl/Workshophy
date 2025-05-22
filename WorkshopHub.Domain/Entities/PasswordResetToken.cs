using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class PasswordResetToken : Entity
    {
        public Guid UserId { get; private set; }
        public string Otp { get; private set; }
        public DateTime ExpireAt { get; private set; }
        public bool IsUsed { get; private set; }

        [ForeignKey("UserId")]
        [InverseProperty("PasswordResetTokens")]
        public virtual User? User { get; set; }

        public PasswordResetToken(
            Guid id,
            Guid userId,
            string otp,
            DateTime expireAt,
            bool isUsed
        ) : base(id)
        {
            UserId = userId;
            Otp = otp;
            ExpireAt = expireAt;
            IsUsed = isUsed;
        }

        public void SetUserId( Guid userId ) { UserId = userId; }
        public void SetOTP( string otp ) { Otp = otp; }
        public void SetExpireAt( DateTime expireAt ) { ExpireAt = expireAt; }
        public void SetIsUsed( bool isUsed ) { IsUsed = isUsed; }
    }
}
