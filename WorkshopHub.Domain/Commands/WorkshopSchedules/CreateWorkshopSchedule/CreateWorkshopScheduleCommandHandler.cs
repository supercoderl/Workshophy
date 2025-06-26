using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.WorkshopSchedule;

namespace WorkshopHub.Domain.Commands.WorkshopSchedules.CreateWorkshopSchedule
{
    public sealed class CreateWorkshopScheduleCommandHandler : CommandHandlerBase, IRequestHandler<CreateWorkshopScheduleCommand>
    {
        private readonly IWorkshopScheduleRepository _workshopScheduleRepository;

        public CreateWorkshopScheduleCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IWorkshopScheduleRepository workshopScheduleRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _workshopScheduleRepository = workshopScheduleRepository;
        }

        public async Task Handle(CreateWorkshopScheduleCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var workshopSchedule = new Entities.WorkshopSchedule(
                request.Id,
                request.WorkshopId,
                request.StartTime,
                request.EndTime,
                Enums.ScheduleStatus.Pending
            );

            _workshopScheduleRepository.Add( workshopSchedule );    

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new WorkshopScheduleCreatedEvent(workshopSchedule.Id));
            }
        }
    }
}
