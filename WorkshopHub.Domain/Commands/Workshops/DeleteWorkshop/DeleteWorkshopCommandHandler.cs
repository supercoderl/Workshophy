using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Shared.Events.Workshop;

namespace WorkshopHub.Domain.Commands.Workshops.DeleteWorkshop
{
    public sealed class DeleteWorkshopCommandHandler : CommandHandlerBase, IRequestHandler<DeleteWorkshopCommand>
    {
        private readonly IWorkshopRepository _workshopRepository;

        public DeleteWorkshopCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IWorkshopRepository workshopRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _workshopRepository = workshopRepository;
        }

        public async Task Handle(DeleteWorkshopCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var workshop = await _workshopRepository.GetByIdAsync(request.WorkshopId);

            if (workshop == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is not any workshop with id: {request.WorkshopId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            _workshopRepository.Remove(workshop);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new WorkshopDeletedEvent(request.WorkshopId));
            }
        }
    }
}
