using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Shared.Events.Badge;

namespace WorkshopHub.Domain.Commands.Badges.CreateBadge
{
    public sealed class CreateBadgeCommandHandler : CommandHandlerBase, IRequestHandler<CreateBadgeCommand>
    {
        private readonly IBadgeRepository _badgeRepository;

        public CreateBadgeCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBadgeRepository badgeRepository
        ) : base(bus, unitOfWork, notifications)
        {
            _badgeRepository = badgeRepository;
        }

        public async Task Handle(CreateBadgeCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var badge = new Entities.Badge(
                request.BadgeId,
                request.Name,
                request.Description,
                request.Area,
                request.ImageUrl
            );

            _badgeRepository.Add( badge );

            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new BadgeCreatedEvent(badge.Id));
            }
        }
    }
}
