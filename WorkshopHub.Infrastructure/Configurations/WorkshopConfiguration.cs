using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;
using WorkshopHub.Domain.Enums;

namespace WorkshopHub.Infrastructure.Configurations
{
    public sealed class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder
                .Property(w => w.OrganizerId)
                .IsRequired();

            builder
                .Property(w => w.Title)
                .IsRequired();

            builder
                .Property(w => w.Description);

            builder
                .Property(w => w.CategoryId)
                .IsRequired();

            builder
                .Property(w => w.Location)
                .IsRequired();

            builder
                 .Property(w => w.IntroVideoUrl);

            builder
                .Property(w => w.DurationMinutes)
                .IsRequired();

            builder
                .Property(w => w.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            builder
                .Property(w => w.Status)
                .IsRequired()
                .HasDefaultValue(WorkshopStatus.Pending)
                .HasConversion<string>();

            builder
                .Property(w => w.CreatedAt)
                .IsRequired();

            builder
                .ToTable(t => t.HasCheckConstraint("CK_Workshop_Status", "[Status] IN ('Pending', 'Approved', 'Rejected')"));

            builder
              .HasOne(c => c.Category)
              .WithMany(w => w.Workshops)
              .HasForeignKey(c => c.CategoryId)
              .HasConstraintName("FK_Workshop_Category_CategoryId")
              .OnDelete(DeleteBehavior.Cascade);

            builder
              .HasOne(u => u.User)
              .WithMany(w => w.Workshops)
              .HasForeignKey(u => u.OrganizerId)
              .HasConstraintName("FK_Workshop_User_OrganizerId")
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
