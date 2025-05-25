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
    public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder
                .Property(b => b.UserId)
                .IsRequired();

            builder
                .Property(b => b.WorkshopId)
                .IsRequired();

            builder
                .Property(b => b.Quantity)
                .IsRequired();

            builder
                .Property(b => b.OrderCode)
                .IsRequired();

            builder
                .Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>();

            builder
                .Property(b => b.CreatedAt)
                .IsRequired();

            builder
                .Property(b => b.PurchasedAt);

            builder
                .ToTable(t => t.HasCheckConstraint("CK_Booking_Status", "[Status] IN ('Pending', 'Paid', 'Failed', 'Canceled')"));

            builder
                .HasOne(u => u.User)
                .WithMany(b => b.Bookings)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Booking_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(w => w.Workshop)
                .WithMany(b => b.Bookings)
                .HasForeignKey(w => w.WorkshopId)
                .HasConstraintName("FK_Booking_Workshop_WorkshopId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
