using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Infrastructure.Configurations
{
    public sealed class PasswordResetTokenConfiguration : IEntityTypeConfiguration<PasswordResetToken>
    {
        public void Configure(EntityTypeBuilder<PasswordResetToken> builder)
        {
            builder
                .Property(p => p.UserId)
                .IsRequired();

            builder
                .Property(p => p.Otp)
                .IsRequired();

            builder
                .Property(p => p.ExpireAt)
                .IsRequired();

            builder
                .Property(p => p.IsUsed)
                .IsRequired();

            builder
                .HasOne(u => u.User)
                .WithMany(p => p.PasswordResetTokens)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_PasswordResetToken_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
