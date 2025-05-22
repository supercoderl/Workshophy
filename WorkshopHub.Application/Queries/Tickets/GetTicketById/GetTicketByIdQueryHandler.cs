using MediatR;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Application.ViewModels.Tickets;

namespace WorkshopHub.Application.Queries.Tickets.GetTicketById
{
    public sealed class GetTicketByIdQueryHandler :
            IRequestHandler<GetTicketByIdQuery, TicketViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly ITicketRepository _ticketRepository;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository, IMediatorHandler bus)
        {
            _ticketRepository = ticketRepository;
            _bus = bus;
        }

        public async Task<TicketViewModel?> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.Id);

            if (ticket is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetTicketByIdQuery),
                        $"Ticket with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return TicketViewModel.FromTicket(ticket);
        }
    }
}
