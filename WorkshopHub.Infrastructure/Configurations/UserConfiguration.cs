using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Constanst;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Infrastructure.Configurations
{
    public sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .Property(user => user.Email)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Email);

            builder
                .Property(user => user.FirstName)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.FirstName);

            builder
                .Property(user => user.LastName)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.LastName);

            builder
                .Property(user => user.Password)
                .IsRequired()
                .HasMaxLength(MaxLengths.User.Password);

            builder.HasData(new User(
                Ids.Seed.UserId,
                "admin@email.com",
                "Admin",
                "User",
                // !Password123#
                "$2a$12$Blal/uiFIJdYsCLTMUik/egLbfg3XhbnxBC6Sb5IKz2ZYhiU/MzL2",
                UserRole.Admin,
                UserStatus.Active
            ));
        }
    }
}
