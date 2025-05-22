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
    public sealed class BadgeConfiguration : IEntityTypeConfiguration<Badge>
    {
        public void Configure(EntityTypeBuilder<Badge> builder)
        {
            builder
                .Property(b => b.Name)
                .IsRequired();

            builder
                .Property(b => b.Description);

            builder
                .Property(b => b.Area)
                .IsRequired();

            builder
                .Property(b => b.ImageUrl)
                .IsRequired();
        }
    }
}
