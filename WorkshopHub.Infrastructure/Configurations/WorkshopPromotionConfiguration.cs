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
    public sealed class WorkshopPromotionConfiguration : IEntityTypeConfiguration<WorkshopPromotion>
    {
        public void Configure(EntityTypeBuilder<WorkshopPromotion> builder)
        {
            builder
                .Property(wp => wp.WorkshopId)
                .IsRequired();

            builder
                .Property(wp => wp.PromotionId)
                .IsRequired();

            builder
                .HasOne(w => w.Workshop)
                .WithMany(wp => wp.WorkshopPromotions)
                .HasForeignKey(w => w.WorkshopId)
                .HasConstraintName("FK_WorkshopPromotion_Workshop_WorkshopId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(p => p.Promotion)
                .WithMany(wp => wp.WorkshopPromotions)
                .HasForeignKey(p => p.PromotionId)
                .HasConstraintName("FK_WorkshopPromotion_Promotion_PromotionId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
