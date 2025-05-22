using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Domain.Errors;
using WorkshopHub.Domain.Interfaces.Repositories;
using WorkshopHub.Domain.Interfaces;
using WorkshopHub.Domain.Notifications;
using WorkshopHub.Application.ViewModels.Badges;

namespace WorkshopHub.Application.Queries.Badges.GetBadgeById
{
    public sealed class GetBadgeByIdQueryHandler :
                IRequestHandler<GetBadgeByIdQuery, BadgeViewModel?>
    {
        private readonly IMediatorHandler _bus;
        private readonly IBadgeRepository _badgeRepository;

        public GetBadgeByIdQueryHandler(IBadgeRepository badgeRepository, IMediatorHandler bus)
        {
            _badgeRepository = badgeRepository;
            _bus = bus;
        }

        public async Task<BadgeViewModel?> Handle(GetBadgeByIdQuery request, CancellationToken cancellationToken)
        {
            var badge = await _badgeRepository.GetByIdAsync(request.Id);

            if (badge is null)
            {
                await _bus.RaiseEventAsync(
                    new DomainNotification(
                        nameof(GetBadgeByIdQuery),
                        $"Badge with id {request.Id} could not be found",
                        ErrorCodes.ObjectNotFound));
                return null;
            }

            return BadgeViewModel.FromBadge(badge);
        }
    }
}
