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
    public sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder
                .Property(r => r.UserId)
                .IsRequired();

            builder
                .Property(r => r.WorkshopId)
                .IsRequired();

            builder
                .Property(r => r.Rating)
                .IsRequired()
                .HasDefaultValue(1);

            builder
                .Property(r => r.Comment);

            builder
                .Property(r => r.CreatedAt)
                .IsRequired();

            builder
                .ToTable(t => t.HasCheckConstraint("CK_Review_Rating", "[Rating] >= 1 AND [Rating] <= 5"));

            builder
              .HasOne(u => u.User)
              .WithMany(r => r.Reviews)
              .HasForeignKey(u => u.UserId)
              .HasConstraintName("FK_Review_User_UserId")
              .OnDelete(DeleteBehavior.Cascade);

            builder
              .HasOne(w => w.Workshop)
              .WithMany(r => r.Reviews)
              .HasForeignKey(w => w.WorkshopId)
              .HasConstraintName("FK_Review_Workshop_WorkshopId")
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
