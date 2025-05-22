using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.ViewModels.Tickets;

namespace WorkshopHub.Application.Queries.Tickets.GetTicketById
{
    public sealed record GetTicketByIdQuery(Guid Id) : IRequest<TicketViewModel?>;
}
