using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Ticket;

namespace WorkshopHub.Domain.Commands.Tickets.CreateTicket
{
    public sealed class CreateTicketCommandHandler : CommandHandlerBase, IRequestHandler<CreateTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;

        public CreateTicketCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITicketRepository ticketRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var ticket = new Entities.Ticket(
                request.TicketId,
                request.UserId,
                request.WorkshopId
            );

            _ticketRepository.Add( ticket );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TicketCreatedEvent(ticket.Id));
            }
        }
    }
}
