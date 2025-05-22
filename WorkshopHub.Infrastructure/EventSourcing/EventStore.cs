using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.DomainEvents;
using WorkshopHub.Domain.DomainNotifications;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Infrastructure.Database;
using WorkshopHub.Shared.Events;

namespace WorkshopHub.Infrastructure.EventSourcing
{
    public sealed class DomainEventStore : IDomainEventStore
    {
        private readonly IEventStoreContext _context;
        private readonly DomainNotificationStoreDbContext _domainNotificationStoreDbContext;
        private readonly EventStoreDbContext _eventStoreDbContext;

        public DomainEventStore(
            EventStoreDbContext eventStoreDbContext,
            DomainNotificationStoreDbContext domainNotificationStoreDbContext,
            IEventStoreContext context)
        {
            _eventStoreDbContext = eventStoreDbContext;
            _domainNotificationStoreDbContext = domainNotificationStoreDbContext;
            _context = context;
        }

        public async Task SaveAsync<T>(T domainEvent) where T : DomainEvent
        {
            var serializedData = JsonConvert.SerializeObject(domainEvent);

            switch (domainEvent)
            {
                case DomainNotification d:
                    var storedDomainNotification = new StoredDomainNotification(
                        d,
                        serializedData,
                        _context.GetUserEmail(),
                        _context.GetCorrelationId());

                    _domainNotificationStoreDbContext.StoredDomainNotifications.Add(storedDomainNotification);
                    await _domainNotificationStoreDbContext.SaveChangesAsync();

                    break;
                default:
                    var storedDomainEvent = new StoredDomainEvent(
                        domainEvent,
                        serializedData,
                        _context.GetUserEmail(),
                        _context.GetCorrelationId());

                    _eventStoreDbContext.StoredDomainEvents.Add(storedDomainEvent);
                    await _eventStoreDbContext.SaveChangesAsync();

                    break;
            }
        }
    }
}
