using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Tickets.GetAll;
using WorkshopHub.Application.Queries.Tickets.GetTicketById;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels.Tickets;
using WorkshopHub.Domain.Commands.Tickets.CreateTicket;
using WorkshopHub.Domain.Commands.Tickets.DeleteTicket;
using WorkshopHub.Domain.Commands.Tickets.UpdateTicket;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IMediatorHandler _bus;

        public TicketService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateTicketAsync(CreateTicketViewModel ticket)
        {
            var ticketId = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateTicketCommand(ticketId, ticket.UserId, ticket.WorkshopId));
            return ticketId;
        }

        public async Task DeleteTicketAsync(DeleteTicketViewModel ticket)
        {
            await _bus.SendCommandAsync(new DeleteTicketCommand(ticket.TicketId));
        }

        public async Task<PagedResult<TicketViewModel>> GetAllTicketsAsync(PageQuery query, bool includeDeleted, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllTicketsQuery(
                query,
                includeDeleted,
                searchTerm,
                sortQuery
            ));
        }

        public async Task<TicketViewModel?> GetTicketByIdAsync(Guid ticketId)
        {
            return await _bus.QueryAsync(new GetTicketByIdQuery(ticketId));
        }

        public async Task UpdateTicketAsync(UpdateTicketViewModel ticket)
        {
            await _bus.SendCommandAsync(new UpdateTicketCommand(
                ticket.TicketId,
                ticket.UserId,
                ticket.WorkshopId
            ));
        }
    }
}
