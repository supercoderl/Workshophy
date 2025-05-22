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
    public sealed class UserInterestConfiguration : IEntityTypeConfiguration<UserInterest>
    {
        public void Configure(EntityTypeBuilder<UserInterest> builder)
        {
            builder
                .Property(ui => ui.UserId)
                .IsRequired();

            builder
                .Property(ui => ui.CategoryId)
                .IsRequired();

            builder
                .HasOne(u => u.User)
                .WithMany(ui => ui.UserInterests)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_UserInterest_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(c => c.Category)
                .WithMany(ui => ui.UserInterests)
                .HasForeignKey(c => c.CategoryId)
                .HasConstraintName("FK_UserInterest_Category_CategoryId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
