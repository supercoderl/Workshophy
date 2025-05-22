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
    public sealed class RecommendationConfiguration : IEntityTypeConfiguration<Recommendation>
    {
        public void Configure(EntityTypeBuilder<Recommendation> builder)
        {
            builder
                .Property(r => r.UserId)
                .IsRequired();

            builder
                .Property(r => r.WorkshopId)
                .IsRequired();

            builder
                .Property(r => r.RecommendedAt)
                .IsRequired();

            builder
                .HasOne(u => u.User)
                .WithMany(r => r.Recommendations)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_Recommendation_User_UserId");

            builder
                .HasOne(w => w.Workshop)
                .WithMany(r => r.Recommendations)
                .HasForeignKey(w => w.WorkshopId)
                .HasConstraintName("FK_Recommendation_Workshop_WorkshopId");
        }
    }
}
