using MediatR;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Application.ViewModels.Tickets;
using WorkshopHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WorkshopHub.Application.Extensions;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Queries.Tickets.GetAll
{
    public sealed class GetAllTicketsQueryHandler :
            IRequestHandler<GetAllTicketsQuery, PagedResult<TicketViewModel>>
    {
        private readonly ISortingExpressionProvider<TicketViewModel, Ticket> _sortingExpressionProvider;
        private readonly IUser _user;
        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketsQueryHandler(
            ITicketRepository ticketRepository,
            ISortingExpressionProvider<TicketViewModel, Ticket> sortingExpressionProvider,
            IUser user
        )
        {
            _ticketRepository = ticketRepository;
            _sortingExpressionProvider = sortingExpressionProvider;
            _user = user;
        }

        public async Task<PagedResult<TicketViewModel>> Handle(
            GetAllTicketsQuery request,
            CancellationToken cancellationToken)
        {
            var ticketsQuery = _ticketRepository
                .GetAllNoTracking()
                .IgnoreQueryFilters()
                .Where(x => request.IncludeDeleted || x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {

            }

            ticketsQuery = ticketsQuery.Where(ticket => ticket.UserId == _user.GetUserId());

            var totalCount = await ticketsQuery.CountAsync(cancellationToken);

            ticketsQuery = ticketsQuery.GetOrderedQueryable(request.SortQuery, _sortingExpressionProvider);

            var tickets = await ticketsQuery
                .Skip((request.Query.Page - 1) * request.Query.PageSize)
                .Take(request.Query.PageSize)
                .Select(ticket => TicketViewModel.FromTicket(ticket))
                .ToListAsync(cancellationToken);

            return new PagedResult<TicketViewModel>(
                totalCount, tickets, request.Query.Page, request.Query.PageSize);
        }
    }
}
