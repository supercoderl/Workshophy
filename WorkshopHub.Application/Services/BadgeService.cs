using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopHub.Application.Interfaces;
using WorkshopHub.Application.Queries.Badges.GetAll;
using WorkshopHub.Application.Queries.Badges.GetBadgeById;
using WorkshopHub.Application.ViewModels;
using WorkshopHub.Application.ViewModels.Badges;
using WorkshopHub.Application.ViewModels.Sorting;
using WorkshopHub.Domain.Commands.Badges.CreateBadge;
using WorkshopHub.Domain.Commands.Badges.DeleteBadge;
using WorkshopHub.Domain.Commands.Badges.UpdateBadge;
using WorkshopHub.Domain.Interfaces;

namespace WorkshopHub.Application.Services
{
    public class BadgeService : IBadgeService
    {
        private readonly IMediatorHandler _bus;

        public BadgeService(IMediatorHandler bus)
        {
            _bus = bus;
        }

        public async Task<Guid> CreateBadgeAsync(CreateBadgeViewModel badge)
        {
            var badgeId = Guid.NewGuid();
            await _bus.SendCommandAsync(new CreateBadgeCommand(
                badgeId,
                badge.Name,
                badge.Description,
                badge.Area,
                badge.ImageUrl
            ));

            return badgeId;
        }

        public async Task DeleteBadgeAsync(DeleteBadgeViewModel badge)
        {
            await _bus.SendCommandAsync(new DeleteBadgeCommand(badge.BadgeId));
        }

        public async Task<PagedResult<BadgeViewModel>> GetAllBadgesAsync(PageQuery query, bool includeDeleted, string searchTerm = "", SortQuery? sortQuery = null)
        {
            return await _bus.QueryAsync(new GetAllBadgesQuery(query, includeDeleted, searchTerm, sortQuery));
        }

        public async Task<BadgeViewModel?> GetBadgeByIdAsync(Guid badgeId)
        {
            return await _bus.QueryAsync(new GetBadgeByIdQuery(badgeId));
        }

        public async Task UpdateBadgeAsync(UpdateBadgeViewModel badge)
        {
            await _bus.SendCommandAsync(new UpdateBadgeCommand(
                badge.BadgeId,
                badge.Name,
                badge.Description,
                badge.Area,
                badge.ImageUrl
            ));
        }
    }
}
