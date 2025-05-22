using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.DomainNotifications;
using WorkshopHub.Infrastructure.Configurations.EventSourcing;

namespace WorkshopHub.Infrastructure.Database
{
    public class DomainNotificationStoreDbContext : DbContext
    {
        public virtual DbSet<StoredDomainNotification> StoredDomainNotifications { get; set; } = null!;

        public DomainNotificationStoreDbContext(DbContextOptions<DomainNotificationStoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StoredDomainNotificationConfiguration());
        }
    }
}
