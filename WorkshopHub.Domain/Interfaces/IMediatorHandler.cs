using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Commands;
using WorkshopHub.Shared.Events;

namespace WorkshopHub.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task RaiseEventAsync<T>(T @event) where T : DomainEvent;

        Task SendCommandAsync<T>(T command) where T : CommandBase;

        Task<TResponse> QueryAsync<TResponse>(IRequest<TResponse> query);
    }
}
