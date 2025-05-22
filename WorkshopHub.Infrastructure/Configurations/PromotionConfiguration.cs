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
    public sealed class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder
                .Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Description);

            builder
                .Property(p => p.DiscountPercent)
                .HasPrecision(5, 2);

            builder
                .Property(p => p.ValidFrom)
                .IsRequired();

            builder
                .Property(p => p.ValidUntil)
                .IsRequired();
        }
    }
}
