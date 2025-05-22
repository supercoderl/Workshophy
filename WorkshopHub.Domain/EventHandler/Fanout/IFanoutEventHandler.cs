using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Shared.Events;

namespace WorkshopHub.Domain.EventHandler.Fanout
{
    public interface IFanoutEventHandler
    {
        Task<T> HandleDomainEventAsync<T>(T @event) where T : DomainEvent;
    }
}
