using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Infrastructure.Configurations
{
    public sealed class UserBadgeConfiguration : IEntityTypeConfiguration<UserBadge>
    {
        public void Configure(EntityTypeBuilder<UserBadge> builder)
        {
            builder
                .Property(ub => ub.UserId)
                .IsRequired();

            builder
                .Property(ub => ub.BadgeId)
                .IsRequired();

            builder
                .Property(ub => ub.AwardedAt)
                .IsRequired();

            builder
                .HasOne(u => u.User)
                .WithMany(ub => ub.UserBadges)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_UserBadge_User_UserId");

            builder
                .HasOne(b => b.Badge)
                .WithMany(ub => ub.UserBadges)
                .HasForeignKey(b => b.BadgeId)
                .HasConstraintName("FK_UserBadge_Badge_BadgeId");
        }
    }

}
