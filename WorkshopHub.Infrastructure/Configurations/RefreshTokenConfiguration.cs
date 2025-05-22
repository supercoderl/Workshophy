﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Entities;

namespace WorkshopHub.Infrastructure.Configurations
{
    public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .Property(r => r.UserId)
                .IsRequired();

            builder
                .Property(r => r.Token)
                .IsRequired();

            builder
                .Property(r => r.ExpiryDate)
                .IsRequired();

            builder
                .HasOne(u => u.User)
                .WithMany(r => r.RefreshTokens)
                .HasForeignKey(u => u.UserId)
                .HasConstraintName("FK_RefreshToken_User_UserId")
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
