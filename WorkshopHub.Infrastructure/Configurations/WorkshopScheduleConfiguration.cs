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
    public sealed class WorkshopScheduleConfiguration : IEntityTypeConfiguration<WorkshopSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkshopSchedule> builder)
        {
            builder
                .Property(ws => ws.WorkshopId)
                .IsRequired();

            builder
                .Property(ws => ws.StartTime)
                .IsRequired();

            builder
                .Property(ws => ws.EndTime)
                .IsRequired();

            builder
                .HasOne(w => w.Workshop)
                .WithMany(ws => ws.WorkshopSchedules)
                .HasForeignKey(w => w.WorkshopId)
                .HasConstraintName("FK_WorkshopSchedule_Workshop_WorkshopId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
