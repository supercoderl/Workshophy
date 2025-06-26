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
using WorkshopHub.Shared.Events.WorkshopSchedule;

namespace WorkshopHub.Domain.Commands.WorkshopSchedules.UpdateWorkshopSchedule
{
    public sealed class UpdateWorkshopScheduleCommandHandler : CommandHandlerBase, IRequestHandler<UpdateWorkshopScheduleCommand>
    {
        private readonly IWorkshopScheduleRepository _workshopScheduleRepository;

        public UpdateWorkshopScheduleCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IWorkshopScheduleRepository workshopScheduleRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _workshopScheduleRepository = workshopScheduleRepository;
        }

        public async Task Handle(UpdateWorkshopScheduleCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var workshopSchedule = await _workshopScheduleRepository.GetByIdAsync(request.Id);

            if (workshopSchedule == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any schedule with id {request.Id}",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            workshopSchedule.SetStartTime(workshopSchedule.StartTime);
            workshopSchedule.SetEndTime(workshopSchedule.EndTime);
            workshopSchedule.SetScheduleStatus(workshopSchedule.ScheduleStatus);

            _workshopScheduleRepository.Update(workshopSchedule);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new WorkshopScheduleUpdatedEvent(request.Id));
            }
        }
    }
}
