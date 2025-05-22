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
    public sealed class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder
                .Property(b => b.Title)
                .IsRequired();

            builder
                .Property(b => b.Content)
                .IsRequired();

            builder
                .Property(b => b.UserId)
                .IsRequired();

            builder
                .Property(b => b.CreatedAt)
                .IsRequired();

            builder
              .HasOne(a => a.Author)
              .WithMany(b => b.BlogPosts)
              .HasForeignKey(a => a.UserId)
              .HasConstraintName("FK_BlogPost_User_UserId")
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
