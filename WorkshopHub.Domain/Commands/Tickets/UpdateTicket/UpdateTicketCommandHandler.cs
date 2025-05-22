using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Ticket;

namespace WorkshopHub.Domain.Commands.Tickets.UpdateTicket
{
    public sealed class UpdateTicketCommandHandler : CommandHandlerBase, IRequestHandler<UpdateTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;

        public UpdateTicketCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            ITicketRepository ticketRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);

            if(ticket == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any ticket with id: {request.TicketId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            ticket.SetUserId(request.TicketId);
            ticket.SetWorkshopId(request.WorkshopId);

            _ticketRepository.Update(ticket);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new TicketUpdatedEvent(request.TicketId));
            }
        }
    }
}
