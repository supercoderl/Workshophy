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
using WorkshopHub.Shared.Events.Badge;

namespace WorkshopHub.Domain.Commands.Badges.DeleteBadge
{
    public sealed class DeleteBadgeCommandHandler : CommandHandlerBase, IRequestHandler<DeleteBadgeCommand>
    {
        private readonly IBadgeRepository _badgeRepository;

        public DeleteBadgeCommandHandler(
            IMediatorHandler bus,
            IUnitOfWork unitOfWork,
            INotificationHandler<DomainNotification> notifications,
            IBadgeRepository badgeRepository
        ) : base(bus, unitOfWork, notifications ) 
        {
            _badgeRepository = badgeRepository;
        }

        public async Task Handle(DeleteBadgeCommand request, CancellationToken cancellationToken)
        {
            if (!await TestValidityAsync(request)) return;

            var badge = await _badgeRepository.GetByIdAsync(request.BadgeId);   

            if(badge == null)
            {
                await NotifyAsync(new DomainNotification(
                    request.MessageType,
                    $"There is no any badge with id: {request.BadgeId}.",
                    ErrorCodes.ObjectNotFound
                ));
                return;
            }

            _badgeRepository.Remove(badge);
            
            if(await CommitAsync())
            {
                await Bus.RaiseEventAsync(new BadgeDeletedEvent(request.BadgeId));
            }
        }
    }
}
