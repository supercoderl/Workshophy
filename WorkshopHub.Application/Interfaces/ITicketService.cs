using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Application.ViewModels.Tickets;

namespace WorkshopHub.Application.Interfaces
{
    public interface ITicketService
    {
        public Task<TicketViewModel?> GetTicketByIdAsync(Guid ticketId);

        public Task<PagedResult<TicketViewModel>> GetAllTicketsAsync(
            PageQuery query,
            bool includeDeleted,
            string searchTerm = "",
            SortQuery? sortQuery = null
        );

        public Task<Guid> CreateTicketAsync(CreateTicketViewModel ticket);
        public Task UpdateTicketAsync(UpdateTicketViewModel ticket);
        public Task DeleteTicketAsync(DeleteTicketViewModel ticket);
    }
}
