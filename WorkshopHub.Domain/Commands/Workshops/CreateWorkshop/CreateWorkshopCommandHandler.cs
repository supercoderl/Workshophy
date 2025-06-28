using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Workshop;

namespace WorkshopHub.Domain.Commands.Workshops.CreateWorkshop
{
    public sealed class CreateWorkshopCommandHandler : CommandHandlerBase, IRequestHandler<CreateWorkshopCommand>
    {
        private readonly IWorkshopRepository _workshopRepository;

        public CreateWorkshopCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IWorkshopRepository workshopRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _workshopRepository = workshopRepository;
        }

        public async Task Handle(CreateWorkshopCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var workshop = new Entities.Workshop(
                request.WorkshopId,
                request.OrganizerId,
                request.Title,
                request.Description,
                request.CategoryId,
                request.Location,
                request.Price,
                Enums.WorkshopStatus.Pending,
                request.StartTime,
                request.EndTime,
                Enums.ScheduleStatus.Pending
            );

            _workshopRepository.Add( workshop );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new WorkshopCreatedEvent(workshop.Id));
            }
        }
    }
}
