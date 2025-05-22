using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Constanst;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Infrastructure.Configurations
{
    public sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder
              .Property(t => t.UserId)
              .IsRequired();

            builder
              .Property(t => t.WorkshopId)
              .IsRequired();

            builder
              .Property(t => t.PurchasedAt)
              .IsRequired();

            builder
              .HasOne(u => u.User)
              .WithMany(t => t.Tickets)
              .HasForeignKey(u => u.UserId)
              .HasConstraintName("FK_Ticket_User_UserId")
              .OnDelete(DeleteBehavior.Cascade);

            builder
              .HasOne(w => w.Workshop)
              .WithMany(t => t.Tickets)
              .HasForeignKey(w => w.WorkshopId)
              .HasConstraintName("FK_Ticket_Workshop_WorkshopId")
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
