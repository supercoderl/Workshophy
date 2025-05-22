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

namespace WorkshopHub.Domain.Commands.Workshops.UpdateWorkshop
{
    public sealed class UpdateWorkshopCommandHandler : CommandHandlerBase, IRequestHandler<UpdateWorkshopCommand>
    {
        private readonly IWorkshopRepository _workshopRepository;

        public UpdateWorkshopCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IWorkshopRepository workshopRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _workshopRepository = workshopRepository;
        }

        public async Task Handle(UpdateWorkshopCommand request, CancellationToken cancellationToken)
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

            workshop.SetOrganizerId(request.OrganizerId);
            workshop.SetTitle(request.Title);
            workshop.SetDescription(request.Description);
            workshop.SetCategoryId(request.CategoryId);
            workshop.SetLocation(request.Location);
            workshop.SetDurationMinutes(request.DurationMinutes);
            workshop.SetPrice(request.Price);
            workshop.SetIntroVideoUrl(request.IntroVideoUrl);
            workshop.SetStatus(request.Status);

            _workshopRepository.Update(workshop);

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new WorkshopUpdatedEvent(request.WorkshopId));
            }
        }
    }
}
