using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Enums;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Workshop;

namespace WorkshopHub.Domain.Commands.Workshops.ApproveWorkshop
{
    public sealed class ApproveWorkshopCommandHandler : CommandHandlerBase, IRequestHandler<ApproveWorkshopCommand>
    {
        private readonly IWorkshopRepository _workshopRepository;

        public ApproveWorkshopCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IWorkshopRepository workshopRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _workshopRepository = workshopRepository;
        }

        public async Task Handle(ApproveWorkshopCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var workshop = await _workshopRepository.GetByIdAsync(request.WorkshopId);

            if(workshop == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any workshop with id: {request.WorkshopId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            workshop.SetStatus(request.IsAccept ? WorkshopStatus.Approved : WorkshopStatus.Rejected);

            _workshopRepository.Update(workshop);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new WorkshopUpdatedEvent(request.WorkshopId));
            }
        }
    }
}
